using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace QuizPlizGame
{
    public enum AnswerStatus { None, Correct, NotCorrect, InvalidInput, TimeEnd };

    public class Game
    {
        #region Fields  
        const int TimeQuestion = 30;
        IDisplayer _displayer;
        IController _controller;
        IStorageProvider _storageProvider;
        DateTime _timeBeginGame;
        GameStateMachine _gameStateMachine;
        QuizQuestion _quizQuestion;
        GameTimer _timer;
        bool _answered;
        bool _optionChosed;
        UserInputReader _userInputReader;
        AnswerStatus _answerStatus;
        #endregion

        #region Properties
        public AnswerStatus AnswerStatus { get { return _answerStatus; } }
        public IDisplayer Displayer { get { return _displayer; } set { _displayer = value; } }
        public IController Controller { get { return _controller; } set { _controller = value; } }
        public bool IsAnswered { get { return _answered; } set { _answered = value; } }
        public Player Player { get; set; }
        public Stack<QuizQuestion> Data { get; set; }
        #endregion

        #region ctor
        public Game(IDisplayer displayer, IController controller, IStorageProvider storageProvider)
        {
            Player = new Player();
            _gameStateMachine = new GameStateMachine(this);
            _gameStateMachine.GetMakeTurn += MakeTurn;
            this.Displayer = displayer;
            this.Controller = controller;
            IsAnswered = false;
            _optionChosed = false;
            _answerStatus = AnswerStatus.None;
            Init(storageProvider);
        }
        #endregion    

        private void Init(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
            IQuestionRepository qr = _storageProvider.GetDataRepository();
            QuizQuestion[] qp = QuizQuestionShuffle.SimpleShuffle(qr.Read());
            Data = new Stack<QuizQuestion>(qp);
            _storageProvider.GetReportRepository();
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
            _storageProvider.GetReportRepository().Write(fm);
            Displayer.ShowGameStats(new List<Report>() { fm });
        }

        private void Introduce()
        {
            Displayer.Greetings();
        }

        private void ChooseOption()
        {
            while (!_optionChosed)
            {
                Controller.WaitForUserChoiceOption(HandleUserChoiceOption);
            }
            _timeBeginGame = DateTime.Now;
        }

        private void HandleUserChoiceOption(ChosenMenuOption option)
        {
            if (option == ChosenMenuOption.Start)
            {
                _optionChosed = true;
            }
            else if (option == ChosenMenuOption.Report)
            {
                Displayer.ShowGameStats(_storageProvider.GetReportRepository().Read().Take(4));
            }
            else if (option == ChosenMenuOption.Exit)
            {
                Environment.Exit(0);
            }
        }

        private void Play()
        {
            _userInputReader = new UserInputReader(this);
            _userInputReader.GetHandleUserChoiceAnswer += HandleUserChoiceAnswer;
            _gameStateMachine.Launch();
            Player.GameDate = DateTime.Now;
            Player.GameTime = Player.GameDate - _timeBeginGame;
        }

        private void NoSuccessInput()
        {
            _timer.Stop();
            Displayer.ShowEndTime();
            _answerStatus = AnswerStatus.TimeEnd;
        }

        private void MakeTurn(GameTimer gameTimer, QuizQuestion question)
        {
            _timer = gameTimer;
            _quizQuestion = question;

            _userInputReader.WaitForInput(NoSuccessInput, (TimeQuestion - _timer.CurrentCount) * 1000);
            if (_answerStatus == AnswerStatus.InvalidInput)
            {
                _gameStateMachine.SetState(_gameStateMachine.GetRepeadQuestionState(gameTimer, question));
                return;
            }

            Thread.Sleep(2000);
            if (Data.Count <= 0)
            {
                _gameStateMachine.SetState(_gameStateMachine.GetNoQuestionState());
            }
            else
            {
                _gameStateMachine.SetState(_gameStateMachine.GetNextQuestionState());
            }
        }

        private void HandleUserChoiceAnswer(ChosenAnswer chosenAnswer)
        {
            int answer = (int)chosenAnswer;
            if (answer == _quizQuestion.Correct)
            {
                _timer.Stop();
                Player.Score += 1;
                Displayer.ShowSuccess();
                _answered = true;
                _answerStatus = AnswerStatus.Correct;
            }
            else if (answer >= 1 && answer <= 4)
            {
                _timer.Stop();
                Player.Score -= 1;
                Displayer.ShowNoCorrect();
                _answered = true;
                _answerStatus = AnswerStatus.NotCorrect;
            }
            else
            {
                Displayer.ShowNoCorrectButton();
                _answered = true;
                _answerStatus = AnswerStatus.InvalidInput;
            }
        }
    }
}