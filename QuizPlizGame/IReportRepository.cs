namespace QuizPlizGame
{
    /// <summary>
    /// Represents a repository interface for reading and writing reports.
    /// </summary>
    public interface IReportRepository
    {
        /// <summary>
        /// Reads and retrieves an array of reports.
        /// </summary>
        /// <returns>An array of reports.</returns>
        Report[] Read();

        /// <summary>
        /// Writes a report.
        /// </summary>
        /// <param name="unit">The report to be written.</param>
        void Write(Report unit);
    }
}

