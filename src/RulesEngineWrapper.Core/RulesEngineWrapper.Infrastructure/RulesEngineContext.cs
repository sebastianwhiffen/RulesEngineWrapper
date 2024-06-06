using Microsoft.EntityFrameworkCore;
using RulesEngineWrapper.Domain;

namespace RulesEngine.Data
{
    public class RulesEngineWrapperContext : DbContext, IRulesEngineWrapperContext
    {
        public RulesEngineWrapperContext(DbContextOptions<RulesEngineWrapperContext> options) : base(options)
        {
        }

        public DbSet<WorkflowEntity> Workflows { get; set; }
        public DbSet<WorkflowToInject> WorkflowToInject { get; set; }
        public DbSet<RuleEntity> Rules { get; set; }
        public DbSet<Rule_Rules> RuleRules { get; set; }
        public DbSet<RuleActionEntity> RuleActions { get; set; }
        public DbSet<ScopedParamEntity> ScopedParams { get; set; }
        public DbSet<ActionInfoEntity> ActionInfos { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // await _mediator.DispatchDomainEventsAsync(this);

            _ = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkflowEntity>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.WorkflowName).IsUnique();
                e.HasMany(e => e.WorkflowsToInject);
                // .WithOne(e => e.Workflow).HasForeignKey(e => e.WorkflowId);
            });

            modelBuilder.Entity<WorkflowToInject>(e =>
            {
                e.HasKey(e => new { e.WorkflowId, e.InjectedWorkflowId });
            });

            modelBuilder.Entity<RuleEntity>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.RuleName).IsUnique();
                e.HasMany(e => e.Rules);
            });

            modelBuilder.Entity<ActionInfoEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasMany(e => e.Contexts)
                    .WithOne(c => c.ActionInfoEntity)
                    .HasForeignKey(c => c.Id);
            });

            modelBuilder.Entity<ActionInfoContext>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Key).IsRequired();
                entity.Property(c => c.Value).IsRequired();
            });
        }
    }
}