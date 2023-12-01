using System;
namespace QuizPlizGame
{
    internal class RepeatQuestionState : State
    {
        public RepeatQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void Handle(Action<GameTimer, QuizQuestion> act)
        {
            act(GameTimer, QuizPart);
        }
    }
}

