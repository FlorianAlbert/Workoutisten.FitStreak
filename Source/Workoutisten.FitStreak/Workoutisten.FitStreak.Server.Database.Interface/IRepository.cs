using Workoutisten.FitStreak.Server.Model;

namespace Workoutisten.FitStreak.Server.Database.Interface
{
    public interface IRepository : IDisposable
    {
        Task<TEntity> GetAsync<TEntity>(Guid id) where TEntity : BaseEntity;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Func<TEntity, bool> where = null) where TEntity : BaseEntity;

        Task<TEntity> CreateOrUpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task DeleteAsync<TEntity>(Guid id) where TEntity : BaseEntity;

        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
    }
}