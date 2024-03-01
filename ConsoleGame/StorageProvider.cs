using System;
using System.Collections.Specialized;
using System.IO;
using QuizPlizGame;


namespace ConsoleGame
{
    public class StorageProvider : IStorageProvider
    {
        const string FileNameOfData = "datawithJson.txt";
        const string FileNameOfReport = "report.txt";
        const string ConfigStorageName = "storage";
        const string StorageName = "json";
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
            string fullPath = Path.Combine(rootDirectory, FileNameOfData);
            if (_dataRepository == null)
            {
                try
                {
                    if (_nameValueCollection[ConfigStorageName] == StorageName)
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
            string fullPath = Path.Combine(rootDirectory, FileNameOfReport);
            if (_reportRepository == null)
            {
                try
                {
                    if (_nameValueCollection[ConfigStorageName] == StorageName)
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
