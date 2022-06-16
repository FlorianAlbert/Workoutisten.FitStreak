using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training;

public class ExerciseService : IExerciseService
{
    private IRepository Repository { get; }

    public ExerciseService(IRepository repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<Exercise>> CreateExercise(Guid userId, string name, string description, ExerciseCategory exerciseCategory)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var exercise = new Exercise
            {
                Name = name,
                Description = description,
                Category = exerciseCategory,
                Creator = user,
            };

            var savedExercise = await Repository.CreateOrUpdateAsync(exercise);

            return new Result<Exercise>
            {
                StatusCode = StatusCodes.Status201Created,
                Value = savedExercise
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<Exercise>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result> DeleteExercise(Guid userId, Guid exerciseId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var exercise = await Repository.GetAsync<Exercise>(exerciseId);
            if (exercise is null)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no exercise with the id {exerciseId}."
                };
            }

            if (exercise.Creator != user)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to delete the exercise with the id {exerciseId} because you are not the creator!"
                };
            }

            await Repository.DeleteAsync(exercise);

            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
        catch (EntryNotFoundException) // TODO: Repository doesn't throw EntryNotFoundException, just throws a DatabaseRepositoryException with a nested EntryNotFoundException
        {
            return new Result
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"The exercise couldn't be deleted because while deleting there was no exercise with the id  {exerciseId}."
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

    public async Task<Result<Exercise>> GetExercise(Guid userId, Guid exerciseId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var exercise = await Repository.GetAsync<Exercise>(exerciseId);
            if (exercise is null)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no exercise with the id {exerciseId}."
                };
            }

            if (exercise.Creator != user)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to get the exercise with the id {exerciseId} because you are not the creator!"
                };
            }

            return new Result<Exercise>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = exercise
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<Exercise>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<IEnumerable<Exercise>>> GetExercises(Guid userId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<IEnumerable<Exercise>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var exercises = await Repository.GetAllAsync<Exercise>();
            var exercisesOfUser = exercises
                .Where(e => e.Creator == user);

            return new Result<IEnumerable<Exercise>>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = exercisesOfUser
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<IEnumerable<Exercise>>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<Exercise>> UpdateExercise(Guid userId, Guid exerciseId, string? name, string? description, ExerciseCategory exerciseCategory)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var exercise = await Repository.GetAsync<Exercise>(exerciseId);
            if (exercise is null)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no exercise with the id {exerciseId}."
                };
            }

            if (exercise.Creator != user)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to update the exercise with the id {exerciseId} because you are not the creator!"
                };
            }

            exercise.Name = name ?? exercise.Name;
            exercise.Description = description ?? exercise.Description;
            exercise.Category = exerciseCategory;

            var savedExercise = await Repository.CreateOrUpdateAsync(exercise);

            return new Result<Exercise>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = savedExercise
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<Exercise>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }
}
