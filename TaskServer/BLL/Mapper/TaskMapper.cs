using AutoMapper;
using BLL.Dtos;
using BLL.Models;

namespace BLL.Mapper
{
    public class TaskMapper : Profile
    {
        public TaskMapper()
        {
            CreateMap<AddTaskDto, DAL.Models.Task>().ReverseMap();
            CreateMap<DAL.Models.Task, TaskModel>().ReverseMap();
            CreateMap<DAL.Models.Task, TaskDetailsModel>().ReverseMap();
        }
    }
}
