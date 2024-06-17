namespace RulesEngineWrapper.Dashboard;

public static class DashBoardOptionExtensions
{
    public static IRulesEngineWrapperDashboard<T> UseCustomUrl<T>(
    this IRulesEngineWrapperDashboard<T> dashboard,
    string customUrl
    ) where T : IRulesEngineWrapperDashboard
    {
        dashboard.Instance.DashboardOptions.BaseUrl = customUrl;

        return dashboard;
    }

}