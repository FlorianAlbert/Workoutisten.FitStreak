using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Model.Workout;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training;

public class WorkoutService : IWorkoutService
{
    private IRepository Repository { get; }

    public WorkoutService(IRepository repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<Workout>> CreateWorkout(Guid userId, string name, string description, IEnumerable<Guid> exerciseIds)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var workout = new Workout
            {
                Name = name,
                Creator = user,
                Description = description
            };

            var exercises = await Repository.GetAllAsync<Exercise>();
            var exercisesToAdd = exercises
                .Where(e => exerciseIds.Contains(e.Id));
            if (exerciseIds.Count() != exercisesToAdd.Count())
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"Not all exercises which should be added to the workout existed!"
                };
            }

            foreach (var exercise in exercisesToAdd)
            {
                workout.WorkoutExercises.Add(new WorkoutExercise
                {
                    Workout = workout,
                    Exercise = exercise
                });
            }

            var savedWorkout = await Repository.CreateOrUpdateAsync(workout);

            return new Result<Workout>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = savedWorkout
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<Workout>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result> DeleteWorkout(Guid userId, Guid workoutId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var workout = await Repository.GetAsync<Workout>(workoutId);
            if (workout is null)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no workout with the id {workoutId}."
                };
            }

            if (workout.Creator != user)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to delete the workout with the id {workout} because you are not the creator!"
                };
            }

            await Repository.DeleteAsync(workout);

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
                Detail = $"The workout couldn't be deleted because while deleting there was no workout with the id {workoutId}."
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

    public async Task<Result<Workout>> GetWorkout(Guid userId, Guid workoutId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var workout = await Repository.GetAsync<Workout>(workoutId);
            if (workout is null)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no workout with the id {workoutId}."
                };
            }

            if (workout.Creator != user)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to get the workout with the id {workoutId} because you are not the creator!"
                };
            }

            return new Result<Workout>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = workout
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<Workout>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<IEnumerable<Workout>>> GetWorkouts(Guid userId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<IEnumerable<Workout>>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var workouts = await Repository.GetAllAsync<Workout>();
            var workoutsOfUser = workouts
                .Where(e => e.Creator == user);

            return new Result<IEnumerable<Workout>>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = workoutsOfUser
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<IEnumerable<Workout>>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<Workout>> UpdateWorkout(Guid userId, Guid workoutId, string? name, string? description, IEnumerable<Guid>? exerciseIds)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no user with the id {userId}."
                };
            }

            var workout = await Repository.GetAsync<Workout>(workoutId);
            if (workout is null)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no workout with the id {workoutId}."
                };
            }

            if (workout.Creator != user)
            {
                return new Result<Workout>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to update the workout with the id {workoutId} because you are not the creator!"
                };
            }


            workout.Name = name ?? workout.Name;
            workout.Description = description ?? workout.Description;
            if (exerciseIds is not null)
            {
                var exercises = await Repository.GetAllAsync<Exercise>();
                var exercisesToAdd = exercises
                    .Where(e => exerciseIds.Contains(e.Id));
                if(exerciseIds.Count() != exercisesToAdd.Count())
                {
                    return new Result<Workout>
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Detail = $"Not all exercises which should be added to the workout existed!"
                    };
                }

                workout.WorkoutExercises.Clear();
                foreach(var exercise in exercisesToAdd)
                {
                    workout.WorkoutExercises.Add(new WorkoutExercise
                    {
                        Workout = workout,
                        Exercise = exercise
                    });
                }
            }

            var savedWorkout = await Repository.CreateOrUpdateAsync(workout);

            return new Result<Workout>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = savedWorkout
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<Workout>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }
}
