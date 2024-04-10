using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RulesEngineWrapper.presentation.Options;
using static RulesEngineWrapper.presentation.Options.RulesEngineServiceOptions;
using RulesEngineWrapper.presentation.Queries;
using RulesEngine.Interfaces;

namespace RulesEngineWrapper.presentation;

public static class RuleEngineServicesExtensions
{
    //this needs to be improved greatly. primary functionality should be added for multiple rule source types. not just DB. as well as including the Action<DbContextOptionsBuilder> optionsAction inside of
    //the RulesEngineServiceOptions class. got a demo coming up. quick implemented this for it. currenlty only to be used with a database
     public static IServiceCollection AddRulesEngineWrapper<TContext>(this IServiceCollection services,
        Action<RulesEngineServiceOptions> optionsAction) where TContext : DbContext, IRulesEngineContext
    {
        services.AddServiceDefaults();

        RulesEngineServiceOptions options = new RulesEngineServiceOptions();
        optionsAction?.Invoke(options);

        switch(options.rulesEngineDataSource)
        {
            case RulesEngineDataSource.Database:
            services.AddDbContext<IRulesEngineContext ,TContext>(options.DbContextOptionsAction);
            services.AddScoped<IRulesEngine ,RulesEngine.RulesEngine>(p =>
            {
                var dbContext = p.GetRequiredService<TContext>();
                return new RulesEngine.RulesEngine(dbContext.Workflows.Include(w => w.Rules).ToArray(), options.reSettings);
            });
            services.AddScoped<IDataSourceRepository, DatabaseRulesEngineRepository>();
            break;

            case RulesEngineDataSource.File:
            services.AddScoped<IDataSourceRepository, FileSourceRepository>();
            break;
        }

        return services;
    }

    private static IServiceCollection AddServiceDefaults(this IServiceCollection services)
    {
        services.AddScoped<IRulesEngineQueries, RulesEngineQueries>();
        return services;
    }
}

