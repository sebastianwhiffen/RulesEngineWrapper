using RulesEngineWrapper.Dashboard;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDirectoryBrowser();

builder.Services.AddRulesEngineWrapperDashboard();

var app = builder.Build();

app.UseRulesEngineDashboard(x => 
{
    x.UseCustomUrl("ffs");
});

var fileProvider = new EmbeddedFileProvider(Assembly.GetCallingAssembly());

app.UseStaticFiles();

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = "/files"
});

app.Run();