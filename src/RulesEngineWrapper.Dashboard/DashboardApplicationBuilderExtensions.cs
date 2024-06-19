using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Rewrite;
//don't remove Microsoft.Extensions.FileProviders even if it says 'unused', its being used, but the compiler (ProductionBuild) flag is hiding it
using Microsoft.Extensions.FileProviders;

namespace RulesEngineWrapper.Dashboard
{
    public static class DashboardApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRulesEngineDashboard(
            this IApplicationBuilder app,
            Action<IRulesEngineWrapperDashboard<RulesEngineWrapperDashboard>> action = null!)
            => app.UseRulesEngineDashboard<RulesEngineWrapperDashboard>(action);

        public static IApplicationBuilder UseRulesEngineDashboard<T>(
        this IApplicationBuilder app,
        Action<IRulesEngineWrapperDashboard<T>> action = null!)
        {
            //this requires the AddRulesEngineWrapperDashboard call to actually return something
            var instance = app.ApplicationServices.GetRequiredService<IRulesEngineWrapperDashboard<T>>();

            action?.Invoke(instance);

            var rewriteOptions = new RewriteOptions();

#if !ProductionBuild == true
            //for future reference, app.UseRewriter needs to go before UseStaticFiles like below,
            //this is sketch asf, careful.
            rewriteOptions.AddRewrite(instance.DashboardOptions.CustomUrl, DashboardOptions.embeddedFilePath, true);

            app.UseRewriter(rewriteOptions);

            //look inside the csproj file for this project to see the packing of the embedded files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(DashboardApplicationBuilderExtensions).Assembly)
            });
#else
            app.UseSpa(spa => spa.UseProxyToSpaDevelopmentServer(instance.DashboardOptions.ApiUrl));
#endif

            return app;
        }
    }
}
