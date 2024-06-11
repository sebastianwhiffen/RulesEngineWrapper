using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineWrapper.Dashboard;
public static class DashboardApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRulesEngineDashboard(
       this IApplicationBuilder app,
       Action<DashboardOptions> action = null!)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        var options = new DashboardOptions();

        action?.Invoke(options);

        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        app.MapWhen(context => context.Request.Path.StartsWithSegments(options.BaseUrl), builder =>
        {
            if (options.isLocal)
            {
                builder.UseSpa(spa =>
                {
                    spa.UseProxyToSpaDevelopmentServer(options.ApiUrl);
                });
            }
            else
            {
                builder.UseStaticFiles("/dist");

                // Redirect to options.BaseUrl/index.html
                builder.Use((context, next) =>
                {
                    context.Request.Path = options.BaseUrl + "/index.html";
                    return next();
                });
            }
        });

        return app;
    }
}
