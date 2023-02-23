namespace DAL.Models
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
