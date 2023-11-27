using System.Collections.Generic;

namespace QuizPlizGame
{
    public interface IDisplayer
    {
        void ShowQuestion(QuizQuestion questionData);

        void ShowGameStats(IEnumerable<Report> fm);

        void ShowInPosition(string text, int x, int y);

        void Greetings();

        void ShowSuccess();

        void ShowNoCorrectButton();

        void ShowNoCorrect();
        void ShowEndTime();

        void ShowTime(int time);

        void Clear();
    }
}
