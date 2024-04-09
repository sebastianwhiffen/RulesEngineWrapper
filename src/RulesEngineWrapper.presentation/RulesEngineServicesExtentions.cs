using Microsoft.AspNetCore.Builder;
using RulesEngineWrapper.presentation.Options;
using RulesEngineWrapper.presentation.APIs;
using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineWrapper.presentation;

public static class RuleEngineServicesExtensions
{
    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services, RulesEngineServiceOptions options)
    {
        services.AddScoped<RulesEngine.RulesEngine>();

        return services;
    }

    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services, Action<RulesEngineServiceOptions>? optionsAction = null)
    {
        RulesEngineServiceOptions options = new RulesEngineServiceOptions();

        optionsAction?.Invoke(options);

        return services.AddRulesEngineWrapper(options);
    }
 
}
