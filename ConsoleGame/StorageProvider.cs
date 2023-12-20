using System.Collections.Specialized;
using QuizPlizGame;


namespace ConsoleGame
{
    public class StorageProvider : IStorageProvider
    {
        const string fileNameOfData = "datawithJson.txt";
        const string fileNameOfReport = "report.txt";

        IQuestionRepository _dataRepository;
        IReportRepository _reportRepository;
        NameValueCollection _nameValueCollection;


        public StorageProvider(NameValueCollection nvc)
        {
            _dataRepository = null;
            _reportRepository = null;
            _nameValueCollection = nvc;
        }

        public IQuestionRepository getDataRepository()
        {
            if (_dataRepository == null)
            {
                if (_nameValueCollection["storage"] == "json")
                    _dataRepository = new QuestionJSONRepository(fileNameOfData);
                //else другие источники данных
            }
            return _dataRepository;
        }


        public IReportRepository getReportRepository()
        {
            if (_reportRepository == null)
            {
                if (_nameValueCollection["storage"] == "json")
                    _reportRepository = new ReportJSONRepository(fileNameOfReport);
                //else другие источники данных
            }
            return _reportRepository;
        }
    }
}
