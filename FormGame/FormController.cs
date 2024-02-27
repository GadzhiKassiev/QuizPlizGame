using QuizPlizGame;
using System;
using System.Windows.Forms;

namespace FormGame
{
    internal class FormController : IController
    {

        public void WaitForUserChoiceOption(Action<ChosenMenuOption> act)
        {
            ChosenMenuOption option = ChosenMenuOption.None;
            Form1.Instance.getInput.WaitOne();
            if (Form1.Instance.Key == ConsoleKey.Y)
            {
                option = ChosenMenuOption.Start;
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
                option = ChosenMenuOption.Report;
            }
            else if (Form1.Instance.Key == ConsoleKey.N)
            {
                option = ChosenMenuOption.Exit;
            }
            Form1.Instance.Key = null;
            act(option);
        }

        public void WaitForUserChoiceAnswer(Action<ChosenAnswer> act)
        {
            ChosenAnswer answer = ChosenAnswer.None;
            Form1.Instance.getInput.WaitOne();
            if (Form1.Instance.Key == ConsoleKey.D1)
            {
                answer = ChosenAnswer.Answer1;
            }
            else if (Form1.Instance.Key == ConsoleKey.D2)
            {
                answer = ChosenAnswer.Answer2;
            }
            else if (Form1.Instance.Key == ConsoleKey.D3)
            {
                answer = ChosenAnswer.Answer3;
            }
            else if (Form1.Instance.Key == ConsoleKey.D4)
            {
                answer = ChosenAnswer.Answer4;
            }
            Form1.Instance.Key = null;
            act(answer);
        }
    }
}
