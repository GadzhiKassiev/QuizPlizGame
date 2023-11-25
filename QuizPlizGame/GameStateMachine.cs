﻿using System.Collections.Generic;


namespace QuizPlizGame
{
    public class GameStateMachine
    {
        public Game game;
        State _nextQuestionState;
        State _repeadQuestionState;
        State _noQuestionState;
        State _state;

        public Player Player { get { return game.Player; } }
        public Stack<QuizPart> DataGame { get { return game.Data; } }

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
                _state.Handle();
            }
        }

        public void setState(State state)
        {
            _state = state;
        }

        public IDisplayer getDisplaer()
        {
            return game.displayer;
        }

        public State getNextQuestionState()
        {
            return _nextQuestionState;
        }

        public State getRepeadQuestionState(GameTimer gameTimer, QuizPart quizPart)
        {
            _repeadQuestionState.QuizPart = quizPart;
            _repeadQuestionState.GameTimer = gameTimer;
            return _repeadQuestionState;
        }

        public State getNoQuestionState()
        {
            return _noQuestionState;
        }
    }
}

