using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RulesEngine.Interfaces;
using RulesEngineWrapper.Domain;

namespace RulesEngineWrapper.Presentation;
public static class RuleEngineServicesExtensions
{
    // public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
    //       [Optional] Action<IConfiguration<RulesEngineWrapper>> action)
    // {


    //     services.AddDefaultServices(options);

    //     services.AddScoped<IWorkflowService, WorkflowService>();

    //     if (options.UseDatabase)
    //     {
    //         services.AddDbContext<IRulesEngineWrapperContext, RulesEngineWrapperContext>(options.DbContextOptionsAction);
    //         services.AddScoped<IWorkflowRepository, WorkflowRepository>();
    //         services.AddScoped<IWorkflowService, WorkflowDataSourceService>();
    //     }

    //     options.Logger(services);

    //     return services;
    // }

    internal static IRulesEngineWrapper<T> AddServiceDefaults<T>(this IRulesEngineWrapper<T> wrapper) where T : IRulesEngineWrapper
    {   
        wrapper.ModifyRulesEngineWrapperServiceCollection(x => x.AddLogging(builder =>builder.AddProvider(new NoOpLoggerProvider())));
        wrapper.ModifyRulesEngineWrapperServiceCollection(x => x.AddSingleton<IRulesEngine, RulesEngine.RulesEngine>());
        wrapper.ModifyRulesEngineWrapperServiceCollection(x => x.AddSingleton<IWorkflowService, WorkflowService>());
        wrapper.ModifyRulesEngineWrapperServiceCollection(x => x.AddSingleton<IWorkflowRepository, WorkflowRepository>());

        return wrapper;
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

