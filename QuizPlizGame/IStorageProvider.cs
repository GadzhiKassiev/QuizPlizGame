namespace QuizPlizGame
{
    public interface IStorageProvider
    {
        Game GetService();
        IQuestionRepository getDataRepository();

        IReportRepository getReportRepository();

    }
}
