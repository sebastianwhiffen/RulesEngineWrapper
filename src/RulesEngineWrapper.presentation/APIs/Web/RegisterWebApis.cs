using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace RulesEngineWrapper.presentation.APIs
{
    public static class RegisterWebApis
    {
        public static IEndpointRouteBuilder RegisterRulesEngineWebApis(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/test", () =>
            {
                var value = Enumerable.Range(1, 5);
                return value;
            })
            .WithName("test")
            .WithOpenApi();

            return endpoints;

        }
    }
}