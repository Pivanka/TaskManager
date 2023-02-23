namespace BLL.Models
{
    public class TaskDetailsModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }

        public ICollection<CommentModel>? Comments { get; set; }
    }
}
