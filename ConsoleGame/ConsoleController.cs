using QuizPlizGame;
using System;
using System.Linq;

namespace ConsoleGame
{
    internal class ConsoleController : IController
    {
        public void WaitForUserChoiceOption(Action<int> act)
        {
            ConsoleKeyInfo cki;
      
            cki = Console.ReadKey();
            int index = -1;
            if (cki.Key == ConsoleKey.Y)
            {
                index = 0;
            }
            else if (cki.Key == ConsoleKey.R)
            {
                index = 1;
            }
            else if (cki.Key == ConsoleKey.N)
            {
                index = 2;
            }        
            act(index);
        }

        public void WaitForUserChoiceAnswer(Action<int>act)
        {
            ConsoleKeyInfo cki;
            int index = -1;
            cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.D1)
            {
                index = 1;
            }
            else if (cki.Key == ConsoleKey.D2)
            {
                index = 2;
            }
            else if (cki.Key == ConsoleKey.D3)
            {
                index = 3;
            }
            else if (cki.Key == ConsoleKey.D4)
            {
                index = 4;
            }
            act(index);
        }
    }
}
