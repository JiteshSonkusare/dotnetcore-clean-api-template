using MediatR;
using Shared.Wrapper;
using Application.Features.Users.Dtos;

namespace Application.Features.Users.Queries;

public record GetUserByAPIQuery : IRequest<Result<List<UserDto>>>;