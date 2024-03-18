using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace QuizPlizGame
{
    public class ReportJSONRepository : IReportRepository
    {

        string filepath;

        public ReportJSONRepository(string path)
        {
            filepath = path;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
        }

        public Report[] Read()
        {
            return GetData().OrderByDescending(n => n.Number).ToArray();
        }

        public void Write(Report unit)
        {
            string json;
            List<Report> data = GetData();
            data.Add(unit);
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(filepath, json);
            }
            catch
            {
                throw;
            }
        }

        private List<Report> GetData()
        {
            List<Report> data = new List<Report>();

            if (new FileInfo(filepath).Length != 0)
            {
                try
                {
                    var json = File.ReadAllText(filepath);
                    data = JsonSerializer.Deserialize<List<Report>>(json);
                }
                catch
                {
                    throw;
                }
            }

            return data;
        }
    }
}
