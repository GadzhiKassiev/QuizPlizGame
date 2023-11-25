using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using System.IO;

namespace QuizPlizGame
{
    public class Game
    {

        #region Fields      
        public Player Player { get; set; }
        public Stack<QuizPart> Data { get; set; }
        public IDisplayer displayer;
        GameStateMachine _gameStateMachine;
        public IController controller;
        DateTime _timeBeginGame;
        const int timeQuestion = 30;
        IStorageProvider _storageProvider;
        QuizPart qp;
        GameTimer timer;
        bool answered;
        bool optionChosed;
        static Thread s_inputThread;
        static AutoResetEvent s_getInput, s_gotInput;
        enum AnswerStatus {Non, Correct, NotCorrect, InvalidInput, TimeEnd};
        AnswerStatus answerStatus;

        #endregion

        #region ctor
        public Game(IDisplayer displayer, IController controller)
        {
            Player = new Player();
            _gameStateMachine = new GameStateMachine(this);
            this.displayer = displayer;
            this.controller = controller;
            answered = false;
            optionChosed = false;
            answerStatus = AnswerStatus.Non;
        }
        #endregion    

        public void Init(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
            IQuestionRepository qr = _storageProvider.getDataRepository();
            QuizPart[] qp = Shuffle(qr.Read());
            Data = new Stack<QuizPart>(qp);
            _storageProvider.getReportRepository();
        }

        public void Start()
        {
            Run();
            End();
        }

        private void Run()
        {
            Introduce();
            ChooseOption();
            Play();
        }

        private void End()
        {
            Report fm = new Report();
            fm.Data = Player.Date;
            fm.Time = Player.GameTime;
            fm.Number = Player.Point;
            _storageProvider.getReportRepository().Write(fm);
            displayer.ShowReport(new List<Report>() { fm });
        }

        protected QuizPart[] Shuffle(QuizPart[] qp)
        {
            Random rnd = new Random();

            for (int i = 0; i < qp.Length; i++)
            {
                int r = rnd.Next(0, i + 1);
                QuizPart swap = qp[i];
                qp[i] = qp[r];
                qp[r] = swap;
            }
            return qp;
        }

        private void Introduce()
        {
            displayer.Greetings();
        }

        private void ChooseOption()
        {
            while (!optionChosed)
            {
                controller.WaitForUserChoiceOption(HandleUserChoiceOption);
            }
            _timeBeginGame = DateTime.Now;
        }

        void HandleUserChoiceOption(int index)
        {
            if (index == 0)
            {
                optionChosed = true;
            }
            else if (index == 1)
            {
                displayer.ShowReport(_storageProvider.getReportRepository().Read().Take(4));
            }
            else if (index == 2)
            {
                Environment.Exit(0);
            }
        }

        private void Play()
        {
            s_getInput = new AutoResetEvent(false);
            s_gotInput = new AutoResetEvent(false);
            s_inputThread = new Thread(reader);
            s_inputThread.IsBackground = true;
            s_inputThread.Start();
            _gameStateMachine.Launch();
            Player.Date = DateTime.Now;
            Player.GameTime = Player.Date - _timeBeginGame;
        }
        private void reader()
        {
            while (true)
            {
                s_getInput.WaitOne();
                while (!answered)
                    controller.WaitForUserChoiceAnswer(HandleUserChoiceAnswer);
                answered = false;
                s_gotInput.Set();
            }
        }

        private void WaitForInput(int timeOutMillisecs = Timeout.Infinite)
        {
            s_getInput.Set();
            bool success = s_gotInput.WaitOne(timeOutMillisecs);
            if (!success)
            {
                timer.Stop();
                displayer.ShowEndTime();
                answerStatus = AnswerStatus.TimeEnd;
            }
        }

        public void MakeTurn(GameTimer gameTimer, QuizPart quizPart)
        {
            timer = gameTimer;
            qp = quizPart;
            // вызывать вайт фор юзер и сдель отсчитывавет таймер если я получил импут
            // то таймер сбрасывается если не получил импут то нужно сказать больше не жду 
            WaitForInput((timeQuestion - timer.CurrentCount) * 1000);           
            if (answerStatus == AnswerStatus.InvalidInput)
            {
                _gameStateMachine.setState(_gameStateMachine.getRepeadQuestionState(gameTimer, quizPart));
                return;
            }
            
            Thread.Sleep(2000);
            if (Data.Count <= 0)
            {
                _gameStateMachine.setState(_gameStateMachine.getNoQuestionState());
            }
            else
            {
                _gameStateMachine.setState(_gameStateMachine.getNextQuestionState());
            }
        }

        void HandleUserChoiceAnswer(int index)
        {
           if (index == int.Parse(qp.correct))
            {
                timer.Stop();
                Player.Point += 1;
                displayer.ShowSuccess();
                answered = true;
                answerStatus = AnswerStatus.Correct;
            }
            else if (index != 1 && index != 2 && index != 3 && index != 4)
            {
                displayer.ShowNoCorrectButton();
                answered = true;
                answerStatus = AnswerStatus.InvalidInput;
            }
            else if (index == 1 || index == 2 || index == 3 || index == 4)
            {
                timer.Stop();
                Player.Point -= 1;
                displayer.ShowNoCorrect();
                answered = true;
                answerStatus = AnswerStatus.NotCorrect;
            }
        }
    }
}


