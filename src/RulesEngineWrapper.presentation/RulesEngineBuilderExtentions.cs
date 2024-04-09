using Microsoft.AspNetCore.Builder;
using RulesEngineWrapper.presentation.Options;
using RulesEngineWrapper.presentation.APIs;

namespace RulesEngineWrapper.presentation;

public static class RulesEngineBuilderExtensions
{
    public static IApplicationBuilder UseRulesEngineWrapper(this IApplicationBuilder app, RulesEngineBuilderOptions options)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.RegisterRulesEngineWebApis();
        });

        return app;
    }

    public static IApplicationBuilder UseRulesEngineWrapper(this IApplicationBuilder app, Action<RulesEngineBuilderOptions>? optionsAction = null)
    {
        var options = new RulesEngineBuilderOptions();
        optionsAction?.Invoke(options);

        return app.UseRulesEngineWrapper(options);
    }
}
