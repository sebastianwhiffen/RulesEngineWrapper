namespace RulesEngineWrapper.Dashboard;

public static class RulesEngineWrapperDashboardOptions
{
    public static IRulesEngineWrapperDashboard<RulesEngineWrapperDashboard> UseCustomBaseUrl(
    this IRulesEngineWrapperDashboard<RulesEngineWrapperDashboard> dashboard,
    string customBaseUrl
    )
    => dashboard.UseUrlRewrite<RulesEngineWrapperDashboard>(customBaseUrl);

    public static IRulesEngineWrapperDashboard<T> UseUrlRewrite<T>(
    this IRulesEngineWrapperDashboard<T> dashboard,
    string customBaseUrl
    ) where T : IRulesEngineWrapperDashboard
    {
        dashboard.Instance.DashboardOptions.CustomBaseUrl = customBaseUrl;

        return dashboard;
    }
}