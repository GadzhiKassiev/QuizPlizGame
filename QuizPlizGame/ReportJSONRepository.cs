using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

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
            var options = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filepath, json);
        }

        private List<Report> GetData()
        {
            List<Report> data = new List<Report>();

            if (new FileInfo(filepath).Length != 0)
            {
                var json = File.ReadAllText(filepath);
                data = JsonSerializer.Deserialize<List<Report>>(json);
            }

            return data;
        }
    }
}
