using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model;

namespace Workoutisten.FitStreak.Server.Database.Implementation
{
    public class Repository : IRepository
    {
        public Repository(FitStreakDbContext dbContext)
        {
            DbContext = dbContext;
        }

        private FitStreakDbContext DbContext;

        public async Task<TEntity> CreateOrUpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            EntityEntry<TEntity> entityEntry;

            try
            {
                await using var transaction = await DbContext.Database.BeginTransactionAsync();

                var searchedEntity = await DbContext.Set<TEntity>().FindAsync(entity.Id);

                if (searchedEntity is not null)
                {
                    entityEntry = DbContext.Set<TEntity>().Update(entity);
                }
                else
                {
                    entityEntry = await DbContext.Set<TEntity>().AddAsync(entity);
                }

                await DbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseRepositoryException(DbContext.Database.GetDbConnection().Database,
                    $"Creating or updating the entity with Id = {entity.Id} was not successfull.",
                    ex);
            }

            return entityEntry.Entity;
        }

        public async Task DeleteAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            try
            {
                await using var transaction = await DbContext.Database.BeginTransactionAsync();

                var entity = await DbContext.Set<TEntity>().FindAsync(id);

                if (entity is not null)
                {
                    DbContext.Set<TEntity>().Remove(entity);
                }
                else
                {
                    await transaction.RollbackAsync();

                    throw new EntryNotFoundException(id, typeof(TEntity), DbContext.Database.GetDbConnection().Database);
                }

                await DbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseRepositoryException(DbContext.Database.GetDbConnection().Database,
                    $"Deleting the entity with Id = {id} was not successfull.",
                    ex);
            }
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                await using var transaction = await DbContext.Database.BeginTransactionAsync();

                if (DbContext.Set<TEntity>().Contains(entity))
                {
                    DbContext.Set<TEntity>().Remove(entity);
                }
                else
                {
                    await transaction.RollbackAsync();

                    throw new EntryNotFoundException(entity.Id, typeof(TEntity), DbContext.Database.GetDbConnection().Database);
                }

                await DbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseRepositoryException(DbContext.Database.GetDbConnection().Database,
                    $"Deleting the entity with Id = {entity.Id} was not successfull.",
                    ex);
            }
        }

        public async Task<TEntity> GetAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            try
            {
                return await DbContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DatabaseRepositoryException(DbContext.Database.GetDbConnection().Database,
                    $"There was an error finding the entity with Id = {id}.",
                    ex);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Func<TEntity, bool> where = null) where TEntity : BaseEntity
        {
            where ??= x => true;

            try
            {
                return DbContext.Set<TEntity>().Where(where).ToList();
            }
            catch (Exception ex)
            {
                throw new DatabaseRepositoryException(DbContext.Database.GetDbConnection().Database,
                    $"There was an error selecting the entities.",
                    ex);
            }
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
