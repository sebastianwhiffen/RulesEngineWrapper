using Newtonsoft.Json;

namespace RulesEngineWrappers.UnitTest;
public class RulesEngineWrapperConstructionTests
{
    [Fact]
    public void InstantiateRulesEngineWrapperNoParams_ShouldWork()
    {
        Assert.NotNull(new RulesEngineWrapper());
    }

    [Fact]
    public void InstantiateRulesEngineWrapperWithJsonConfig_ShouldWork()
    {
        var jsonConfig = new string[] { JsonConvert.SerializeObject(RulesEngineWrapperUtility.NewWorkflow()) };
        var re = new RulesEngineWrapper(jsonConfig);

        Assert.NotNull(re);
        Assert.NotNull(re.GetAllRegisteredWorkflowNames());
    }

    [Fact]
    public void InstantiateRulesEngineWrapperWithWorkflows_ShouldWork()
    {
        var re = new RulesEngineWrapper(new[] { RulesEngineWrapperUtility.NewWorkflow() });

        Assert.NotNull(re);
        Assert.NotNull(re.GetAllRegisteredWorkflowNames());
    }
}