using Workoutisten.FitStreak.Server.Model;

namespace Workoutisten.FitStreak.Server.Service.Interface.Converter
{
    public interface IConverterWrapper
    {
        Task<TDto> ToDto<TEntity, TDto>(TEntity entity) where TEntity : BaseEntity
                                                  where TDto : class;

        Task<TEntity> ToEntity<TDto, TEntity>(TDto dto) where TEntity : BaseEntity
                                                  where TDto : class;
    }
}
