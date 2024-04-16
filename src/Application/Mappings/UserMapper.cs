using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Features.Users.Queries.ViewModels;
using Application.Features.Users.Commands.UpsertUser;

namespace Application.Mappings;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UpsertUserCommand, User>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<Result<List<UserViewModel>>, List<UserViewModel>>().ReverseMap();
    }
}