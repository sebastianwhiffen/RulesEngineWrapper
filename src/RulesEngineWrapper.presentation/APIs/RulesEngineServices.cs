
using RulesEngineWrapper.presentation;

public class RulesEngineServices(
    IRulesEngineQueries queries,
    // VERY BAD. USE MEDIATOR SOON. THIS IS JUST FOR THE UPCOMING DEMO.
    IDataSourceRepository repository
    )
{
    public IRulesEngineQueries Queries { get; } = queries;

    public IDataSourceRepository Repository { get; } = repository;
}
