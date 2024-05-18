using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using RulesEngine.Interfaces;
using RulesEngine.Data;
using RulesEngineWrappers.Domain;

namespace RulesEngineWrappers.Presentation;
public static class RuleEngineServicesExtensions
{
    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
          [Optional] RulesEngineWrapperSettings options)
    {
        options ??= new RulesEngineWrapperSettings();

        services.AddDefaultServices();

        services.AddScoped<IWorkflowService, WorkflowService>();

        if (options.UseDatabase)
        {
            services.AddDbContext<IRulesEngineWrapperContext, RulesEngineWrapperContext>(options.DbContextOptionsAction);
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();
            services.AddScoped<IWorkflowService, WorkflowDataSourceService>();
        }

        return services;
    }

    private static IServiceCollection AddDefaultServices(this IServiceCollection services)
    {
        services.AddScoped<IRulesEngine, RulesEngine.RulesEngine>();
        return services;
    }
}

