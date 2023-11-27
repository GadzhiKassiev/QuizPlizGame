using System;
using System.Text.Json.Serialization;

namespace QuizPlizGame
{
    public class Report
    {
        [JsonPropertyName("time")]
        public TimeSpan GameTime { get; set; }

        [JsonPropertyName("numberCorrectAnswer")]
        public int Number { get; set; }

        [JsonPropertyName("data")]
        public DateTime GameDate { get; set; }
    }
}
