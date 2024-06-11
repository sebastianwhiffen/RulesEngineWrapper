
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using RulesEngineWrapper.Dashboard;
using RulesEngineWrapper.Presentation;

namespace RulesEngineWrapper.UnitTest;
public class DashboardTests
{

    [Fact]
    public void UseRulesEngineDashboard_ShouldThrowException_WhenRulesEngineNotConfigured() =>
        Assert.Throws<InvalidOperationException>(() => new ApplicationBuilder(new ServiceCollection().BuildServiceProvider()).UseRulesEngineDashboard());


    [Fact]
    public void UseRulesEngineDashboard_ShouldRegisterMiddleware()
    {
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        app.UseRulesEngineDashboard();

    }


}