using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Data;
using RulesEngineWrapper.presentation;
using Testcontainers.PostgreSql;

public static class ContainerUtility
{
    public static IEnumerable<object[]> GetDatabaseConfigurations()
    {
        var serviceCollection = UsePostgreSQL().GetAwaiter().GetResult();
        yield return new object[] { serviceCollection };
    }

    public static async Task<IServiceCollection> UsePostgreSQL()
    {
        var container = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .Build();

        await container.StartAsync();

        var configureOptions = new Action<RulesEngineWrapperOptions>(options =>
        {
            options.DbContextOptionsAction = builder =>
            {
                builder.UseNpgsql(container.GetConnectionString());
                options.WrapperDbEnsureCreated = true;
            };
        });

        return new ServiceCollection().AddRulesEngineWrapper<RulesEngineContext>(configureOptions);
    }
}
