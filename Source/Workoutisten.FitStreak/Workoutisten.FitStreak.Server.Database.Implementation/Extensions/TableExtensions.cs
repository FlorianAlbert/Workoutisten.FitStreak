using Microsoft.EntityFrameworkCore;
using Workoutisten.FitStreak.Server.Model;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Extensions
{
    public static class TableExtensions
    {
        public static string GetTableName(this Type type, FitStreakDbContext context)
        {
            if (!type.IsSubclassOf(typeof(BaseEntity)))
            {
                throw new ArgumentException("Argument does not inherit from abstract class BaseEntity.", nameof(type));
            }

            var entityType = context.Model.FindRuntimeEntityType(type);
            var tableName = entityType?.GetTableName();
            if (tableName is null)
            {
                throw new ArgumentException($"There exists no table with the tableName {tableName}.", nameof(tableName));
            }

            return tableName;
        }

        public static string GetTableName(this BaseEntity entity, FitStreakDbContext context)
        {
            return entity.GetType().GetTableName(context);
        }
    }
}
