using BLL.Dtos;
using BLL.Models;

namespace BLL.Services.Contracts
{
    public interface ICommentService
    {
        Task<CommentModel> AddCommentAsync(CommentDto comment);
        Task<IEnumerable<CommentModel>> GetAllCommentsAsync(int taskId);
        Task<CommentModel> GetCommentByIdAsync(int id);
    }
}
