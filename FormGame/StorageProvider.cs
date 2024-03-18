using QuizPlizGame;
using System;
using System.Collections.Specialized;
using System.IO;


namespace FormGame
{
    public class StorageProvider : IStorageProvider
    {
        const string FileNameOfData = "datawithJson.txt";
        const string FileNameOfReport = "reportGame.txt";
        const string ConfigStorageName = "storage";
        const string StorageName = "json";
        string rootDirectory;

        IQuestionRepository dataRepository;
        IReportRepository reportRepository;
        NameValueCollection _nameValueCollection;


        public StorageProvider(NameValueCollection nvc)
        {
            rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dataRepository = null;
            reportRepository = null;
            _nameValueCollection = nvc;

            switch (_nameValueCollection[ConfigStorageName])
            {
                case StorageName:
                    dataRepository = new QuestionJSONRepository(Path.Combine(rootDirectory, FileNameOfData));
                    reportRepository = new ReportJSONRepository(Path.Combine(rootDirectory, FileNameOfReport));
                    break;
                    //else другие источники данных
            }
            if (dataRepository == null)
            {
                throw new NullReferenceException($"Неверно указано имя хранилища {ConfigStorageName} в конфиге");
            }
            if (reportRepository == null)
            {
                throw new NullReferenceException($"Неверно указано имя хранилища {ConfigStorageName} в конфиге");
            }
        }

        public IQuestionRepository GetDataRepository()
        {
            return dataRepository;
        }

        public IReportRepository GetReportRepository()
        {
            return reportRepository;
        }
    }
}
