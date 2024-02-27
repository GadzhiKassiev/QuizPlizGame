using System;

namespace QuizPlizGame
{
    public enum ChosenMenuOption { None = 0, Start = 1, Report = 2, Exit = 3 }
    public enum ChosenAnswer { None = 0, Answer1 = 1, Answer2 = 2, Answer3 = 3, Answer4 = 4 }

    /// <summary>
    /// Представляет собой интерфейс контроллера для обработки пользовательского ввода и выбора в игре.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Ожидает, пока пользователь сделает выбор из набора опций меню.
        /// </summary>
        /// <param name="act">Действие, которое необходимо выполнить с выбранным пунктом меню.</param>
        void WaitForUserChoiceOption(Action<ChosenMenuOption> act);


        /// <summary>
        /// Ждет, пока пользователь даст ответ на вопрос.
        /// </summary>
        /// <param name="act">Действие, которое будет выполнено с выбранным ответом.</param>
        void WaitForUserChoiceAnswer(Action<ChosenAnswer> act);

    }
}
