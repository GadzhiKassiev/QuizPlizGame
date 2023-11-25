namespace QuizPlizGame
{
    public interface IReportRepository
    {
        Report[] Read();
        void Write(Report unit);
    }
}

