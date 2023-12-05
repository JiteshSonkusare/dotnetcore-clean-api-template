using MediatR;
using LazyCache;
using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Resources.Constants;
using Application.Interfaces.Repositories;
using Application.Features.Users.Queries.ViewModels;
using Application.Common.Exceptions;

namespace Application.Features.Users.Queries.GetAll;

public class GetAllUserQuery : IRequest<Result<List<UserViewModel>>>
{
    public GetAllUserQuery() { }
}

internal class GetUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserViewModel>>>
{
    private readonly IUnitOfWork<Guid> _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _cache;

    public GetUserQueryHandler(IMapper mapper, IUnitOfWork<Guid> unitOfWork, IAppCache cache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<List<UserViewModel>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            Task<List<User>> getAllUsers() => _unitOfWork.Repository<User>().GetAllAsync();
            var users = await _cache.GetOrAddAsync(CacheConstants.UsersCacheKey, getAllUsers);
            if (users.Count == 0)
                return await Result<List<UserViewModel>>.FailAsync("No users found!");
            var mappedUser = _mapper.Map<List<UserViewModel>>(users);
            return await Result<List<UserViewModel>>.SuccessAsync(mappedUser);
        }
        catch (Exception ex)
        {
            throw new GeneralApplicationException($"Failed to get user data! Error: {ex.Message}");
        }
    }
}