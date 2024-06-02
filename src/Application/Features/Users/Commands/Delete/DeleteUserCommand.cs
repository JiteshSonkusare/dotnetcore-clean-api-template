using MediatR;
using Shared.Wrapper;
using Domain.Entities;

namespace Application.Features.Users.Commands;

public record DeleteUserCommand(Guid Id) : IRequest<Result<Guid>>;