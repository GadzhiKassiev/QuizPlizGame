namespace QuizPlizGame
{
    /// <summary>
    /// Represents a storage provider interface for managing data and reports in a game.
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Gets the data repository for managing game questions.
        /// </summary>
        /// <returns>The data repository for game questions.</returns>
        IQuestionRepository getDataRepository();

        /// <summary>
        /// Gets the report repository for managing game reports.
        /// </summary>
        /// <returns>The report repository for game reports.</returns>
        IReportRepository getReportRepository();

    }
}
