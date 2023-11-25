using QuizPlizGame;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FormGame
{
    internal class FormController : IController
    {

        public void WaitForUserChoiceOption(Action<int> act)
        {
            int index = -1;
            while (Form1.Instance.Key == null)
            {
            }
            if (Form1.Instance.Key == ConsoleKey.Y)
            {
                index = 0;
                Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {
                    Form1.Instance.StartButton.Enabled = false;
                    Form1.Instance.ReportButton.Enabled = false;
                    Form1.Instance.ExitButton.Enabled = false;
                    Form1.Instance.ButtonOne.Enabled = true;
                    Form1.Instance.ButtonTwo.Enabled = true;
                    Form1.Instance.ButtonThree.Enabled = true;
                    Form1.Instance.ButtonFour.Enabled = true;
                });
            }
            else if (Form1.Instance.Key == ConsoleKey.R)
            {
                index = 1;
            }
            else if (Form1.Instance.Key == ConsoleKey.N)
            {
                index = 2;
            }
            Form1.Instance.Key = null;
            act(index);
        }

        public void WaitForUserChoiceAnswer(Action<int> act)
        {
            int index = -1;
            while (Form1.Instance.Key == null)
            {
            }
            if (Form1.Instance.Key == ConsoleKey.D1)
            {
                index = 1;
            }
            else if (Form1.Instance.Key == ConsoleKey.D2)
            {
                index = 2;
            }
            else if (Form1.Instance.Key == ConsoleKey.D3)
            {
                index = 3;
            }
            else if (Form1.Instance.Key == ConsoleKey.D4)
            {
                index = 4;
            }
            Form1.Instance.Key = null;
            act(index);
        }
    }
}
