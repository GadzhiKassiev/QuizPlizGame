using System;
using System.Collections.Specialized;
using System.IO;
using QuizPlizGame;


namespace ConsoleGame
{
    public class StorageProvider : IStorageProvider
    {
        const string fileNameOfData = "datawithJson.txt";
        const string fileNameOfReport = "report.txt";
        string storageName = "json";
        string rootDirectory;

        IQuestionRepository _dataRepository;
        IReportRepository _reportRepository;
        NameValueCollection _nameValueCollection;


        public StorageProvider(NameValueCollection nvc)
        {
            rootDirectory = Path.GetFullPath(Path.Combine
                (AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            _dataRepository = null;
            _reportRepository = null;
            _nameValueCollection = nvc;
        }

        public IQuestionRepository getDataRepository()
        {
            string fullPath = Path.Combine(rootDirectory, fileNameOfData);
            if (_dataRepository == null)
            {
                if (_nameValueCollection["storage"] == storageName)
                    _dataRepository = new QuestionJSONRepository(fullPath);
                //else другие источники данных
            }
            return _dataRepository;
        }


        public IReportRepository getReportRepository()
        {
            string fullPath = Path.Combine(rootDirectory, fileNameOfReport);
            if (_reportRepository == null)
            {
                if (_nameValueCollection["storage"] == storageName)
                    _reportRepository = new ReportJSONRepository(fullPath);
                //else другие источники данных
            }
            return _reportRepository;
        }
    }
}
