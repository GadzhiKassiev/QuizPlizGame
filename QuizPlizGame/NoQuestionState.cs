using System;

namespace QuizPlizGame
{
    internal class NoQuestionState : State
    {
        public NoQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void Handle(Action<GameTimer, QuizQuestion> act)
        {
            throw new NotImplementedException();
        }
    }
}
