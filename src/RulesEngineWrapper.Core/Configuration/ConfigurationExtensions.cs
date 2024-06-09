using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper.Domain;

namespace RulesEngineWrapper;

public static class ConfigurationExtensions
{
    public static IConfiguration<IRulesEngineWrapper<T>> UseLogging<T>(
        this IConfiguration<IRulesEngineWrapper<T>> configuration,
        Action<ILoggingBuilder> action = null
        ) where T : IRulesEngineWrapper
    {
        configuration.Entry.Entity.Services.AddLogging(action ?? (x => x.AddConsole()));

        return configuration;
    }

    public static IConfiguration<IRulesEngineWrapper<T>> UseRulesEngine<T>(
       this IConfiguration<IRulesEngineWrapper<T>> configuration,
       Action<ReSettings> action = null
       ) where T : IRulesEngineWrapper
    {
        var options = new ReSettings();

        if (action != null) action(options);

        configuration.Entry.Services.AddScoped<IRulesEngine, RulesEngine.RulesEngine>(p =>
        new RulesEngine.RulesEngine(options));

        return configuration;
    }

    public static IConfiguration<IRulesEngineWrapper<RulesEngineWrapper>> UseDatabase<R>(
     this IConfiguration<IRulesEngineWrapper<RulesEngineWrapper>> configuration,
     Action<DbContextOptionsBuilder> action = null)
     where R : DbContext, IRulesEngineWrapperContext
    {
        configuration.Entry.Services.AddDbContext<IRulesEngineWrapperContext,R>(action);

        configuration.Entry.Services.AddSingleton<IWorkflowService, WorkflowDataSourceService>();

        configuration.Entry.Services.AddSingleton<IWorkflowRepository, WorkflowRepository>();
        
        return configuration;
    }

    public static IConfiguration<IRulesEngineWrapper<T>> UseDatabase<T, R>(
     this IConfiguration<IRulesEngineWrapper<T>> configuration,
     Action<DbContextOptionsBuilder> action = null)
     where T : IRulesEngineWrapper where R : DbContext, IRulesEngineWrapperContext
    {
        configuration.Entry.Services.AddDbContext<IRulesEngineWrapperContext,R>(action);
        
        return configuration;
    }

}