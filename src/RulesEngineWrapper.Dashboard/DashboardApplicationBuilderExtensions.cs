﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using System.Net.Mime;

namespace RulesEngineWrapper.Dashboard;
public static class DashboardApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRulesEngineDashboard(this IApplicationBuilder app)
    {
        return app;
    }
}
