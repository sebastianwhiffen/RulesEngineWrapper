using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;

namespace RulesEngineWrappers.Presentation
{
    public class RulesEngineWrapperSettings
    {
        public ReSettings ReSettings { get; set; } = new ReSettings();
        public Action<DbContextOptionsBuilder> DbContextOptionsAction { get; set; } = options => options.UseInMemoryDatabase("RulesEngineWrapper");
        public bool WrapperDbEnsureCreated { get; set; } = false;
        public bool UseDatabase { get; set; } = false;
    }
}
