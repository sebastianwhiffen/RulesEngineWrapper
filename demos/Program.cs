using RulesEngineWrapper.Dashboard;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDirectoryBrowser();

builder.Services.AddRulesEngineWrapperDashboard();

var app = builder.Build();

app.UseRulesEngineDashboard(x => 
{
    x.UseUrlRewrite(@"^dishpup(?!/dist)");
});

var fileProvider = new EmbeddedFileProvider(Assembly.GetCallingAssembly());

app.UseStaticFiles();

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = "/files"
});

app.Run();