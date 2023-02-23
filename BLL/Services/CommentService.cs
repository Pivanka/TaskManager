using AutoMapper;
using BLL.Dtos;
using BLL.Models;
using BLL.Services.Contracts;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<DAL.Models.Comment> _commentRepository;
        private readonly IRepository<DAL.Models.Task> _tasktRepository;
        private readonly IMapper _mapper;
        public CommentService(IRepository<DAL.Models.Comment> commentRepository, 
            IMapper mapper, 
            IRepository<DAL.Models.Task> tasktRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _tasktRepository = tasktRepository;
        }
        public async Task<CommentModel> AddCommentAsync(CommentDto comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));

            if (await _tasktRepository.FirstOrDefaultAsync(t => t.Id == comment.TaskId) == null)
                throw new ArgumentException();

            var commentToAdd = _mapper.Map<DAL.Models.Comment>(comment);

            commentToAdd.CreatedAt = DateTime.Now;

            var addedComment = await _commentRepository.AddAsync(commentToAdd);
            await _commentRepository.SaveChangesAsync();

            return _mapper.Map<CommentModel>(addedComment);
        }

        public async Task<IEnumerable<CommentModel>> GetAllCommentsAsync(int taskId)
        {
            var comments = await _commentRepository.Query(c => c.TaskId == taskId).ToListAsync();

            return _mapper.ProjectTo<CommentModel>(comments.AsQueryable());
        }

        public async Task<CommentModel> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null) throw new ArgumentException();

            return _mapper.Map<CommentModel>(comment);
        }
    }
}
