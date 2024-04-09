using Microsoft.AspNetCore.Builder;
using RulesEngineWrapper.presentation.Options;
using RulesEngineWrapper.presentation.APIs;

namespace RulesEngineWrapper.presentation;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRulesEngineWebAPIs(this IApplicationBuilder app, RulesEngineWrapperPresentationOptions options)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.RegisterWrapperWebApis();
        });

        return app;
    }

    public static IApplicationBuilder UseRulesEngineWebAPIs(this IApplicationBuilder app, Action<RulesEngineWrapperPresentationOptions>? optionsAction = null)
    {
        var options = new RulesEngineWrapperPresentationOptions();
        optionsAction?.Invoke(options);

        return app.UseRulesEngineWebAPIs(options);
    }
}
