namespace QuizPlizGame
{
    internal class RepeatQuestionState : State
    {
        public RepeatQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void Handle()
        {
            gameStateMachine.game.MakeTurn(GameTimer, QuizPart);
        }
    }
}

