using System.Collections.Generic;
using System;

namespace QuizPlizGame
{
    public abstract class State
    {
        protected GameStateMachine gameStateMachine;

        protected Player GetPlayer { get { return gameStateMachine.Player; } }
        protected Stack<QuizQuestion> GetData { get { return gameStateMachine.DataGame; } }

        public GameTimer GameTimer { get; set; }
        public QuizQuestion QuizPart { get; set; }

        abstract public void Handle(Action<GameTimer,QuizQuestion> act);
    }
}

