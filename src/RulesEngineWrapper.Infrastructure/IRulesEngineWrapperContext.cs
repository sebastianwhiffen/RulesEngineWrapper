using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using RulesEngineWrappers.Domain;

public interface IRulesEngineWrapperContext : IUnitOfWork
{
        public DbSet<WorkflowEntity> Workflows { get; set; }
        public DbSet<RuleEntity> Rules { get; set; }
}