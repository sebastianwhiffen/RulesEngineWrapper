using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Data;
using RulesEngineWrapper.presentation;

public class RulesEngineWrapperFactory
{
    private readonly Dictionary<string, Func<IContainer, Action<RulesEngineWrapperOptions>>> _optionsConfigurators;

    public RulesEngineWrapperFactory()
    {
        _optionsConfigurators = new Dictionary<string, Func<IContainer, Action<RulesEngineWrapperOptions>>>
        {
            { "postgres", PostgeSqlOptions() },
            { "server", SqlServerOptions() },
            { "mysql", MySqlOptions() }
        };
    }
    private static Func<IContainer, Action<RulesEngineWrapperOptions>> PostgeSqlOptions()
    {
        return container => options =>
        {
            options.DbContextOptionsAction = builder =>
            {
                if (container is IDatabaseContainer databaseContainer)
                {
                    builder.UseNpgsql(databaseContainer.GetConnectionString())
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
                    options.WrapperDbEnsureCreated = true;
                }
            };
        };
    }
    private static Func<IContainer, Action<RulesEngineWrapperOptions>> SqlServerOptions()
    {
        return container => options =>
        {
            options.DbContextOptionsAction = builder =>
            {
                if (container is IDatabaseContainer databaseContainer)
                {
                    builder.UseSqlServer(databaseContainer.GetConnectionString())
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
                    options.WrapperDbEnsureCreated = true;
                }
            };
        };
    }
    private static Func<IContainer, Action<RulesEngineWrapperOptions>> MySqlOptions()
    {
        return container => options =>
        {
            options.DbContextOptionsAction = builder =>
            {
                if (container is IDatabaseContainer databaseContainer)
                {
                    builder.UseMySQL(databaseContainer.GetConnectionString())
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
                    options.WrapperDbEnsureCreated = true;
                }
            };
        };
    }
    public IServiceCollection Create(IContainer container)
    {
        var optionsAction = _optionsConfigurators[container.Image.Name](container);
        return new ServiceCollection().AddRulesEngineWrapper<RulesEngineContext>(optionsAction);
    }
}
