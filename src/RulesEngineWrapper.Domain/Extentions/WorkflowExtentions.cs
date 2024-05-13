using RulesEngine.Models;
namespace RulesEngineWrappers.Domain;

public static class WorkflowExtentions
{

    public static IEnumerable<Workflow> ToWorkflows(this IEnumerable<WorkflowEntity> workflowEntities)
    {
        foreach (var workflowEntity in workflowEntities)
        {
            yield return workflowEntity.ToWorkflow();
        }
    }

    public static Workflow ToWorkflow(this WorkflowEntity workflowEntity)
    {
        return new Workflow 
        {
            WorkflowName = workflowEntity.WorkflowName,
            Rules = workflowEntity.Rules?.Select(r => r.ToRule()).ToList(),
            GlobalParams = workflowEntity.GlobalParams?.Select(p => p.ToScopedParam()).ToList(),
            WorkflowsToInject = workflowEntity.WorkflowsToInject?.Select(w => w.WorkflowName).ToList()
        };
    }

    public static IEnumerable<WorkflowEntity> ToWorkflowEntities(this IEnumerable<Workflow> workflows)
    {
        foreach (var workflow in workflows)
        {
            yield return workflow.ToWorkflowEntity();
        }
    }

    public static WorkflowEntity ToWorkflowEntity(this Workflow workflow)
    {
        return new WorkflowEntity 
        {
            WorkflowName = workflow.WorkflowName,
            Rules = workflow.Rules?.Select(r => r.ToRuleEntity()).ToList(),
            GlobalParams = workflow.GlobalParams?.Select(p => p.ToScopedParamEntity()).ToList(),
            WorkflowsToInject = workflow.WorkflowsToInject?.Select(w => new WorkflowEntity { WorkflowName = w }).ToList()
        };
    }
}