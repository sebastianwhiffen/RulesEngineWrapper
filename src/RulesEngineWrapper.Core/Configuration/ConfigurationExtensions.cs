using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RulesEngine.Data;
using RulesEngine.Interfaces;
using RulesEngine.Models;
using RulesEngineWrapper.Domain;

namespace RulesEngineWrapper;

public static class ConfigurationExtensions
{
    public static IRulesEngineWrapper<T> UseLogging<T>(
        this IRulesEngineWrapper<T> wrapper,
        Action<ILoggingBuilder> action = null
        ) where T : IRulesEngineWrapper
    {
        wrapper.Services.AddLogging(action ?? (x => x.AddConsole()));

        return wrapper;
    }

    public static IRulesEngineWrapper<T> UseRulesEngine<T>(
       this IRulesEngineWrapper<T> wrapper,
       Action<ReSettings> action = null
       ) where T : IRulesEngineWrapper
    {
        var options = new ReSettings();

        if (action != null) action(options);

        wrapper.Services.AddScoped<IRulesEngine, RulesEngine.RulesEngine>(p =>
        new RulesEngine.RulesEngine(options));

        return wrapper;
    }

    public static IRulesEngineWrapper<RulesEngineWrapper> UseDatabase(
    this IRulesEngineWrapper<RulesEngineWrapper> wrapper,
    Action<DbContextOptionsBuilder> action = null) => wrapper.UseDatabase<RulesEngineWrapperContext>(action);

    public static IRulesEngineWrapper<RulesEngineWrapper> UseDatabase<R>(
     this IRulesEngineWrapper<RulesEngineWrapper> wrapper,
     Action<DbContextOptionsBuilder> action = null)
     where R : DbContext, IRulesEngineWrapperContext
    {
        wrapper.UseDatabase<IRulesEngineWrapper<RulesEngineWrapper>, R>(action);

        return wrapper;
    }
    public static IRulesEngineWrapper<T> UseDatabase<T, R>(
     this IRulesEngineWrapper<T> wrapper,
     Action<DbContextOptionsBuilder> action = null)
     where T : IRulesEngineWrapper where R : DbContext, IRulesEngineWrapperContext
    {
        
        wrapper.Services.AddDbContext<IRulesEngineWrapperContext, R>(action);

        wrapper.Services.AddSingleton<IWorkflowService, WorkflowDataSourceService>();

        wrapper.Services.AddSingleton<IWorkflowRepository, WorkflowRepository>();

        return wrapper;
    }
}

class NoOpLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new NoOpLogger();
    public void Dispose() { }
}

class NoOpLogger : ILogger
{
    public IDisposable BeginScope<TState>(TState state) => null!;
    public bool IsEnabled(LogLevel logLevel) => false;
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
}