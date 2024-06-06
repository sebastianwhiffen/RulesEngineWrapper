using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RulesEngineWrapper.Presentation;

namespace RulesEngineWrapper.Dashboard;
public static class DashboardApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRulesEngineDashboard(
        this IApplicationBuilder app,
        string defaultPath = "/rulesengine",
        DashboardOptions? options = null)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));
        if (defaultPath == null) throw new ArgumentNullException(nameof(defaultPath));

        RuleEngineServicesExtensions.ThrowIfNotConfigured(app.ApplicationServices);

        options = options ?? app.ApplicationServices.GetService<DashboardOptions>() ?? new DashboardOptions();
        
        app.Map(new PathString(defaultPath) , x => x.UseMiddleware<RulesEngineDashboardMiddleware>(options)); 

        return app;
    }
}
