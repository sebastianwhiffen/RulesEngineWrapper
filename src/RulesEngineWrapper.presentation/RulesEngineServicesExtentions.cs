using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RulesEngineWrapper.presentation.Options;
using RulesEngineWrapper.presentation.Queries;
using RulesEngine.Interfaces;
using System.Runtime.InteropServices;
using FastExpressionCompiler;

namespace RulesEngineWrapper.presentation;

public static class RuleEngineServicesExtensions
{
    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
       [Optional] Action<RulesEngineWrapperOptions> optionsAction)
    {
        services.AddScoped<IDataSourceRepository, FileSourceRepository>();

        RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
        optionsAction?.Invoke(options);

        services.AddScoped<IRulesEngine, RulesEngineWrapper>(p =>
        {
            return new RulesEngineWrapper(options.workflowsToInit.ToArray(), options.reSettings);
        });

        return services;
    }

    public static IServiceCollection AddRulesEngineWrapper<TContext>(this IServiceCollection services,
       [Optional] Action<RulesEngineWrapperOptions>? optionsAction) where TContext : DbContext, IRulesEngineContext
    {
        services.AddScoped<IDataSourceRepository, DatabaseRulesEngineRepository>();

        RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
        optionsAction?.Invoke(options);

        if (options?.DbContextOptionsAction == null) throw new ArgumentNullException("You must specify a database provider if you wish to register a DbContext. Please register a provider in options.DbContextOptionsAction ");

        services.AddDbContext<IRulesEngineContext, TContext>(options.DbContextOptionsAction);
        services.AddScoped<IRulesEngine, RulesEngineWrapper>(p =>
        {
            var dbContext = p.GetRequiredService<TContext>();

            if(options.WrapperDbEnsureCreated) dbContext.Database.EnsureCreatedAsync();

            return new RulesEngineWrapper(dbContext.Workflows.Include(w => w.Rules).ToArray(), options.reSettings);
        });

        return services;
    }
}

