namespace QuizPlizGame
{
    /// <summary>
    /// Представляет интерфейс поставщика хранилища для управления данными и отчетами в игре.
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Получает хранилище данных для управления игровыми вопросами.
        /// </summary>
        /// <returns>Хранилище данных для игровых вопросов.</returns>
        IQuestionRepository GetDataRepository();

        /// <summary>
        ///  Получает хранилище отчетов для управления игровыми отчетами.
        /// </summary>
        /// <returns>Хранилище отчетов игры.</returns>
        IReportRepository GetReportRepository();

    }
}
