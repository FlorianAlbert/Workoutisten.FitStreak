using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Model.Workout;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training;

public class ExerciseGroupService : IExerciseGroupService
{
    public ExerciseGroupService(IRepository repo)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    private IRepository Repo { get; }

    public async Task<Result<ExerciseGroup>> CreateNewExerciseGroup(Guid userId, Guid? workoutId = null)
    {
        try
        {
            var user = await Repo.GetAsync<User>(userId);
            if (user is null) return new Result<ExerciseGroup>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"There does not exist a User with Id={userId}."
            };

            var newExerciseGroup = new ExerciseGroup();
            newExerciseGroup.Participants.Add(user);

            if (workoutId.HasValue)
            {
                var workout = await Repo.GetAsync<Workout>(workoutId.Value);
                if (workout is null) return new Result<ExerciseGroup>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There does not exist a Workout with Id={workoutId.Value}."
                };

                if (workout.Creator != user) return new Result<ExerciseGroup>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to create an ExerciseGroup based on the Workout with the id {workoutId.Value} because you are not the creator!"
                };

                newExerciseGroup.Workout = workout;
            }

            var createdEntity = await Repo.CreateOrUpdateAsync(newExerciseGroup);

            return new Result<ExerciseGroup>
            {
                StatusCode = StatusCodes.Status201Created,
                Value = createdEntity
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<ExerciseGroup>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result> DeleteExerciseGroup(Guid userId, Guid exerciseGroupId)
    {
        try
        {
            var exerciseGroup = await Repo.GetAsync<ExerciseGroup>(exerciseGroupId);
            if (exerciseGroup is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no ExerciseGroup with the id {exerciseGroup}."
                };
            }

            if (exerciseGroup.Participants.All(x => x.Id != userId))
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to delete the ExerciseGroup with the id {exerciseGroupId} because you are not a participant!"
                };
            }

            await Repo.DeleteAsync(exerciseGroup);

            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
        catch (DatabaseRepositoryException ex)
        {
            if (ex.InnerException is EntryNotFoundException) return new Result
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"The ExerciseGroup couldn't be deleted because while deleting there was no ExerciseGroup with the id {exerciseGroupId}."
            };

            return new Result
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<IEnumerable<ExerciseGroup>>> GetAllExerciseGroupsForUser(Guid userId)
    {
        try
        {
            var exerciseGroups = await Repo.GetAllAsync<ExerciseGroup>(x => x.Participants.Any(x => x.Id == userId));

            return new Result<IEnumerable<ExerciseGroup>>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = exerciseGroups
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<IEnumerable<ExerciseGroup>>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<ExerciseGroup>> GetExerciseGroupForUser(Guid userId, Guid exerciseGroupId)
    {
        try
        {
            var exerciseGroup = await Repo.GetAsync<ExerciseGroup>(exerciseGroupId);
            if (exerciseGroup is null)
            {
                return new Result<ExerciseGroup>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no ExerciseGroup with the id {exerciseGroupId}."
                };
            }

            if (exerciseGroup.Participants.All(x => x.Id != userId))
            {
                return new Result<ExerciseGroup>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to get the ExerciseGroup with the id {exerciseGroupId} because you are not a participant!"
                };
            }

            return new Result<ExerciseGroup>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = exerciseGroup
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<ExerciseGroup>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }
}
