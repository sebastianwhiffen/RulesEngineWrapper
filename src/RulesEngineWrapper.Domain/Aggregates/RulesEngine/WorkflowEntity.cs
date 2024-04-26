using CodenameGenerator;

namespace RulesEngineWrapper.Domain;

public class WorkflowEntity : Entity 
{
    public string WorkflowName { get; set; } = new Generator().Generate();
    public ICollection<WorkflowEntity>? WorkflowsToInject { get; set; }
    public IEnumerable<ScopedParamEntity>? GlobalParams { get; set; }
    public IEnumerable<RuleEntity>? Rules { get; set; }
}
