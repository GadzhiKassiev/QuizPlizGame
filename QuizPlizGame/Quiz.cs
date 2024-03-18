using System.Text.Json.Serialization;


namespace QuizPlizGame
{
    public class QuizQuestions
    {
        [JsonPropertyName("quiz")]
        public QuizQuestion[] Quiz { get; set; }

        public QuizQuestions(QuizQuestion[] quiz)
        {
            this.Quiz = quiz;
        }
    }

    public class QuizQuestion
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("answer")]
        public QuizAnswer Answer { get; set; }

        [JsonPropertyName("correct")]
        public int Correct { get; set; }



        public QuizQuestion(string question, QuizAnswer answer, int correct)
        {
            this.Question = question;
            this.Answer = answer;
            this.Correct = correct;
        }
    }

    public class QuizAnswer
    {
        /// <summary>
        /// вариант ответа 1
        /// </summary>
        [JsonPropertyName("a1")]
        public string A1 { get; set; }
        /// <summary>
        /// вариант ответа 2
        /// </summary>
        [JsonPropertyName("a2")]
        public string A2 { get; set; }
        /// <summary>
        /// вариант ответа 3
        /// </summary>
        [JsonPropertyName("a3")]
        public string A3 { get; set; }
        /// <summary>
        /// вариант ответа 4
        /// </summary>
        [JsonPropertyName("a4")]
        public string A4 { get; set; }


        public QuizAnswer(string a1, string a2, string a3, string a4)
        {
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
        }
    }
}

