using QuizPlizGame;
using System;
using System.Collections.Generic;

namespace ConsoleGame
{
    public class Screen : IDisplayer
    {
        public static int Width { get; set; }
        public static int Height { get; set; }

        static Screen()
        {
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
        }
        public void ShowQuestion(QuizQuestion questionData)
        {
            Clear();
            Console.WriteLine(questionData.Question);
            Console.WriteLine("1 :  " + questionData.Answer.A1);
            Console.WriteLine("2 :  " + questionData.Answer.A2);
            Console.WriteLine("3 :  " + questionData.Answer.A3);
            Console.WriteLine("4 :  " + questionData.Answer.A4);
        }

        public void ShowGameStats(IEnumerable<Report> fm)
        {
            Clear();
            foreach (var fmItem in fm)
            {
                Console.WriteLine("Дата игры: " + fmItem.GameDate + " Время игры: " + fmItem.GameTime + " сек Очки: " + fmItem.Number);
            }
        }

        public void ShowInPosition(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }

        public void Greetings()
        {
            string text = "сыграем в КВИЗ?(Y - Да, R - рейтинг, N - выход)";
            Console.ForegroundColor = ConsoleColor.Yellow;
            ShowInPosition(text, (Width - text.Length) / 2, Height / 2);
        }

        public void ShowSuccess()
        {
            Clear();
            ShowInPosition("Верно", 0, 0);
        }

        public void ShowNoCorrectButton()
        {
            string text = "Выберите верную кнопку(1,2,3 или 4)";
            ShowInPosition(text, Width - text.Length, 0);
        }

        public void ShowNoCorrect()
        {
            Clear();
            ShowInPosition("Не верно", 0, 0);
        }

        public void ShowEndTime()
        {
            Clear();
            ShowInPosition("Время на ответ истекло", 0, 0);
        }

        public void ShowTime(int time)
        {
            ShowInPosition(time.ToString(), Width / 2, Height / 2);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
