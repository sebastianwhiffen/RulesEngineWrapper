namespace RulesEngineWrapper.Dashboard;

public class DashboardOptions
{
    public string BaseUrl { get; internal set; } = "/dashboard";
    
    //for use inside development mode only, need to figure out a way to obfuscate this
    public string ApiUrl { get; internal set; } = "http://localhost:5173/";

}