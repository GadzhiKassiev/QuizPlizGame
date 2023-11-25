using System;

namespace QuizPlizGame
{
    internal class NoQuestionState : State
    {
        public NoQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void Handle()
        {
            throw new NotImplementedException();
        }
    }
}
