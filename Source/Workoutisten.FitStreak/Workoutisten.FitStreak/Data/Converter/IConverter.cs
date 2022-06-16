using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Converter
{
    public interface IConverter<TEntity, TDto> where TEntity : class
                                                where TDto : class
    {
        Task<TEntity> ToEntity(TDto dto);

        Task<TDto> ToDto(TEntity entity);
    }
}
