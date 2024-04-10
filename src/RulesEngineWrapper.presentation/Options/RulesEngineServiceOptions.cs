using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Data;
using RulesEngine.Models;

namespace RulesEngineWrapper.presentation.Options
{
    public class RulesEngineServiceOptions
    {
        public RulesEngineDataSource rulesEngineDataSource { get; set; } = RulesEngineDataSource.None;

        public ReSettings reSettings { get; set; } = new ReSettings();

        public Action<DbContextOptionsBuilder> DbContextOptionsAction  { get; set; }
        public Type DbContextType { get; private set; } = typeof(RulesEngineContext);

        public enum RulesEngineDataSource
        {
            Database,
            File,
            None
        }

      public void RegisterDbContext<TContext>(Action<DbContextOptionsBuilder> optionsAction) where TContext : DbContext
    {
        this.DbContextType = typeof(TContext);
        this.DbContextOptionsAction = optionsAction;
    }
    }
}