using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuizPlizGame
{
    internal class UserInputReaderAsync
    {
        private Game game;
        private CancellationTokenSource cancellationTokenSource;

        public event Action<ChosenAnswer> GetHandleUserChoiceAnswer;

        public UserInputReaderAsync(Game game)
        {
            this.game = game;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task WaitForInput(Action timeout, int timeOutMillisecs = Timeout.Infinite)
        {
            try
            {
                await Task.Delay(timeOutMillisecs, cancellationTokenSource.Token);
                timeout();
            }
            catch (TaskCanceledException)
            {

            }
        }

        public void StopWaitingForInput()
        {
            cancellationTokenSource.Cancel();
        }

        public void StartReadingInput()
        {
            Task.Run(Reader);
        }

        private void Reader()
        {
            while (true)
            {
                while (!game.IsAnswered)
                    game.Controller.WaitForUserChoiceAnswer(GetHandleUserChoiceAnswer);
                game.IsAnswered = false;
            }
        }
    }
}
