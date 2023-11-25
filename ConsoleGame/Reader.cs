using System;
using System.Threading;

namespace ConsoleGame
{
    public class UserInputReader
    {
        Thread s_inputThread;
        AutoResetEvent s_getInput, s_gotInput;
        ConsoleKeyInfo s_input;

        public UserInputReader()
        {
            s_getInput = new AutoResetEvent(false);
            s_gotInput = new AutoResetEvent(false);
            s_inputThread = new Thread(reader);
            s_inputThread.IsBackground = true;
            s_inputThread.Start();
        }
        public string ReadLine(int timeOutMillisecs = Timeout.Infinite)
        {
            s_getInput.Set();
            bool success = s_gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return s_input.KeyChar.ToString();
            else
                return "";
        }

        private  void reader()
        {
            while (true)
            {
                s_getInput.WaitOne();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
                s_input = Console.ReadKey();
                s_gotInput.Set();
            }
        }
    }
}
