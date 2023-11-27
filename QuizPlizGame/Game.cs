using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using System.IO;

namespace QuizPlizGame
{
    public enum ChosenMenuOption { None, Start, Report, Exit }
    public enum ChosenAnswer { None = 0, Answer1 = 1, Answer2 = 2, Answer3 = 3, Answer4 = 4 }


    public class Game
    {

        #region Fields      
        public IDisplayer displayer;
        public IController controller;
        const int timeQuestion = 30;
        DateTime _timeBeginGame;
        GameStateMachine _gameStateMachine;
        IStorageProvider _storageProvider;
        QuizQuestion _quizQuestion;
        GameTimer timer;
        bool answered;
        bool optionChosed;
        UserInputReader _userInputReader;
        enum AnswerStatus {None, Correct, NotCorrect, InvalidInput, TimeEnd};
        AnswerStatus answerStatus;

        #endregion

        #region Properties
        public bool IsAnswered 
        { 
            get { return answered; } 
            set { answered = value; }
        }
        public Player Player { get; set; }
        public Stack<QuizQuestion> Data { get; set; }
        #endregion

        #region ctor
        public Game(IDisplayer displayer, IController controller, IStorageProvider storageProvider)
        {
            Player = new Player();
            _gameStateMachine = new GameStateMachine(this);
            this.displayer = displayer;
            this.controller = controller;
            answered = false;
            optionChosed = false;
            answerStatus = AnswerStatus.None;
            Init(storageProvider);
        }
        #endregion    

        private void Init(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
            IQuestionRepository qr = _storageProvider.getDataRepository();
            QuizQuestion[] qp = Shuffle(qr.Read());
            Data = new Stack<QuizQuestion>(qp);
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
            fm.GameDate = Player.GameDate;
            fm.GameTime = Player.GameTime;
            fm.Number = Player.Score;
            _storageProvider.getReportRepository().Write(fm);
            displayer.ShowGameStats(new List<Report>() { fm });
        }

        protected QuizQuestion[] Shuffle(QuizQuestion[] qp)
        {
            Random rnd = new Random();

            for (int i = 0; i < qp.Length; i++)
            {
                int r = rnd.Next(0, i + 1);
                QuizQuestion swap = qp[i];
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

        void HandleUserChoiceOption(ChosenMenuOption option)
        {
            if (option == ChosenMenuOption.Start)
            {
                optionChosed = true;
            }
            else if (option == ChosenMenuOption.Report)
            {
                displayer.ShowGameStats(_storageProvider.getReportRepository().Read().Take(4));
            }
            else if (option == ChosenMenuOption.Exit)
            {
                Environment.Exit(0);
            }
        }

        private void Play()
        {
            _userInputReader = new UserInputReader(this);
            _gameStateMachine.Launch();
            Player.GameDate = DateTime.Now;
            Player.GameTime = Player.GameDate - _timeBeginGame;
        }

        private void NoSuccessInput()
        {
            timer.Stop();
            displayer.ShowEndTime();
            answerStatus = AnswerStatus.TimeEnd;
        }

        public void MakeTurn(GameTimer gameTimer, QuizQuestion quizPart)
        {
            timer = gameTimer;
            _quizQuestion = quizPart;
            // вызывать вайт фор юзер и сдель отсчитывавет таймер если я получил импут
            // то таймер сбрасывается если не получил импут то нужно сказать больше не жду 
            _userInputReader.WaitForInput(NoSuccessInput, (timeQuestion - timer.CurrentCount) * 1000);           
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

        public void HandleUserChoiceAnswer(ChosenAnswer chosenAnswer)
        {
            int answer = (int)chosenAnswer;
            if (answer == int.Parse(_quizQuestion.correct))
            {
                timer.Stop();
                Player.Score += 1;
                displayer.ShowSuccess();
                answered = true;
                answerStatus = AnswerStatus.Correct;
            }
            else if (answer != 1 && answer != 2 && answer != 3 && answer != 4)
            {
                displayer.ShowNoCorrectButton();
                answered = true;
                answerStatus = AnswerStatus.InvalidInput;
            }
            else if (answer == 1 || answer == 2 || answer == 3 || answer == 4)
            {
                timer.Stop();
                Player.Score -= 1;
                displayer.ShowNoCorrect();
                answered = true;
                answerStatus = AnswerStatus.NotCorrect;
            }
        }
    }
}


