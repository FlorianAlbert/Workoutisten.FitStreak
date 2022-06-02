using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training
{
    public class SetService : ISetService
    {
        public SetService(IRepository repo)
        {
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        private IRepository Repo { get; }

        public async Task<Result<CardioSet>> CreateCardioSet(Guid userId, Guid doneExerciseId, double distance, TimeSpan duration)
        {
            try
            {
                var doneExercise = await Repo.GetAsync<DoneExercise>(doneExerciseId);

                if (doneExercise is null) return new Result<CardioSet>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There does not exist a DoneExercise with Id={doneExerciseId}."
                };

                if (doneExercise.ExercisingUser.Id != userId) return new Result<CardioSet>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to create the create a CardioSet in the DoneExercise with the id {doneExerciseId} because you are not the creator!"
                };

                var newCardioSet = new CardioSet
                {
                    DoneExercise = doneExercise,
                    Distance = distance,
                    Duration = duration
                };

                var createdEntity = await Repo.CreateOrUpdateAsync(newCardioSet);

                return new Result<CardioSet>
                {
                    StatusCode = StatusCodes.Status201Created,
                    Value = createdEntity
                };
            }
            catch (DatabaseRepositoryException)
            {
                return new Result<CardioSet>
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Detail = "The Database - Service couldn't connect to the Database."
                };
            }
        }

        public async Task<Result<StrengthSet>> CreateStrengthSet(Guid userId, Guid doneExerciseId, double weight, int repetitions)
        {
            try
            {
                var doneExercise = await Repo.GetAsync<DoneExercise>(doneExerciseId);

                if (doneExercise is null) return new Result<StrengthSet>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There does not exist a DoneExercise with Id={doneExerciseId}."
                };

                if (doneExercise.ExercisingUser.Id != userId) return new Result<StrengthSet>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"You are not authorized to create the create a StrengthSet in the DoneExercise with the id {doneExerciseId} because you are not the creator!"
                };

                var newStrengthSet = new StrengthSet
                {
                    DoneExercise = doneExercise,
                    Weight = weight,
                    Repetitions = repetitions
                };

                var createdEntity = await Repo.CreateOrUpdateAsync(newStrengthSet);

                return new Result<StrengthSet>
                {
                    StatusCode = StatusCodes.Status201Created,
                    Value = createdEntity
                };
            }
            catch (DatabaseRepositoryException)
            {
                return new Result<StrengthSet>
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Detail = "The Database - Service couldn't connect to the Database."
                };
            }
        }

        public async Task<Result> DeleteSet(Guid userId, Guid setId)
        {
            try
            {
                var set = await Repo.GetAsync<Set>(setId);
                if (set is null)
                {
                    return new Result
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Detail = $"There exists no Set with the id {setId}."
                    };
                }

                if (set.DoneExercise.ExercisingUser.Id != userId)
                {
                    return new Result<Exercise>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Detail = $"You are not authorized to delete the set with the id {setId} because you are not the creator!"
                    };
                }

                await Repo.DeleteAsync(set);

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
                    Detail = $"The Set couldn't be deleted because while deleting there was no Set with the id {setId}."
                };

                return new Result
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Detail = "The Database - Service couldn't connect to the Database."
                };
            }
        }

        public async Task<Result<IEnumerable<Set>>> GetAllSetsForUser(Guid userId)
        {
            try
            {
                var sets = await Repo.GetAllAsync<Set>(x => x.DoneExercise.ExercisingUser.Id == userId);

                return new Result<IEnumerable<Set>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Value = sets
                };
            }
            catch (DatabaseRepositoryException)
            {
                return new Result<IEnumerable<Set>>
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Detail = "The Database - Service couldn't connect to the Database."
                };
            }
        }

        public async Task<Result<Set>> GetSetForUser(Guid userId, Guid setId)
        {
            try
            {
                var set = await Repo.GetAsync<Set>(setId);
                if (set is null)
                {
                    return new Result<Set>
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Detail = $"There exists no Set with the id {setId}."
                    };
                }

                if (set.DoneExercise.ExercisingUser.Id != userId)
                {
                    return new Result<Set>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Detail = $"You are not authorized to get the Set with the id {setId} because you are not the creator!"
                    };
                }

                return new Result<Set>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Value = set
                };
            }
            catch (DatabaseRepositoryException)
            {
                return new Result<Set>
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Detail = "The Database - Service couldn't connect to the Database."
                };
            }
        }

        public async Task<Result<CardioSet>> UpdateCardioSet(Guid userId, Guid setId, double distance, TimeSpan duration)
        {
            try
            {
                var set = await Repo.GetAsync<Set>(setId);
                if (set is null || set is not CardioSet cardioSet)
                {
                    return new Result<CardioSet>
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Detail = $"There exists no CardioSet with the id {setId}."
                    };
                }

                if (cardioSet.DoneExercise.ExercisingUser.Id != userId)
                {
                    return new Result<CardioSet>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Detail = $"You are not authorized to update the CardioSet with the id {setId} because you are not the creator!"
                    };
                }

                cardioSet.Duration = duration;
                cardioSet.Distance = distance;

                var updatedEntity = await Repo.CreateOrUpdateAsync(cardioSet);

                return new Result<CardioSet>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Value = cardioSet
                };
            }
            catch (DatabaseRepositoryException)
            {
                return new Result<CardioSet>
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Detail = "The Database - Service couldn't connect to the Database."
                };
            }
        }

        public async Task<Result<StrengthSet>> UpdateStrengthSet(Guid userId, Guid setId, double weight, int repetitions)
        {
            try
            {
                var set = await Repo.GetAsync<Set>(setId);
                if (set is null || set is not StrengthSet strengthSet)
                {
                    return new Result<StrengthSet>
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Detail = $"There exists no StrengthSet with the id {setId}."
                    };
                }

                if (strengthSet.DoneExercise.ExercisingUser.Id != userId)
                {
                    return new Result<StrengthSet>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Detail = $"You are not authorized to update the StrengthSet with the id {setId} because you are not the creator!"
                    };
                }

                strengthSet.Repetitions = repetitions;
                strengthSet.Weight = weight;

                var updatedEntity = await Repo.CreateOrUpdateAsync(strengthSet);

                return new Result<StrengthSet>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Value = strengthSet
                };
            }
            catch (DatabaseRepositoryException)
            {
                return new Result<StrengthSet>
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Detail = "The Database - Service couldn't connect to the Database."
                };
            }
        }
    }
}
