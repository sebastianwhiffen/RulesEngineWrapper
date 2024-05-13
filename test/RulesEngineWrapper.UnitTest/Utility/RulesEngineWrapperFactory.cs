using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Data;
using RulesEngineWrappers.presentation;

public class RulesEngineWrapperFactory
{
    public readonly Dictionary<string, Func<IContainer, Action<RulesEngineWrapperSettings>>> _optionsConfigurators;

    public RulesEngineWrapperFactory()
    {
        _optionsConfigurators = new Dictionary<string, Func<IContainer, Action<RulesEngineWrapperSettings>>>
        {
            { "postgres", PostgeSqlOptions() },
            { "server", SqlServerOptions() },
            { "mysql", MySqlOptions() }
        };
    }
    private static Func<IContainer, Action<RulesEngineWrapperSettings>> PostgeSqlOptions()
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

    private static Func<IContainer, Action<RulesEngineWrapperSettings>> InMemoryDbOptions()
    {
        return container => options =>
        {
            options.DbContextOptionsAction = builder =>
            {
                builder.UseInMemoryDatabase("RulesEngineWrapper");
                options.WrapperDbEnsureCreated = true;
            };
        };
    }

    private static Func<IContainer, Action<RulesEngineWrapperSettings>> SqlServerOptions()
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
    private static Func<IContainer, Action<RulesEngineWrapperSettings>> MySqlOptions()
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
        var configureOptions = _optionsConfigurators[container.Image.Name];

        var settings = new RulesEngineWrapperSettings();

        configureOptions(container)(settings);

        return new ServiceCollection().AddRulesEngineWrapper<RulesEngineWrapperContext>(settings);

    }
}
