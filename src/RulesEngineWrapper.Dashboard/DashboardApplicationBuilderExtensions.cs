using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Rewrite;
//dont remove Microsoft.Extensions.FileProviders even if it says 'unused', its being used, but the compiler (ProductionBuild) flag is hiding it
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

            var rewriteOptions = new RewriteOptions();

#if ProductionBuild == true
            //for future reference, app.UseRewriter needs to go before UseStaticFiles like below,
            //this is sketch asf, careful.
            rewriteOptions.AddRewrite(@"^dashboard(?!/dist)", "dashboard/dist/index.html", true);

            app.UseRewriter(rewriteOptions);

            //look inside the csproj file for this project to see the packing of the embedded files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(DashboardApplicationBuilderExtensions).Assembly, "RulesEngineWrapper.Dashboard"),
            });

#else
            rewriteOptions.AddRewrite(@"^dashboard(?!/dist)", "dashboard/dist/index.html", true);

            app.UseRewriter(rewriteOptions);

            app.UseSpa(spa =>
            {
                spa.UseProxyToSpaDevelopmentServer(options.ApiUrl);
            });
#endif

            return app;
        }
    }
}
