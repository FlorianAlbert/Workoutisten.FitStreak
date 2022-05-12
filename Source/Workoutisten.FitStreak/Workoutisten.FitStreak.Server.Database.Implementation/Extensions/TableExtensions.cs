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

            var tableName = context.Model.FindEntityType(type).GetTableName();

            return tableName;
        }

        public static string GetTableName(this BaseEntity entity, FitStreakDbContext context)
        {
            return entity.GetType().GetTableName(context);
        }
    }
}
