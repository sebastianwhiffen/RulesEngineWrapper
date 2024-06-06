using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using RulesEngine.Interfaces;
using RulesEngine.Data;
using RulesEngineWrapper.Domain;

namespace RulesEngineWrapper.Presentation;
public static class RuleEngineServicesExtensions
{
    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
          [Optional] RulesEngineWrapperSettings options)
    {
        options ??= new RulesEngineWrapperSettings();

        services.AddDefaultServices(options);

        services.AddScoped<IWorkflowService, WorkflowService>();

        if (options.UseDatabase)
        {
            services.AddDbContext<IRulesEngineWrapperContext, RulesEngineWrapperContext>(options.DbContextOptionsAction);
            services.AddScoped<IWorkflowRepository, WorkflowRepository>();
            services.AddScoped<IWorkflowService, WorkflowDataSourceService>();
        }
        
        options.Logger(services);

        return services;
    }

    private static IServiceCollection AddDefaultServices(this IServiceCollection services, RulesEngineWrapperSettings options)
    {
        services.AddScoped<IRulesEngine, RulesEngine.RulesEngine>(p =>
        new RulesEngine.RulesEngine(options.ReSettings)
        );
        return services;
    }

     public static void ThrowIfNotConfigured(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IRulesEngine>();
            if (configuration == null)
            {
                throw new InvalidOperationException(
                    "Unable to find the required services. Please add all the required services by calling 'IServiceCollection.AddRulesEngineWrapper");
            }
        }
}

