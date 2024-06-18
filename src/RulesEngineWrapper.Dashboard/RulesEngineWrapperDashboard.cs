using RulesEngineWrapper.Dashboard;

public class RulesEngineWrapperDashboard : IRulesEngineWrapperDashboard<RulesEngineWrapperDashboard>
{
    public DashboardOptions DashboardOptions { get; set; } = new DashboardOptions();

    public RulesEngineWrapperDashboard Instance => this;
}

public interface IRulesEngineWrapperDashboard
{
    DashboardOptions DashboardOptions { get; set; }
}

public interface IRulesEngineWrapperDashboard<out T> : IRulesEngineWrapperDashboard
{
    T Instance { get; }
}