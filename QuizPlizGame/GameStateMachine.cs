using System;
using System.Collections.Generic;


namespace QuizPlizGame
{
    public class GameStateMachine
    {
        public Game game;
        State _nextQuestionState;
        State _repeadQuestionState;
        State _noQuestionState;
        State _state;
        public event Action<GameTimer, QuizQuestion> GetMakeTurn;

        public Player Player { get { return game.Player; } }
        public Stack<QuizQuestion> DataGame { get { return game.Data; } }

        public GameStateMachine(Game g)
        {
            game = g;
            _nextQuestionState = new NextQuestionState(this);
            _repeadQuestionState = new RepeatQuestionState(this);
            _noQuestionState = new NoQuestionState(this);
            _state = _nextQuestionState;
        }

        public void Launch()
        {
            while (_state != _noQuestionState)
            {
                _state.Handle(GetMakeTurn);
            }
        }

        public void SetState(State state)
        {
            _state = state;
        }

        public IDisplayer GetDisplaer()
        {
            return game.Displayer;
        }

        public State GetNextQuestionState()
        {
            return _nextQuestionState;
        }

        public State GetRepeadQuestionState(GameTimer gameTimer, QuizQuestion quizPart)
        {
            _repeadQuestionState.QuizPart = quizPart;
            _repeadQuestionState.GameTimer = gameTimer;
            return _repeadQuestionState;
        }

        public State GetNoQuestionState()
        {
            return _noQuestionState;
        }
    }
}


