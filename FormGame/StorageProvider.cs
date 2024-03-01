using System;
using System.Collections.Specialized;
using System.IO;
using QuizPlizGame;


namespace FormGame
{
    public class StorageProvider : IStorageProvider
    {
        const string FileNameOfData = "datawithJson.txt";
        const string FileNameOfReport = "report.txt";
        const string ConfigStorageName = "storage";
        const string StorageName = "json";
        string rootDirectory;

        IQuestionRepository dataRepository;
        IReportRepository reportRepository;
        NameValueCollection _nameValueCollection;


        public StorageProvider(NameValueCollection nvc)
        {
            rootDirectory = Path.GetFullPath(Path.Combine
                (AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            dataRepository = null;
            reportRepository = null;
            _nameValueCollection = nvc;
        }

        public IQuestionRepository GetDataRepository()
        {
            string fullPath = Path.Combine(rootDirectory, FileNameOfData);
            if (dataRepository == null)
            {
                try
                {
                    if (_nameValueCollection[ConfigStorageName] == StorageName)
                        dataRepository = new QuestionJSONRepository(fullPath);
                    //else другие источники данных
                }
                catch
                {
                    throw;
                }           
            }
            return dataRepository;
        }

        public IReportRepository GetReportRepository()
        {
            string fullPath = Path.Combine(rootDirectory, FileNameOfReport);
            if (reportRepository == null)
            {
                try
                {
                    if (_nameValueCollection[ConfigStorageName] == StorageName)
                        reportRepository = new ReportJSONRepository(fullPath);
                    //else другие источники данных
                }
                catch
                {
                    throw;
                }
            }
            return reportRepository;
        }
    }
}
