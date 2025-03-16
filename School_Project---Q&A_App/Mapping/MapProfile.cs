using AutoMapper;
using School_Project___Q_A_App.DTOs;
using School_Project___Q_A_App.Models;

namespace School_Project___Q_A_App.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

        }
    }
}
