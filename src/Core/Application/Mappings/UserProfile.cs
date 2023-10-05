﻿using AutoMapper;
using Domain.Entities;
using Application.Features.Users.Queries.ViewModels;
using Application.Features.Users.Commands.UpsertUser;

namespace Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpsertUserCommand, User>().ReverseMap();
        CreateMap<UserViewModel, User>().ReverseMap();
    }
}
