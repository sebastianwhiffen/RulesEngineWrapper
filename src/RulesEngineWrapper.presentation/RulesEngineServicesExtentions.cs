using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RulesEngineWrapper.presentation.Options;
using System.Runtime.InteropServices;

namespace RulesEngineWrapper.presentation;

public static class RuleEngineServicesExtensions
{
    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
       [Optional] Action<RulesEngineWrapperOptions> optionsAction)
    {
        services.AddScoped<IDataSourceRepository, FileSourceRepository>();

        RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
        optionsAction?.Invoke(options);

        services.AddScoped<IRulesEngineWrapper, RulesEngineWrapper>(p =>
        {
            var dataSourceRepository = p.GetRequiredService<IDataSourceRepository>();
            return new RulesEngineWrapper(options.workflowsToInit.ToArray(), options, dataSourceRepository);
        });

        return services;
    }

    public static IServiceCollection AddRulesEngineWrapper<TContext>(this IServiceCollection services,
        [Optional] Action<RulesEngineWrapperOptions>? optionsAction) where TContext : DbContext, IRulesEngineWrapperContext
    {
        services.AddScoped<IDataSourceRepository, DatabaseSourceRepository>();

        RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
        optionsAction?.Invoke(options);

        if (options?.DbContextOptionsAction == null) throw new ArgumentNullException("You must specify a database provider if you wish to register a DbContext. Please register a provider in options.DbContextOptionsAction ");

        services.AddDbContext<IRulesEngineWrapperContext, TContext>(options.DbContextOptionsAction);
        services.AddScoped<IRulesEngineWrapper, RulesEngineWrapper>(p =>
        {
            var dbContext = p.GetRequiredService<TContext>();
            var dataSourceRepository = p.GetRequiredService<IDataSourceRepository>();

            if (options.WrapperDbEnsureCreated) dbContext.Database.EnsureCreatedAsync();

            var workflows = dbContext.Workflows.Include(w => w.Rules).ToArray();

            //this requires a workflow Entity. currenlty the wrapper object is set up for a workflow from the base rules engine. need to evaluate and change
            return ActivatorUtilities.CreateInstance<RulesEngineWrapper>(p, workflows, options, dataSourceRepository);
        });

        return services;
    }
}

