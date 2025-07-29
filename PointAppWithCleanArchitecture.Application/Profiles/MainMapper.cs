using AutoMapper;
using PointAppWithCleanArchitecture.Application.DTOS;
using PointAppWithCleanArchitecture.Domain.Models;

namespace PointAppWithCleanArchitecture.Application.Profiles
{
    public class MainMapper : Profile
    {
        public MainMapper()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Point, PointDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserSignUpDto>().ReverseMap();
        }
    }
}
