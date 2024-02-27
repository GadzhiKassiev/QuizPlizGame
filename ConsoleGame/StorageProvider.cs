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
        const string configStorageName = "storage";
        const string storageName = "json";
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

        public IQuestionRepository GetDataRepository()
        {
            string fullPath = Path.Combine(rootDirectory, fileNameOfData);
            if (_dataRepository == null)
            {
                try
                {
                    if (_nameValueCollection[configStorageName] == storageName)
                        _dataRepository = new QuestionJSONRepository(fullPath);
                    //else другие источники данных
                }
                catch 
                {
                    throw;
                }
            }
            return _dataRepository;
        }


        public IReportRepository GetReportRepository()
        {
            string fullPath = Path.Combine(rootDirectory, fileNameOfReport);
            if (_reportRepository == null)
            {
                try
                {
                    if (_nameValueCollection[configStorageName] == storageName)
                        _reportRepository = new ReportJSONRepository(fullPath);
                    //else другие источники данных
                }
                catch 
                {
                    throw;
                }
            }
            return _reportRepository;
        }
    }
}
