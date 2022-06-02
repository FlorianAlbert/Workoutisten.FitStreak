using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Model.Workout;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training;
public class DoneExerciseService : IDoneExerciseService
{
    public DoneExerciseService(IRepository repo)
    {
        Repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    private IRepository Repo { get; }

    public async Task<Result<DoneExercise>> CreateDoneExercise(Guid userId, Guid exerciseId, Guid? exerciseGroupId = null)
    {
        try
        {
            var user = await Repo.GetAsync<User>(userId);
            if (user is null) return new Result<DoneExercise>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"There does not exist a User with Id={userId}."
            };

            var exercise = await Repo.GetAsync<Exercise>(exerciseId);
            if (exercise is null) return new Result<DoneExercise>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"There does not exist an Exercise with Id={exerciseId}."
            };

            if (exercise.Creator != user) return new Result<DoneExercise>
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Detail = $"You are not authorized to create the create a DoneExercise based on the Exercise with the id {exerciseId} because you are not the creator!"
            };

            var newDoneExercise = new DoneExercise
            {
                Exercise = exercise,
                ExercisingUser = user
            };

            if (exerciseGroupId.HasValue)
            {
                var exerciseGroup = await Repo.GetAsync<ExerciseGroup>(exerciseGroupId.Value);

                if (exerciseGroup is null) return new Result<DoneExercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There does not exist an ExerciseGroup with Id={exerciseGroupId.Value}."
                }; 
                
                if (exercise.Creator.Id != userId) return new Result<DoneExercise>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to delete the create a DoneExercise in the context of the ExerciseGroup with the id {exerciseGroupId} because you are not the creator!"
                };

                newDoneExercise.ExerciseGroup = exerciseGroup;
            }

            var createdEntity = await Repo.CreateOrUpdateAsync(newDoneExercise);

            return new Result<DoneExercise>
            {
                StatusCode = StatusCodes.Status201Created,
                Value = createdEntity
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<DoneExercise>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result> DeleteDoneExercise(Guid userId, Guid doneExerciseId)
    {
        try
        {
            var user = await Repo.GetAsync<User>(userId);
            if (user is null) return new Result<DoneExercise>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"There does not exist a User with Id={userId}."
            };

            var doneExercise = await Repo.GetAsync<DoneExercise>(doneExerciseId);
            if (doneExercise is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no DoneExercise with the id { doneExerciseId }."
                };
            }

            if (doneExercise.ExercisingUser != user)
            {
                return new Result<Exercise>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to delete the exercise with the id {doneExerciseId} because you are not the creator!"
                };
            }

            await Repo.DeleteAsync(doneExercise);

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
                Detail = $"The DoneExercise couldn't be deleted because while deleting there was no DoneExercise with the id { doneExerciseId }."
            };

            return new Result
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<IEnumerable<DoneExercise>>> GetAllDoneExercisesForUser(Guid userId)
    {
        try
        {
            var doneExercises = await Repo.GetAllAsync<DoneExercise>(x => x.ExercisingUser.Id == userId);

            return new Result<IEnumerable<DoneExercise>>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = doneExercises
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<IEnumerable<DoneExercise>>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<DoneExercise>> GetDoneExerciseForUser(Guid userId, Guid doneExerciseId)
    {
        try
        {
            var doneExercise = await Repo.GetAsync<DoneExercise>(doneExerciseId);
            if (doneExercise is null)
            {
                return new Result<DoneExercise>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no DoneExercise with the id { doneExerciseId }."
                };
            }

            if (doneExercise.ExercisingUser.Id != userId)
            {
                return new Result<DoneExercise>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to get the DoneExercise with the id { doneExerciseId } because you are not the creator!"
                };
            }

            return new Result<DoneExercise>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = doneExercise
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<DoneExercise>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }
}
