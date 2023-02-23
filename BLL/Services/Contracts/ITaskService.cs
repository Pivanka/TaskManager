using BLL.Dtos;
using BLL.Models;

namespace BLL.Services.Contracts
{
    public interface ITaskService
    {
        Task<TaskModel> AddTaskAsync(AddTaskDto task);
        //Task<int> UpdateTaskAsync(TaskDto task); //think about it
        Task<IEnumerable<TaskModel>> GetAllTasksAsync(int userId);
        Task<TaskDetailsModel> GetTaskByIdAsync(int id);
    }
}
