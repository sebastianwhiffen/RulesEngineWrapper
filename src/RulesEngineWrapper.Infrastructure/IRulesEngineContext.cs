using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using RulesEngineWrapper.Domain;

public interface IRulesEngineWrapperContext
{
        public DbSet<WorkflowEntity> Workflows { get; set; }

        public DbSet<RuleEntity> Rules { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}