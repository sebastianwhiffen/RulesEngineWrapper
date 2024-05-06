using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Data;
using RulesEngineWrapper.presentation;
using Testcontainers.MsSql;
using Testcontainers.MySql;
using Testcontainers.PostgreSql;

public class DatabaseFixture : IAsyncLifetime
{
    public static List<IServiceCollection> ServiceCollections { get; private set; } = new List<IServiceCollection>();
    private static bool isInitialized = false;
    private static readonly object initLock = new object();

    public static async Task EnsureInitializedAsync()
    {
        if (!isInitialized)
        {
            lock (initLock)
            {
                if (!isInitialized)
                {
                    InitializeDatabases().GetAwaiter().GetResult();
                    isInitialized = true;
                }
            }
        }
    }

    public Task InitializeAsync() => EnsureInitializedAsync();

    public Task DisposeAsync()
    {
        // Dispose of containers and other resources
        return Task.CompletedTask;
    }
    private static async Task InitializeDatabases()
    {
        // Assuming these builders correctly asynchronously build and start the containers
        var msSql = new MsSqlBuilder().Build();
        var mySql = new MySqlBuilder().Build();
        var postgreSql = new PostgreSqlBuilder().Build();

        ServiceCollections.Add(await UseContainer(postgreSql, PostgeSqlOptions(postgreSql)));
        ServiceCollections.Add(await UseContainer(mySql, MySqlOptions(mySql)));
        ServiceCollections.Add(await UseContainer(msSql, SqlServerOptions(msSql)));
    }

    private static async Task<IServiceCollection> UseContainer(IContainer container, Action<RulesEngineWrapperOptions> configureOptions)
    {
        await container.StartAsync();
        return new ServiceCollection().AddRulesEngineWrapper<RulesEngineContext>(configureOptions);
    }

    #region options
    private static Action<RulesEngineWrapperOptions> PostgeSqlOptions(IContainer container)
    {
        return options =>
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
    private static Action<RulesEngineWrapperOptions> SqlServerOptions(IContainer container)
    {
        return options =>
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

    private static Action<RulesEngineWrapperOptions> MySqlOptions(IContainer container)
    {
        return options =>
        {
            options.DbContextOptionsAction = builder =>
            {
                if (container is IDatabaseContainer databaseContainer)
                {
                    builder.UseMySql(databaseContainer.GetConnectionString(), new MySqlServerVersion(new Version(8, 0, 21)))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
                    options.WrapperDbEnsureCreated = true;
                }
            };
        };
    }

    #endregion

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}

