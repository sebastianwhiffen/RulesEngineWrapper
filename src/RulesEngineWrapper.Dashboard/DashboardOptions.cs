namespace RulesEngineWrapper.Dashboard;

public class DashboardOptions
{
    public string BaseUrl { get; set; } = "/dashboard";

    public string ApiUrl { get; set; } = "http://localhost:5173/";

    public bool isLocal = bool.Parse(Environment.GetEnvironmentVariable("IS_LOCAL") ?? "false");

}