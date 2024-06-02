using MediatR;
using Shared.Wrapper;
using Application.Features.Users.Dtos;

namespace Application.Features.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserDto>>;