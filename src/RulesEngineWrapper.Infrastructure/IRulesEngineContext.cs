using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;

public interface IRulesEngineWrapperContext
{
        public DbSet<Workflow> Workflows { get; set; }

        public DbSet<Rule> Rules { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}