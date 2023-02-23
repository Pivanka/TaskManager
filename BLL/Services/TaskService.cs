using AutoMapper;
using BLL.Dtos;
using BLL.Models;
using BLL.Services.Contracts;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<DAL.Models.Task> _taskRepository;
        private readonly IMapper _mapper;
        public TaskService(IRepository<DAL.Models.Task> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<TaskModel> AddTaskAsync(AddTaskDto task)
        {
            if(task == null) throw new ArgumentNullException(nameof(task));

            var taskToAdd = _mapper.Map<DAL.Models.Task>(task);

            taskToAdd.CreatedAt = DateTime.Now;

            var addedTask = await _taskRepository.AddAsync(taskToAdd);
            await _taskRepository.SaveChangesAsync();

            return _mapper.Map<TaskModel>(addedTask);
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync(int userId)
        {
            var tasks = await _taskRepository.Query(t => t.UserId == userId).ToListAsync();

            return _mapper.ProjectTo<TaskModel>(tasks.AsQueryable());
        }

        public async Task<TaskDetailsModel> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.Query()
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null) throw new ArgumentException();

            return _mapper.Map<TaskDetailsModel>(task);
        }
    }
}
