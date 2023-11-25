using System;
using System.Text.Json.Serialization;

namespace QuizPlizGame
{
    public class Report
    {
        [JsonPropertyName("time")]
        public TimeSpan Time { get; set; }

        [JsonPropertyName("numberCorrectAnswer")]
        public int Number { get; set; }

        [JsonPropertyName("data")]
        public DateTime Data { get; set; }
    }
}
