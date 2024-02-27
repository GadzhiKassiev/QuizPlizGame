namespace QuizPlizGame
{
    /// <summary>
    /// Представляет собой интерфейс репозитория для чтения вопросов викторины.
    /// </summary>
    public interface IQuestionRepository
    {
        /// <summary>
        /// Считывает и извлекает массив вопросов викторины.
        /// </summary>
        /// <returns>Массив вопросов викторины.</returns>
        QuizQuestion[] Read();
    }
}
