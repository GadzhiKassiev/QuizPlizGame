namespace QuizPlizGame
{
    /// <summary>
    /// Represents a repository interface for reading quiz questions.
    /// </summary>
    public interface IQuestionRepository
    {
        /// <summary>
        /// Reads and retrieves an array of quiz questions.
        /// </summary>
        /// <returns>An array of quiz questions.</returns>
        QuizQuestion[] Read();
    }
}
