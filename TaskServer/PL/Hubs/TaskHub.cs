using BLL.Dtos;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PL.Hubs
{
    [Authorize]
    public class TaskHub : Hub
    {
        private readonly ITaskService _taskService;
        public TaskHub(ITaskService taskService)
        {
            _taskService = taskService;
        }
        public async Task AddTask(AddTaskDto task)
        {
            var newTask = await _taskService.AddTaskAsync(task);

            // Відправляємо повідомлення про додавання нового завдання всім клієнтам
            await Clients.All.SendAsync("TaskAdded", newTask);
        }
    }
}
