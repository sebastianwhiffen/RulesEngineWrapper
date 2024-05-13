using MediatR;
using Microsoft.EntityFrameworkCore;
using RulesEngine.Data;
using RulesEngine.Models;

namespace RulesEngineWrappers.presentation
{
    public class RulesEngineWrapperSettings
    {
        public ReSettings reSettings { get; set; } = new ReSettings();
        public Action<DbContextOptionsBuilder> DbContextOptionsAction { get; set; } = options => options.UseInMemoryDatabase("RulesEngineWrapper");
        public bool WrapperDbEnsureCreated { get; set; }
        public Type RulesEngineWrapperContextType { get; set; } = typeof(RulesEngineWrapperContext);
        public IMediator mediator { get; set; } = null;
    }
}