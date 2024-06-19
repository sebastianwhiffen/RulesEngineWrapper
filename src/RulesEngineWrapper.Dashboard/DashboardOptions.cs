using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace RulesEngineWrapper.Dashboard;

public class DashboardOptions
{
    //this is the path to the embedded files, this is the path to the index.html file
    //if you wish to change this you also need to change this hardcoded value at these places:
    //RulesEngineWrapper.Dashboard.csproj
    //vite.config.ts (also re-run npm build)
    //here
    public const string embeddedFilePath = "rulesEngineWrapper-dashboard/dist/index.html";

    public string CustomUrl { get; set; } = "rulesEngine-dashboard";

    //for use inside development mode only, need to figure out a way to obfuscate this
    public string DevelopmentServerUrl { get; internal set; } = "http://localhost:5173/";

}