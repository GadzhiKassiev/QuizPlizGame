using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizPlizGame
{
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
