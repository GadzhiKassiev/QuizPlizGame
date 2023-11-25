using System.Threading;


namespace QuizPlizGame
{
    public class GameTimer
    {
        Thread _timerThread;
        System.Timers.Timer _timer;
        int _count;
        IDisplayer _displayer;

        public int CurrentCount
        {
            get
            {
                return _count;
            }
        }

        public GameTimer(IDisplayer displayer)
        {
            _count = 0;
            _timer = new System.Timers.Timer(1000);
            _timerThread = new Thread(ElapseHookup);
            _timerThread.IsBackground = true;
            _timerThread?.Start();
            _displayer = displayer;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _count = 0;
        }

        private void ElapseHookup()
        {
            _timer.Elapsed += HandleTimerElapsed;
        }

        private void HandleTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _displayer.ShowTime(_count++);
        }
    }
}
