using QuizPlizGame;
using System;
using System.Collections.Specialized;
using System.IO;


namespace ConsoleGame
{
    public class StorageProvider : IStorageProvider
    {
        const string FileNameOfData = "datawithJson.txt";
        const string FileNameOfReport = "reportGame.txt";
        const string ConfigStorageName = "storage";
        const string StorageName = "json";
        readonly string rootDirectory;

        IQuestionRepository _dataRepository;
        IReportRepository _reportRepository;
        NameValueCollection _nameValueCollection;


        public StorageProvider(NameValueCollection nvc)
        {
            rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _dataRepository = null;
            _reportRepository = null;
            _nameValueCollection = nvc;

            switch (_nameValueCollection[ConfigStorageName])
            {
                case StorageName:
                    _dataRepository = new QuestionJSONRepository(Path.Combine(rootDirectory, FileNameOfData));
                    _reportRepository = new ReportJSONRepository(Path.Combine(rootDirectory, FileNameOfReport));
                    break;
                    //else другие источники данных
            }
            if (_dataRepository == null)
            {
                throw new NullReferenceException($"Неверно указано имя хранилища {ConfigStorageName} в конфиге");
            }
            if (_reportRepository == null)
            {
                throw new NullReferenceException($"Неверно указано имя хранилища {ConfigStorageName} в конфиге");
            }
        }

        public IQuestionRepository GetDataRepository()
        {
            return _dataRepository;
        }


        public IReportRepository GetReportRepository()
        {
            return _reportRepository;
        }
    }
}
