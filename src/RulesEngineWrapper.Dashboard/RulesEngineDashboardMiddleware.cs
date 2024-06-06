using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace RulesEngineWrapper.Dashboard;

public class RulesEngineDashboardMiddleware
{
    private readonly RequestDelegate _next;
    private readonly DashboardOptions _options;

    public RulesEngineDashboardMiddleware( 
        RequestDelegate next,
        DashboardOptions options)
    {
        if (next == null) throw new ArgumentNullException(nameof(next));
        if (options == null) throw new ArgumentNullException(nameof(options));

        _next = next;
        _options = options;
    }

    public async Task Invoke(HttpContext httpContext)
    {


    }
}