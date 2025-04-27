using AutoMapper;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;

namespace School_Project___Q_A_App.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Comment, CommentDto>();

        }
    }
}
