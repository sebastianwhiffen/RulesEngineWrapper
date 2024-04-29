using Microsoft.EntityFrameworkCore;
using RulesEngine.Data;
using RulesEngine.Models;

namespace RulesEngineWrapper.presentation
{
    public class RulesEngineWrapperOptions
    {
        public ReSettings reSettings { get; set; } = new ReSettings();
        public Action<DbContextOptionsBuilder> DbContextOptionsAction  { get; set; }
        public bool WrapperDbEnsureCreated {get; set;}
        public List<Workflow> workflowsToInit {get; set;}

    }
}