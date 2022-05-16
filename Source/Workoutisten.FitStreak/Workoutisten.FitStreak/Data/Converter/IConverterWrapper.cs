using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Converter
{
    public interface IConverterWrapper
    {
        Task<TDto> ToDto<TEntity, TDto>(TEntity entity) where TEntity : class
                                                  where TDto : class;

        Task<TEntity> ToEntity<TDto, TEntity>(TDto dto) where TEntity : class
                                                  where TDto : class;
    }
}
