//these shouldnt be here. again. just need this working for demo
namespace RulesEngineWrapper.presentation;

public record ExecuteActionWorkflowCommand(string workflowName, string startingRule, object data);