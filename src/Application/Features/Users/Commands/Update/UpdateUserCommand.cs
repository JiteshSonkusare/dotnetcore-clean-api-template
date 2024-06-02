﻿using MediatR;
using Domain.Enums;
using Shared.Wrapper;
using Domain.Entities;

namespace Application.Features.Users.Commands;

public record UpdateUserCommand(
	Guid Id,
	string? FirstName,
	string? LastName,
	string? UserId,
	int? Mobile,
	int? Phone,
	string? Address,
	string? Gender,
	UserStatus? Status) : IRequest<Result<Guid>>;

public record UpdateUserRequest(
	Guid Id,
	string? FirstName,
	string? LastName,
	int? Mobile,
	int? Phone,
	string? Address,
	string? Gender,
	UserStatus? Status);