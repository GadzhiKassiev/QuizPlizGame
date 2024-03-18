using QuizPlizGame;
using System;

namespace ConsoleGame
{
    public class ConsoleController : IController
    {
        public void WaitForUserChoiceOption(Action<ChosenMenuOption> act)
        {
            ConsoleKeyInfo cki;

            cki = Console.ReadKey();
            ChosenMenuOption option = ChosenMenuOption.None;
            if (cki.Key == ConsoleKey.Y)
            {
                option = ChosenMenuOption.Start;
            }
            else if (cki.Key == ConsoleKey.R)
            {
                option = ChosenMenuOption.Report;
            }
            else if (cki.Key == ConsoleKey.N)
            {
                option = ChosenMenuOption.Exit;
            }
            act(option);
        }

        public void WaitForUserChoiceAnswer(Action<ChosenAnswer> act)
        {
            ConsoleKeyInfo cki;
            ChosenAnswer answer = ChosenAnswer.None;
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.D1)
            {
                answer = ChosenAnswer.Answer1;
            }
            else if (cki.Key == ConsoleKey.D2)
            {
                answer = ChosenAnswer.Answer2;
            }
            else if (cki.Key == ConsoleKey.D3)
            {
                answer = ChosenAnswer.Answer3;
            }
            else if (cki.Key == ConsoleKey.D4)
            {
                answer = ChosenAnswer.Answer4;
            }
            act(answer);
        }
    }
}
