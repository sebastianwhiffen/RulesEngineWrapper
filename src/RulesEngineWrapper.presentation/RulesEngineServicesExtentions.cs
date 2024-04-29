using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using RulesEngineWrapper.Infrastructure;
using System.Reflection;

namespace RulesEngineWrapper.presentation;
public static class RuleEngineServicesExtensions
{
    public static IServiceCollection AddRulesEngineWrapper(this IServiceCollection services,
        [Optional] Action<RulesEngineWrapperOptions> optionsAction,
        Assembly callingAssembly)
    {
        services.AddScoped<IDataSourceRepository, FileSourceRepository>();

        RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
        optionsAction?.Invoke(options);

        services.AddScoped<IRulesEngineWrapper, RulesEngineWrapper>(p =>
        {
            var dataSourceRepository = p.GetRequiredService<IDataSourceRepository>();
            return new RulesEngineWrapper(options.workflowsToInit.ToArray(), options, dataSourceRepository);
        });

        services.AddDefaultServices(callingAssembly); 

        return services;
    }

    public static IServiceCollection AddRulesEngineWrapper<TContext>(this IServiceCollection services,
        [Optional] Action<RulesEngineWrapperOptions>? optionsAction, Assembly callingAssembly) where TContext : DbContext, IRulesEngineWrapperContext
    {
        services.AddScoped<IDataSourceRepository, DatabaseSourceRepository>();

        RulesEngineWrapperOptions options = new RulesEngineWrapperOptions();
        optionsAction?.Invoke(options);

        if (options?.DbContextOptionsAction == null)
            throw new ArgumentNullException("You must specify a database provider if you wish to register a DbContext. Please register a provider in options.DbContextOptionsAction");

        services.AddDbContext<IRulesEngineWrapperContext, TContext>(options.DbContextOptionsAction);
        services.AddScoped<IRulesEngineWrapper, RulesEngineWrapper>(p =>
        {
            var dbContext = p.GetRequiredService<TContext>();
            var dataSourceRepository = p.GetRequiredService<IDataSourceRepository>();

            if (options.WrapperDbEnsureCreated) dbContext.Database.EnsureCreated();

            var workflows = dbContext.Workflows.Include(w => w.Rules);
            return ActivatorUtilities.CreateInstance<RulesEngineWrapper>(p, workflows, options, dataSourceRepository);
        });

        services.AddDefaultServices(callingAssembly);  

        return services;
    }

    private static IServiceCollection AddDefaultServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(assembly.GetType());
        });

        return services;
    }
}

