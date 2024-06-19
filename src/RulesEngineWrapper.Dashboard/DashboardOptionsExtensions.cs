namespace RulesEngineWrapper.Dashboard;

public static class RulesEngineWrapperDashboardOptions
{
    public static IRulesEngineWrapperDashboard<RulesEngineWrapperDashboard> UseCustomUrl(
    this IRulesEngineWrapperDashboard<RulesEngineWrapperDashboard> dashboard,
    string customUrl
    )
    => dashboard.UseUrlRewrite<RulesEngineWrapperDashboard>(customUrl);

    public static IRulesEngineWrapperDashboard<T> UseUrlRewrite<T>(
    this IRulesEngineWrapperDashboard<T> dashboard,
    string customUrl
    ) where T : IRulesEngineWrapperDashboard
    {
        dashboard.Instance.DashboardOptions.CustomUrl = customUrl;

        return dashboard;
    }
}