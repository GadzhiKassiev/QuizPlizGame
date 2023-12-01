using System;

namespace QuizPlizGame
{
    public enum ChosenMenuOption { None = 0, Start = 1, Report = 2, Exit = 3 }
    public enum ChosenAnswer { None = 0, Answer1 = 1, Answer2 = 2, Answer3 = 3, Answer4 = 4 }

    /// <summary>
    /// Represents a controller interface for handling user input and choices in a game.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Waits for the user to make a choice from a set of menu options.
        /// </summary>
        /// <param name="act">The action to be performed with the chosen menu option.</param>
        void WaitForUserChoiceOption(Action<ChosenMenuOption> act);


        /// <summary>
        /// Waits for the user to provide an answer to a question.
        /// </summary>
        /// <param name="act">The action to be performed with the chosen answer.</param>
        void WaitForUserChoiceAnswer(Action<ChosenAnswer> act);

    }
}
