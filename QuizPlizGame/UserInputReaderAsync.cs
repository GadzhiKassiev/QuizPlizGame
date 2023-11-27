using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuizPlizGame
{
    internal class UserInputReaderAsync
    {
        private Task inputTask;
        private CancellationTokenSource cancellationTokenSource;
        private Game game;

        public UserInputReaderAsync(Game game)
        {
            this.game = game;
        }

        public async Task WaitForInputAsync(Action timeout, int timeOutMillisecs = Timeout.Infinite)
        {
            cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            try
            {
                await Task.Run(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    await Task.Delay(timeOutMillisecs, cancellationToken);

                    if (!game.IsAnswered)
                    {
                        timeout();
                    }
                }, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // Handle cancellation if needed
            }
        }

        public void CancelInputWaiting()
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
