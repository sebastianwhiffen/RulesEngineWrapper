using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RulesEngineWrapper.presentation.Options;
using RulesEngineWrapper.presentation.Queries;
using RulesEngine.Interfaces;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace RulesEngineWrapper.presentation;

public static class RuleEngineServicesExtensions
{
    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
       [Optional] Action<RulesEngineWrapperOptions> optionsAction)
    {
        RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
        optionsAction?.Invoke(options);

        services.AddScoped<IRulesEngine, RulesEngine.RulesEngine>(p =>
        {
            return new RulesEngine.RulesEngine(options.workflowsToInit.ToArray(), options.reSettings);
        });

        services.AddScoped<IDataSourceRepository, FileSourceRepository>();

        return services;
    }

    public static IServiceCollection AddRulesEngineWrapper<TContext>(this IServiceCollection services,
       [Optional] Action<RulesEngineWrapperOptions>? optionsAction) where TContext : DbContext, IRulesEngineContext
    {
        services.AddServiceDefaults();

        RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
        optionsAction?.Invoke(options);

        if (options?.DbContextOptionsAction == null) throw new ArgumentNullException("You must specify a database provider if you wish to register a DbContext. Please register a provider in options.DbContextOptionsAction ");

        services.AddDbContext<IRulesEngineContext, TContext>(options.DbContextOptionsAction);
        services.AddScoped<IRulesEngine, RulesEngine.RulesEngine>(p =>
        {
            var dbContext = p.GetRequiredService<TContext>();

            if(options.WrapperDbEnsureCreated) dbContext.Database.EnsureCreatedAsync();

            return new RulesEngine.RulesEngine(dbContext.Workflows.Include(w => w.Rules).ToArray(), options.reSettings);
        });
        services.AddScoped<IDataSourceRepository, DatabaseRulesEngineRepository>();

        return services;
    }

    private static IServiceCollection AddServiceDefaults(this IServiceCollection services)
    {
        services.AddScoped<IRulesEngineQueries, RulesEngineQueries>();
        return services;
    }
}

