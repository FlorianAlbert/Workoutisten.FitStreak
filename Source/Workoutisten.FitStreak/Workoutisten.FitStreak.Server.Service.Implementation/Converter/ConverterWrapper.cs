using Microsoft.Extensions.DependencyInjection;
using Workoutisten.FitStreak.Server.Model;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Converter
{
    public class ConverterWrapper : IConverterWrapper
    {
        public ConverterWrapper(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private IServiceProvider ServiceProvider { get; }

        public async Task<TDto> ToDto<TEntity, TDto>(TEntity entity)
            where TEntity : BaseEntity
            where TDto : class
        {
            var specialConverter = ServiceProvider.GetRequiredService<IConverter<TEntity, TDto>>();

            return await specialConverter.ToDto(entity);
        }

        public async Task<TEntity> ToEntity<TDto, TEntity>(TDto dto)
            where TDto : class
            where TEntity : BaseEntity
        {
            var specialConverter = ServiceProvider.GetRequiredService<IConverter<TEntity, TDto>>();

            return await specialConverter.ToEntity(dto);
        }
    }
}
