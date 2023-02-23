namespace DAL.Models
{
    public class Comment : BaseEntity
    {
        public Task Task { get; set; }
        public int TaskId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
