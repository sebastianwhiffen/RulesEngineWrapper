using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineWrapper.Dashboard
{
    public static class RulesEngineWrapperDashboardServiceCollectionExtensions
    {
        public static IServiceCollection AddRulesEngineWrapperDashboard(this IServiceCollection services)
        {
            return services.AddRulesEngineWrapperDashboard<RulesEngineWrapperDashboard>();
        }

        public static IServiceCollection AddRulesEngineWrapperDashboard<T>(this IServiceCollection services) where T : class , IRulesEngineWrapperDashboard
        {
            return services.AddSingleton<IRulesEngineWrapperDashboard, T>();
        }
    }
}
