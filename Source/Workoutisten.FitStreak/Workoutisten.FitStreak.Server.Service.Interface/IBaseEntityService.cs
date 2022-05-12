using Workoutisten.FitStreak.Server.Model;

namespace Workoutisten.FitStreak.Server.Service.Interface;
public interface IBaseEntityService<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> CreateOrUpdateAsync(TEntity entity);

    Task<bool> DeleteAsync(Guid id);

    Task<bool> DeleteAsync(TEntity entity);
}
