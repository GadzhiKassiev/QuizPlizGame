using System;
using System.Threading;

namespace QuizPlizGame
{
    public class UserInputReader
    {
        private Thread inputThread;
        private AutoResetEvent getInput, gotInput;
        private Game game;

        public UserInputReader(Game game)
        {
            this.game = game;
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(Reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        public void WaitForInput(Action timeout, int timeOutMillisecs = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (!success)
            {
                timeout();
            }
        }

        private  void Reader()
        {
            while (true)
            {
                getInput.WaitOne();
                while (!game.IsAnswered)
                    game.controller.WaitForUserChoiceAnswer(game.HandleUserChoiceAnswer);
                game.IsAnswered = false;
                gotInput.Set();
            }
        }
    }
}
