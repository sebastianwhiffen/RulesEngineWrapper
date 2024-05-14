using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using RulesEngineWrappers.Domain;
using MediatR;

namespace RulesEngineWrappers.presentation;
public static class RuleEngineServicesExtensions
{
    // public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
    //     [Optional] Action<RulesEngineWrapperOptions> optionsAction,
    //     Assembly callingAssembly)
    // {
    //     services.AddScoped<IWorkflowRepository, Work>();

    //     RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
    //     optionsAction?.Invoke(options);

    //     services.AddScoped<IRulesEngineWrapper, RulesEngineWrapper>(p =>
    //     {
    //         var dataSourceRepository = p.GetRequiredService<IDataSourceRepository>();
    //         return new RulesEngineWrapper(options.workflowsToInit.ToArray(), options, dataSourceRepository);
    //     });

    //     services.AddDefaultServices(callingAssembly); 

    //     return services;
    // }

    public static IServiceCollection AddRulesEngineWrapper<TContext>(this IServiceCollection services,
        [Optional] RulesEngineWrapperSettings options) where TContext : DbContext, IRulesEngineWrapperContext
    {
        options ??= new RulesEngineWrapperSettings();

        services.AddDefaultServices();  

        services.AddDbContext<IRulesEngineWrapperContext, TContext>(options.DbContextOptionsAction);
        services.AddScoped<IRulesEngineWrapper, RulesEngineWrapper>(p =>
        {
            var dbContext = p.GetRequiredService<TContext>();
            var mediator = p.GetRequiredService<IMediator>();

            options.mediator = mediator;
            
            if (options.WrapperDbEnsureCreated) dbContext.Database.EnsureCreated();

            return new RulesEngineWrapper(options);
        });
        
        return services;
    }

    private static IServiceCollection AddDefaultServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkflowRepository, WorkflowRepository>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<RulesEngineWrapper>();
        });
        return services;
    }
}

