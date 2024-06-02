using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Features.Users.Dtos;
using Application.Features.Users.Commands;

namespace Application.Features.Users.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<CreateUserCommand, User>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<Result<List<UserDto>>, List<UserDto>>().ReverseMap();
    }
}