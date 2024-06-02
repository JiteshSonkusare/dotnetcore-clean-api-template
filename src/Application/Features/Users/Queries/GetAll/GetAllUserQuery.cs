using MediatR;
using Shared.Wrapper;
using Application.Features.Users.Dtos;

namespace Application.Features.Users.Queries.GetAll;

public record GetAllUserQuery : IRequest<Result<List<UserDto>>>;
