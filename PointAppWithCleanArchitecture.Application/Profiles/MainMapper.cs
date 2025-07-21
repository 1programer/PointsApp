using AutoMapper;
using PointAppWithCleanArchitecture.Application.DTOS;
using PointAppWithCleanArchitecture.Domain.Models;

namespace PointAppWithCleanArchitecture.Application.Profiles
{
    public class MainMapper : Profile
    {
        public MainMapper()
        {
            CreateMap<Item, ItemDto>();
            CreateMap<ItemDto, Item>();
            CreateMap<Point, PointDto>();
            CreateMap<PointDto, Point>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserSignUpDto>();
            CreateMap<UserSignUpDto, User>();
        }
    }
}
