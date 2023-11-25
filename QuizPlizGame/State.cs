using System.Collections.Generic;

namespace QuizPlizGame
{
    public abstract class State
    {
        protected GameStateMachine gameStateMachine;

        protected Player GetPlayer { get { return gameStateMachine.Player; } }
        protected Stack<QuizPart> GetData { get { return gameStateMachine.DataGame; } }

        public GameTimer GameTimer { get; set; }
        public QuizPart QuizPart { get; set; }

        abstract public void Handle();
    }
}

