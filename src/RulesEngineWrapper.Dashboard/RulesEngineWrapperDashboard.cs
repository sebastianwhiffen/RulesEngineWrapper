using RulesEngineWrapper.Dashboard;

public class RulesEngineWrapperDashboard : IRulesEngineWrapperDashboard
{
    public DashboardOptions DashboardOptions { get; set; } = new DashboardOptions();
}

public interface IRulesEngineWrapperDashboard
{
    DashboardOptions DashboardOptions { get; set; }
}

public interface IRulesEngineWrapperDashboard<out T> : IRulesEngineWrapperDashboard
{
    T Instance { get; }
}