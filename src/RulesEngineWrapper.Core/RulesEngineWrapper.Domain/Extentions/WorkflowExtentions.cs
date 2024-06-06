using RulesEngine.Models;
namespace RulesEngineWrapper.Domain;

public static class WorkflowExtentions
{

    public static IEnumerable<Workflow> ToDTOs(this IEnumerable<WorkflowEntity> workflowEntities)
    {
        foreach (var workflowEntity in workflowEntities)
        {
            yield return workflowEntity.ToDTO();
        }
    }

    public static Workflow ToDTO(this WorkflowEntity workflowEntity)
    {
        return new Workflow 
        {
            WorkflowName = workflowEntity.WorkflowName,
            Rules = workflowEntity.Rules?.Select(r => r.ToDTO()).ToList(),
            GlobalParams = workflowEntity.GlobalParams?.Select(p => p.ToDTO()).ToList(),
            WorkflowsToInject = workflowEntity.WorkflowsToInject?.Select(w => w.WorkflowName).ToList()
        };
    }

    public static IEnumerable<WorkflowEntity> ToEntities(this IEnumerable<Workflow> workflows)
    {
        foreach (var workflow in workflows)
        {
            yield return workflow.ToEntity();
        }
    }

    public static WorkflowEntity ToEntity(this Workflow workflow)
    {
        return new WorkflowEntity 
        {
            WorkflowName = workflow.WorkflowName,
            Rules = workflow.Rules?.Select(r => r.ToEntity()).ToList(),
            GlobalParams = workflow.GlobalParams?.Select(p => p.ToEntity()).ToList(),
            WorkflowsToInject = workflow.WorkflowsToInject?.Select(w => new WorkflowEntity { WorkflowName = w }).ToList()
        };
    }
}