using System.Collections.Specialized;
using QuizPlizGame;


namespace FormGame
{
    public class StorageProvider : IStorageProvider
    {
        const string fileNameOfData = "datawithJson.txt";
        const string fileNameOfReport = "report.txt";

        IQuestionRepository dataRepository;
        IReportRepository reportRepository;
        NameValueCollection _nameValueCollection;


        public StorageProvider(NameValueCollection nvc)
        {
            dataRepository = null;
            reportRepository = null;
            _nameValueCollection = nvc;
        }

        public Game GetService() {
            var game = new Game(new FormScreen(), new FormController(), this);
            return game; 
        }

        public IQuestionRepository getDataRepository()
        {
            if (dataRepository == null)
            {
                if (_nameValueCollection["storage"] == "json")
                    dataRepository = new QuestionJSONRepository(fileNameOfData);
                //else другие источники данных
            }
            return dataRepository;
        }


        public IReportRepository getReportRepository()
        {
            if (reportRepository == null)
            {
                if (_nameValueCollection["storage"] == "json")
                    reportRepository = new ReportJSONRepository(fileNameOfReport);
                //else другие источники данных
            }
            return reportRepository;
        }
    }
}
