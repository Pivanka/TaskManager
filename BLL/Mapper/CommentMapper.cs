using AutoMapper;
using BLL.Dtos;
using BLL.Models;
using DAL.Models;

namespace BLL.Mapper
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentModel>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
