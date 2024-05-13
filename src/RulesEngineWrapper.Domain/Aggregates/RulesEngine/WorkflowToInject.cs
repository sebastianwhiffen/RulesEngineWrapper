namespace RulesEngineWrappers.Domain;

public class WorkflowToInject : Entity
{
    public Guid WorkflowId { get; set; }
    public Guid InjectedWorkflowId { get; set; }

}