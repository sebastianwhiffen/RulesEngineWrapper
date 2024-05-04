using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Data;
using RulesEngineWrapper.presentation;
using Testcontainers.PostgreSql;
using DotNet.Testcontainers.Containers;
using Testcontainers.MySql;
using Testcontainers.MsSql;

public static class ContainerUtility
{
    public static IEnumerable<object[]> GetServiceSetups()
    {

        var msSql = new MsSqlBuilder().Build();
        var mySql = new MySqlBuilder().Build();
        var postgreSql = new PostgreSqlBuilder().Build();

        var services = new List<IServiceCollection>
        {
            UseContainer(postgreSql,PostgeSqlOptions(postgreSql) ).GetAwaiter().GetResult(),
            UseContainer(mySql, MySqlOptions(mySql)).GetAwaiter().GetResult(),
            UseContainer(msSql, SqlServerOptions(msSql)).GetAwaiter().GetResult(),
        };

        foreach (var serviceSet in services)
        {
            yield return new object[] { serviceSet };
        }
    }

    public static async Task<IServiceCollection> UseContainer(IContainer container, Action<RulesEngineWrapperOptions> configureOptions)
    {
        await container.StartAsync();
        Console.WriteLine($"Starting container {container.Name}");

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
                    builder.UseNpgsql(databaseContainer.GetConnectionString());
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
                    builder.UseSqlServer(databaseContainer.GetConnectionString());
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
                    // Using the connection string from the container and configuring MySQL-specific options.
                    builder.UseMySql(databaseContainer.GetConnectionString(), new MySqlServerVersion(new Version(8, 0, 21)));
                    options.WrapperDbEnsureCreated = true;
                }
            };
        };
    }

    #endregion

}
