using System;
using System.Collections.Specialized;
using System.IO;
using QuizPlizGame;


namespace FormGame
{
    public class StorageProvider : IStorageProvider
    {
        const string fileNameOfData = "datawithJson.txt";
        const string fileNameOfReport = "report.txt";
        const string configStorageName = "storage";
        string storageName = "json";
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
            string fullPath = Path.Combine(rootDirectory, fileNameOfData);
            if (dataRepository == null)
            {
                try
                {
                    if (_nameValueCollection[configStorageName] == storageName)
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
            string fullPath = Path.Combine(rootDirectory, fileNameOfReport);
            if (reportRepository == null)
            {
                try
                {
                    if (_nameValueCollection[configStorageName] == storageName)
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
