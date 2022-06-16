using Workoutisten.FitStreak.Server.Model;

namespace Workoutisten.FitStreak.Server.Service.Interface.Converter
{
    public interface IConverter<TEntity, TDto> where TEntity : BaseEntity
                                               where TDto : class
    {
        Task<TEntity> ToEntity(TDto dto);

        Task<TDto> ToDto(TEntity entity);
    }
}
