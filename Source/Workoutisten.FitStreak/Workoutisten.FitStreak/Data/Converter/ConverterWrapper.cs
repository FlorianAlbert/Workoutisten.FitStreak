using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workoutisten.FitStreak.Converter;

namespace Workoutisten.FitStreak.Data.Converter
{
    public class ConverterWrapper: IConverterWrapper
    {
        public ConverterWrapper(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private IServiceProvider ServiceProvider { get; }

        public async Task<TDto> ToDto<TEntity, TDto>(TEntity entity)
            where TEntity : class
            where TDto : class
        {
            var specialConverter = ServiceProvider.GetRequiredService<IConverter<TEntity, TDto>>();

            return await specialConverter.ToDto(entity);
        }

        public async Task<TEntity> ToEntity<TDto, TEntity>(TDto dto)
            where TDto : class
            where TEntity : class
        {
            var specialConverter = ServiceProvider.GetRequiredService<IConverter<TEntity, TDto>>();

            return await specialConverter.ToEntity(dto);
        }
    }
}
