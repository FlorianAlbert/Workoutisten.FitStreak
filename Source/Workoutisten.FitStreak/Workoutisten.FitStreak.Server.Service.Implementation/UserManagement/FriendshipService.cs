using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Implementation.Extension;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class FriendshipService : IFriendshipService
{
    private IRepository Repository { get; }

    public FriendshipService(IRepository repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result> AcceptFriendshipRequestAsync(Guid requestedUserId, Guid friendshipRequestId)
    {
        try
        {
            var friendshipRequest = await Repository.GetAsync<FriendshipRequest>(friendshipRequestId);
            if (friendshipRequest is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no friendshipRequest with the id {friendshipRequestId}."
                };
            }

            var requestingUser = friendshipRequest.RequestingUser;
            var requestedUser = friendshipRequest.RequestedUser;

            if (requestedUserId != requestedUser.Id)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"Your are not authorized to accept the friendshipRequest with the id {friendshipRequestId}."
                };
            }

            requestingUser.MyFriends.Add(requestedUser);
            requestedUser.UsersIAmFriendOf.Add(requestingUser);

            await Repository.CreateOrUpdateAsync(requestingUser);
            await Repository.CreateOrUpdateAsync(requestedUser);
            await Repository.DeleteAsync(friendshipRequest);
            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
        catch (EntryNotFoundException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"The friendshipRequest couldn't be deleted because while deleting there was no request with the id  {friendshipRequestId}."
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result> CreateFriendshipRequestAsync(Guid requestingUserId, string requestedUserEmail)
    {
        try
        {
            var requestingUser = await Repository.GetAsync<User>(requestingUserId);
            var requestedUser = (await Repository.GetAllAsync<User>())
                .FirstOrDefault(user => user.NormalizedEmail == requestedUserEmail.NormalizeEmail());

            if (requestingUser is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {requestingUserId}."
                };
            }

            if (requestedUser is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the email {requestedUserEmail}."
                };
            }

            var friendshipRequest = new FriendshipRequest
            {
                RequestingUser = requestingUser,
                RequestedUser = requestedUser
            };

            await Repository.CreateOrUpdateAsync(friendshipRequest);

            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result> DeclineFriendshipRequestAsync(Guid requestedUserId, Guid friendshipRequestId)
    {
        try
        {
            var friendshipRequest = await Repository.GetAsync<FriendshipRequest>(friendshipRequestId);
            if (friendshipRequest is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no friendshipRequest with the id {friendshipRequestId}."
                };
            }

            var requestedUser = friendshipRequest.RequestedUser;
            if (requestedUserId != requestedUser.Id)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"Your are not authorized to decline the friendshipRequest with the id {friendshipRequestId}."
                };
            }

            await Repository.DeleteAsync(friendshipRequest);
            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
        catch (EntryNotFoundException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"The friendshipRequest couldn't be deleted because while deleting there was no request with the id  {friendshipRequestId}."
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result> DeleteFollowerAsync(Guid userId, Guid followerId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<User>>> GetFollowedUsersAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<User>>> GetFollowerAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<FriendshipRequest>>> GetIncomingFriendshipRequestsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<FriendshipRequest>>> GetOutgoingFriendshipRequestsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
    public async Task<Result> UnfollowUserAsync(Guid userId, Guid followedUserId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> WithdrawFriendshipRequestAsync(Guid requestingUserId, Guid friendshipRequestId)
    {
        try
        {
            var friendshipRequest = await Repository.GetAsync<FriendshipRequest>(friendshipRequestId);
            if (friendshipRequest is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no friendshipRequest with the id {friendshipRequestId}."
                };
            }

            var requestingUser = friendshipRequest.RequestingUser;
            if (requestingUserId != requestingUser.Id)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"Your are not authorized to withdraw the friendshipRequest with the id {friendshipRequestId}."
                };
            }

            await Repository.DeleteAsync(friendshipRequest);
            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
        catch (EntryNotFoundException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"The friendshipRequest couldn't be deleted because while deleting there was no request with the id  {friendshipRequestId}."
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }
}
