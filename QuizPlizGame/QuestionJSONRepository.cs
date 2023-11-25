using System;
using System.Text.Json;
using System.IO;

namespace QuizPlizGame
{
    public class QuestionJSONRepository : IQuestionRepository
    {

        string filepath;

        public QuestionJSONRepository(string path)
        {
            filepath = path;
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public QuizPart[] Read()
        {
            return GetData().quiz;
        }


        private QuizBox GetData()
        {
            QuizBox data = null;

            try
            {
                var json = File.ReadAllText(filepath);
                data = JsonSerializer.Deserialize<QuizBox>(json);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }

            return data;
        }
    }
}
