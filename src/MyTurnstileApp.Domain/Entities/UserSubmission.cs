namespace MyTurnstileApp.Domain.Entities
{
    public class UserSubmission
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TurnstileResponse { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
