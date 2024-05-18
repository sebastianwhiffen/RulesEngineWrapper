using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using RulesEngine.Interfaces;

namespace RulesEngineWrappers.presentation;
public static class RuleEngineServicesExtensions
{
    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
          [Optional] RulesEngineWrapperSettings options)
    {
        options ??= new RulesEngineWrapperSettings();

        services.AddDefaultServices();
        services.AddScoped<IWorkflowService, WorkflowService>();
        services.AddScoped<IRulesEngineWrapper, RulesEngineWrapper>(p =>
        {
            return new RulesEngineWrapper(options);
        });

        return services;
    }

    public static IServiceCollection AddRulesEngineWrapper<TContext>(this IServiceCollection services,
        [Optional] RulesEngineWrapperSettings options) where TContext : DbContext, IRulesEngineWrapperContext
    {
        options ??= new RulesEngineWrapperSettings();

        services.AddDefaultServices();
        services.AddDbContext<IRulesEngineWrapperContext, TContext>(options.DbContextOptionsAction);
        services.AddScoped<IRulesEngineWrapper, RulesEngineWrapper>(p =>
        {
            var dbContext = p.GetRequiredService<TContext>();
            if (options.WrapperDbEnsureCreated) dbContext.Database.EnsureCreated();

            return new RulesEngineWrapper(options);
        });

        return services;
    }

    public static IServiceCollection AddDefaultServices(this IServiceCollection services)
    {
        services.AddScoped<IRulesEngine, RulesEngine.RulesEngine>();
        return services;
    }
}

