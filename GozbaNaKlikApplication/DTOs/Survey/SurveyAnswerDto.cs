using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlikApplication.DTOs.Survey
{
    public class SurveyAnswerDto
    {
        public int QuestionId { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
