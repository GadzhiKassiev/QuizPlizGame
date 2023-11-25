namespace QuizPlizGame
{
    internal class NextQuestionState : State
    {
        public NextQuestionState(GameStateMachine gsm)
        {
            gameStateMachine = gsm;
        }
        public override void Handle()
        {
            var displayer = gameStateMachine.getDisplaer();
            GameTimer = new GameTimer(displayer);
            QuizPart = GetData.Pop();
            displayer.ShowQuestion(QuizPart);
            GameTimer.Start();
            gameStateMachine.game.MakeTurn(GameTimer, QuizPart);
        }
    }
}
