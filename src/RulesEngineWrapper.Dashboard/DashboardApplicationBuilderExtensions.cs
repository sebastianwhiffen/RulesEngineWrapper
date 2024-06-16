using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace RulesEngineWrapper.Dashboard
{
    public static class DashboardApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRulesEngineDashboard(
            this IApplicationBuilder app,
            Action<DashboardOptions> action = null!)
        {
            var options = new DashboardOptions();
            action?.Invoke(options);

            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

#if !ProductionBuild
            app.MapWhen(context => context.Request.Path.StartsWithSegments(options.BaseUrl), builder =>
            {
                builder.UseSpa(spa =>
                {
                    spa.UseProxyToSpaDevelopmentServer(options.ApiUrl);
                });
            });
#else
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(DashboardApplicationBuilderExtensions).Assembly, "RulesEngineWrapper.Dashboard"),
            });
#endif
            return app;
        }
    }
}
