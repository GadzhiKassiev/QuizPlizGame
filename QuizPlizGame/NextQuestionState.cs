using System;

namespace QuizPlizGame
{
    internal class NextQuestionState : State
    {
        public NextQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void Handle(Action<GameTimer, QuizQuestion> act)
        {
            var displayer = gameStateMachine.getDisplaer();
            GameTimer = new GameTimer(displayer);
            QuizPart = GetData.Pop();
            displayer.ShowQuestion(QuizPart);
            GameTimer.Start();
            act(GameTimer, QuizPart);
        }
    }
}
