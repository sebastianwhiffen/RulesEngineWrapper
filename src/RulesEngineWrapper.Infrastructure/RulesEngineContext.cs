using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RulesEngineWrapper.Domain;
using RulesEngineWrapper.Infrastructure;

namespace RulesEngine.Data
{
    public class RulesEngineContext : DbContext, IRulesEngineWrapperContext
    {
        private readonly IMediator _mediator;
        public RulesEngineContext(DbContextOptions<RulesEngineContext> options, IMediator mediator ) : base(options)
        {
            _mediator = mediator;
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
            
             _ = await base.SaveChangesAsync(cancellationToken);

            await _mediator.DispatchDomainEventsAsync(this);

            return true;
        }

        //ONLY to be used for nested items where it is not possible to know the depth of the object, making it impossible to explicitly load the nested items.
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

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

            modelBuilder.Entity<ActionInfoEntity>()
            .Property(e => e.Context)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, object>>(v)
            );
        }
    }
}