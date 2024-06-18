namespace RulesEngineWrapper.Dashboard;

public static class RulesEngineWrapperDashboardOptions
{
    public static IRulesEngineWrapperDashboard<RulesEngineWrapperDashboard> UseCustomUrl(
    this IRulesEngineWrapperDashboard<RulesEngineWrapperDashboard> dashboard,
    string customUrl
    )
    => dashboard.UseCustomUrl<RulesEngineWrapperDashboard>(customUrl);

    public static IRulesEngineWrapperDashboard<T> UseCustomUrl<T>(
    this IRulesEngineWrapperDashboard<T> dashboard,
    string customUrl
    ) where T : IRulesEngineWrapperDashboard
    {
        dashboard.Instance.DashboardOptions.BaseUrl = customUrl;

        return dashboard;
    }
}