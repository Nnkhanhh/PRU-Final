using System.ComponentModel.DataAnnotations;

namespace QuizzGameAPI.Models
{
    public class QuizQuestion
    {
        [Key] 
        public int QuestionId { get; set; }

        public int MapLevel { get; set; }
        public string QuestionType { get; set; } = "";
        public string QuestionText { get; set; } = "";
        public string OptionA { get; set; } = "";
        public string OptionB { get; set; } = "";
        public string OptionC { get; set; } = "";
        public string OptionD { get; set; } = "";
        public string CorrectOption { get; set; } = "";
        public string? OriginalVerb { get; set; }
        public string? VerbTense { get; set; }
    }
}
