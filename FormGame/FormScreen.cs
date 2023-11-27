using System;
using System.Windows.Forms;
using System.Collections.Generic;
using QuizPlizGame;

namespace FormGame
{
    internal class FormScreen: IDisplayer
    {
        public FormScreen()
        {
        }

        public void ShowQuestion(QuizQuestion questionData)
        {
            Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {
                Form1.Instance.TextBox.Text = questionData.question;
                Form1.Instance.TextBox.AppendText("\r\n" + "1 :  " + questionData.answer.A1);
                Form1.Instance.TextBox.AppendText("\r\n" + "2 :  " + questionData.answer.A2);
                Form1.Instance.TextBox.AppendText("\r\n" + "3 :  " + questionData.answer.A3);
                Form1.Instance.TextBox.AppendText("\r\n" + "4 :  " + questionData.answer.A4);
            });
        }

        public void ShowGameStats(IEnumerable<Report> fm)
        {
            Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {         
                Clear();
                foreach (var fmItem in fm)
                {
                    Form1.Instance.TextBox.AppendText("\r\n" + "Дата игры: " + fmItem.GameDate + " Время игры: " + fmItem.GameTime + " сек Очки: " + fmItem.Number);
                }
            });
        }

        public void ShowInPosition(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
        }

        public void Greetings()
        {
            Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {
                Form1.Instance.TextBox.Text = "сыграем в КВИЗ?(Start - Да, Report - рейтинг, Exit - выход)";
            });
        }

        public void ShowSuccess()
        {
            Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {
                Form1.Instance.TextBox.Text = "Верно";
            });
        }

        public void ShowNoCorrectButton()
        {
            Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {
                Form1.Instance.TextBox.AppendText("\r\n" + "Выберите верную кнопку(1,2,3 или 4)");
            });
        }

        public void ShowNoCorrect()
        {
            Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {
                Form1.Instance.TextBox.Text = "Не верно";
            });
        }

        public void ShowEndTime()
        {
            Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {
                Form1.Instance.TextBox.Text = "Время на ответ истекло";
            });
        }

        public void ShowTime(int time)
        {
            Form1.Instance.TextBox.Invoke((MethodInvoker)delegate {
                Form1.Instance.CloakBox.Text = time.ToString();
            });
        }

        public void Clear()
        {
            Form1.Instance.TextBox.Text = "";
        }
    }
}
