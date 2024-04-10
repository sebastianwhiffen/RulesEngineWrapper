using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using RulesEngine.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RulesEngineWrapper.presentation.APIs
{
    public static class WebApis
    {
        private const string Queries = "Queries";
        private const string Commands = "Commands";
        public static IEndpointRouteBuilder RegisterRulesEngineWebApis(this IEndpointRouteBuilder endpoints)
        {
            #region queries

            endpoints.MapGet("/getAllWorkflows", GetAllWorkflows).WithTags(Queries).WithOpenApi();
            endpoints.MapGet("getWorkflow/{workflowName}", GetWorkflow).WithTags(Queries).WithOpenApi();
            endpoints.MapGet("/getAllRules", GetAllRules).WithTags(Queries).WithOpenApi();
            endpoints.MapGet("getRule/{ruleName}", GetRule).WithTags(Queries).WithOpenApi();

            #endregion

            #region commands

            endpoints.MapPost("executeAllRules/{workflowName}", ExecuteAllRules).WithTags(Commands).WithOpenApi();
            endpoints.MapPost("addOrUpdateWorkflow", AddOrUpdateWorkflow).WithTags(Commands).WithOpenApi();
            endpoints.MapDelete("removeWorkflowByName", RemoveWorkflowByName).WithTags(Commands).WithOpenApi();

            #endregion

            return endpoints;
        }

        #region queries

        public static async Task<Results<Ok<IEnumerable<Workflow>>, BadRequest<string>>> GetAllWorkflows(
            [AsParameters] RulesEngineServices services)
        {
            return TypedResults.Ok(await services.Queries.GetAllWorkflowsAsync());
        }

        public static async Task<Results<Ok<Workflow>, BadRequest<string>>> GetWorkflow(
            string workflowName,
            [AsParameters] RulesEngineServices services
            )
        {
            return TypedResults.Ok(await services.Queries.GetWorkflowAsync(workflowName));
        }

        public static async Task<Results<Ok<IEnumerable<Rule>>, BadRequest<string>>> GetAllRules(
            [AsParameters] RulesEngineServices services
            )
        {
            return TypedResults.Ok(await services.Queries.GetAllRulesAsync());
        }

        public static async Task<Results<Ok<Rule>, BadRequest<string>>> GetRule(
            string ruleName,
            [AsParameters] RulesEngineServices services
            )
        {
            return TypedResults.Ok(await services.Queries.GetRuleAsync(ruleName));
        }
        #endregion

        #region commands
        //Rule Results is self referencing. this causes MASSIVE json loops. breaks swagger UI, that is why return type is 'object'. Cant let swagger know the Type. gross
        public static async Task<Results<Ok<IEnumerable<dynamic>>, BadRequest<string>>> ExecuteAllRules(
            [AsParameters] RulesEngineServices services,
            ExecuteAllRulesCommand executeAllRulesCommand
        )
        {
            var result = await services.Repository.RunAllRulesAsync(executeAllRulesCommand);
            return TypedResults.Ok<IEnumerable<dynamic>>(result);
        }

        public static async Task<Results<Ok<Workflow>, BadRequest<string>>> AddOrUpdateWorkflow(
            [AsParameters] RulesEngineServices services,
            Workflow workflow
            )
        {
            return TypedResults.Ok(await services.Repository.AddOrUpdateWorkflows(workflow));
        }

        public static async Task<Results<Ok<bool>, BadRequest<string>>> RemoveWorkflowByName(
            [AsParameters] RulesEngineServices services,
            string workflowName
        )
        {
            return TypedResults.Ok(await services.Repository.RemoveWorkflowByNameAsync(workflowName));
        }

        #endregion
    }
}
