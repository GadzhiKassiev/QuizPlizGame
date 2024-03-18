using System;
using System.IO;
using System.Text.Json;

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
                throw new FileNotFoundException();
            }
        }

        public QuizQuestion[] Read()
        {
            return GetData().Quiz;
        }


        private QuizQuestions GetData()
        {
            QuizQuestions data = null;

            try
            {
                var json = File.ReadAllText(filepath);
                data = JsonSerializer.Deserialize<QuizQuestions>(json);
            }
            catch (FileNotFoundException e)
            {
                Environment.Exit(0);
            }

            return data;
        }
    }
}
