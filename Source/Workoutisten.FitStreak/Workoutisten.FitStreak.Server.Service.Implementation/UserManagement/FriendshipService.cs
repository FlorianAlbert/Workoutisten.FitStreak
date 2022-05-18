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

    public async Task<Result> AcceptFriendshipRequestAsync(Guid userId, Guid friendshipRequestId)
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

            if (userId != requestedUser.Id)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"Your are not authorized to accept the friendshipRequest with the id {friendshipRequestId}."
                };
            }

            requestingUser.MyFriends.Add(requestedUser);

            await Repository.CreateOrUpdateAsync(requestingUser);
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

    public async Task<Result> CreateFriendshipRequestAsync(Guid userId, string requestedUserEmail)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var requestedUser = (await Repository.GetAllAsync<User>())
                .FirstOrDefault(user => user.NormalizedEmail == requestedUserEmail.NormalizeEmail());
            if (requestedUser is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the email {requestedUserEmail}."
                };
            }

            if (user.OutgoingFriendshipRequests.Any(request => request.RequestedUser == requestedUser))
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Detail = $"Your already created a friendshipRequest for {requestedUserEmail}."
                };
            }

            if (user.MyFriends.Contains(requestedUser))
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Detail = $"Your are already a friend of {requestedUserEmail}."
                };
            }

            var friendshipRequest = new FriendshipRequest
            {
                RequestingUser = user,
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

    public async Task<Result> DeclineFriendshipRequestAsync(Guid userId, Guid friendshipRequestId)
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
            if (userId != requestedUser.Id)
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
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var follower = await Repository.GetAsync<User>(followerId);
            if (follower is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {followerId}."
                };
            }

            var removedFollower = user.UsersIAmFriendOf.Remove(follower);
            if (!removedFollower)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = "The follower couldn't be removed. Maybe the user wasn't a follower."
                };
            }

            await Repository.CreateOrUpdateAsync(user);
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

    public async Task<Result<IEnumerable<User>>> GetFollowedUsersAsync(Guid userId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<IEnumerable<User>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            return new Result<IEnumerable<User>>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = user.MyFriends
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<IEnumerable<User>>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<IEnumerable<User>>> GetFollowerAsync(Guid userId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<IEnumerable<User>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }            

            return new Result<IEnumerable<User>>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = user.UsersIAmFriendOf
        };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<IEnumerable<User>>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<IEnumerable<FriendshipRequest>>> GetIncomingFriendshipRequestsAsync(Guid userId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<IEnumerable<FriendshipRequest>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var incomingFriendshipRequests = (await Repository.GetAllAsync<FriendshipRequest>())
                .Where(request => request.RequestedUserId == userId);

            return new Result<IEnumerable<FriendshipRequest>>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = incomingFriendshipRequests
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<IEnumerable<FriendshipRequest>>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<IEnumerable<FriendshipRequest>>> GetOutgoingFriendshipRequestsAsync(Guid userId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<IEnumerable<FriendshipRequest>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var outgoingFriendshipRequests = (await Repository.GetAllAsync<FriendshipRequest>())
                .Where(request => request.RequestingUserId == userId);

            return new Result<IEnumerable<FriendshipRequest>>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = outgoingFriendshipRequests
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<IEnumerable<FriendshipRequest>>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }
    public async Task<Result> UnfollowUserAsync(Guid userId, Guid followedUserId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var followedUser = await Repository.GetAsync<User>(followedUserId);
            if (followedUser is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {followedUserId}."
                };
            }

            var removedFollowedUser = user.MyFriends.Remove(followedUser);
            if (!removedFollowedUser)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = "The followedUser couldn't be removed. Maybe the user wasn't a followedUser."
                };
            }

            await Repository.CreateOrUpdateAsync(user);
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

    public async Task<Result> WithdrawFriendshipRequestAsync(Guid userId, Guid friendshipRequestId)
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
            if (userId != requestingUser.Id)
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
