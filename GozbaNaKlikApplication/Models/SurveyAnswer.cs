namespace GozbaNaKlikApplication.Models
{
    public class SurveyAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
