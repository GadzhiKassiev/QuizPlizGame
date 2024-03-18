using ConsoleGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizPlizGame;
using System.Collections.Generic;
using System.Reflection;

namespace GameUnitTest
{
    [TestClass]
    public class GameTests
    {
        Game game;
        IDisplayer displayer;
        FieldInfo fieldQuestion;
        FieldInfo fieldTime;
        MethodInfo handleUserChoiceAnswerMethod;

        [TestInitialize] 
        public void TestInitialize() 
        {
            displayer = new DummyDisplayer();
            IController controller = new ConsoleController();
            IStorageProvider storage = new DummyStorage();
            game = new Game(displayer, controller, storage);
            fieldQuestion = game.GetType().GetField("_quizQuestion", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldTime = game.GetType().GetField("_timer", BindingFlags.NonPublic | BindingFlags.Instance);
            handleUserChoiceAnswerMethod = typeof(Game).GetMethod("HandleUserChoiceAnswer", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [TestMethod]
        public void HandleUserChoiceAnswer_CorrectAnswer_IncrementsScoreAndShowsSuccess()
        {       
            ChosenAnswer chosenAnswer = ChosenAnswer.Answer2;
            string expectedAnswerStatus = "Correct";
            int initialScore = game.Player.Score;

            QuizQuestion question = new QuizQuestion("", null, (int)chosenAnswer);
            fieldQuestion.SetValue(game, question);

            GameTimer _timer = new GameTimer(displayer);
            fieldTime.SetValue(game, _timer);

            handleUserChoiceAnswerMethod.Invoke(game, new object[] { chosenAnswer });

            Assert.AreEqual(initialScore + 1, game.Player.Score);
            AssertIsAnswerCorrect(expectedAnswerStatus, (int)chosenAnswer);
        }

        [TestMethod]
        public void HandleUserChoiceAnswer_IncorrectAnswer_DecrementsScoreAndShowsNoCorrect()
        {
            ChosenAnswer incorrectAnswer = ChosenAnswer.Answer2;
            int correctAnswer = 3;
            string expectedAnswerStatus = "NotCorrect";
            int initialScore = game.Player.Score;

            QuizQuestion question = new QuizQuestion("", null, correctAnswer);
            fieldQuestion.SetValue(game, question);

            GameTimer _timer = new GameTimer(displayer);
            fieldTime.SetValue(game, _timer);

            handleUserChoiceAnswerMethod.Invoke(game, new object[] { incorrectAnswer });

            Assert.AreEqual(initialScore - 1, game.Player.Score);
            AssertIsAnswerCorrect(expectedAnswerStatus, correctAnswer);
        }

        [TestMethod]
        public void HandleUserChoiceAnswer_InvalidInput_ShowsNoCorrectButton()
        {
            ChosenAnswer chosenAnswer = ChosenAnswer.None;
            string expectedAnswerStatus = "InvalidInput";
            int correctAnswer = 3;

            QuizQuestion question = new QuizQuestion("", null, correctAnswer);
            fieldQuestion.SetValue(game, question);

            GameTimer _timer = new GameTimer(displayer);
            fieldTime.SetValue(game, _timer);

            handleUserChoiceAnswerMethod.Invoke(game, new object[] { chosenAnswer });

            AssertIsAnswerCorrect(expectedAnswerStatus,correctAnswer);
        }

        [TestMethod]
        private void AssertIsAnswerCorrect(string expectedAnswerStatus, int correctAnswer)
        {
            Assert.IsTrue(game.IsAnswered);
            Assert.AreEqual(expectedAnswerStatus, game.AnswerStatus.ToString());
            Assert.AreEqual(correctAnswer, ((QuizQuestion)fieldQuestion.GetValue(game)).Correct);
        }
    }

    public class DummyDisplayer: IDisplayer
    {  
        public void Clear()
        {
        }

        public void Greetings()
        {
        }

        public void ShowEndTime()
        {
        }

        public void ShowGameStats(IEnumerable<Report> fm)
        {
        }

        public void ShowInPosition(string text, int x, int y)
        {
        }

        public void ShowNoCorrect()
        {
        }

        public void ShowNoCorrectButton()
        {
        }

        public void ShowQuestion(QuizQuestion questionData)
        {
        }

        public void ShowSuccess()
        {
        }

        public void ShowTime(int time)
        {
        }
    }

    public class DummyStorage : IStorageProvider
    {
        IQuestionRepository IStorageProvider.GetDataRepository()
        {
            return new DummyRepository();
        }

        IReportRepository IStorageProvider.GetReportRepository()
        {
            return null;
        }
    }

    public class DummyRepository : IQuestionRepository
    {
        QuizQuestion[] qq = { new QuizQuestion("question", new QuizAnswer("1", "2", "3", "4"), 0) };
        QuizQuestion[] IQuestionRepository.Read()
        {
            return qq;
        }
    }
}
