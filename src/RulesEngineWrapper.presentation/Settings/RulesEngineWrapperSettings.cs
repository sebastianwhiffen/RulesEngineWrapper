using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace RulesEngineWrappers.Presentation
{
    public class RulesEngineWrapperSettings
    {
        public ReSettings ReSettings { get; set; } = new ReSettings();
        public Action<DbContextOptionsBuilder> DbContextOptionsAction { get; set; } = options => options.UseInMemoryDatabase("RulesEngineWrapper");
        public bool WrapperDbEnsureCreated { get; set; } = false;
        public bool UseDatabase { get; set; } = false;
        public Func<IServiceCollection, IServiceCollection> Logger { get; set; } = options => options.AddLogging(builder =>
            {
                builder.AddConsole();
            });
    }
}
