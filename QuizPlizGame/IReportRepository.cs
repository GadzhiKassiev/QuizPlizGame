namespace QuizPlizGame
{
    /// <summary>
    /// Представляет собой интерфейс репозитория для чтения и записи отчетов.
    /// </summary>
    public interface IReportRepository
    {
        /// <summary>
        /// Считывает и извлекает массив отчетов.
        /// </summary>
        /// <returns>Массив отчетов.</returns>
        Report[] Read();

        /// <summary>
        /// Составляет отчет.
        /// </summary>
        /// <param name="unit">Отчет, который необходимо написать.</param>
        void Write(Report unit);
    }
}

