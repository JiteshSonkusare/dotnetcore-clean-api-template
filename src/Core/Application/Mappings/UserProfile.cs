using AutoMapper;
using Domain.Entities;
using Application.Features.Users.Queries.ViewModels;
using Application.Features.Users.Commands.UpsertUser;
using Shared.Wrapper;

namespace Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpsertUserCommand, User>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<Result<List<UserViewModel>>, List<UserViewModel>>().ReverseMap();
    }
}
