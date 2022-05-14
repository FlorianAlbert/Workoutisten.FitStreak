using EntityFrameworkCore.Triggered;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Workoutisten.FitStreak.Server.Database.Implementation.Trigger;

namespace Workoutisten.FitStreak.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTriggers(this IServiceCollection services)
        {
            var triggerCandidates = typeof(OnModifiedBaseEntity)
                .Assembly
                .GetTypes()
                .Where(x => x.IsClass);

            var genericTriggerTypes = new[]
            {
                typeof(IAfterSaveTrigger<>),
                typeof(IBeforeSaveTrigger<>),
                typeof(IAfterSaveFailedTrigger<>)
            };

            foreach (var genericTriggerType in genericTriggerTypes)
            {
                foreach (var triggerCandidate in triggerCandidates)
                {
                    foreach (var @interface in triggerCandidate.GetInterfaces())
                    {
                        if (@interface.IsConstructedGenericType && @interface.GetGenericTypeDefinition() == genericTriggerType)
                        {
                            services.TryAddScoped(triggerCandidate);

                            services.AddScoped(@interface, serviceProvider => serviceProvider.GetRequiredService(triggerCandidate));
                        }
                    }
                }
            }
        }
    }
}
