using Microsoft.Extensions.DependencyInjection;
using RulesEngineWrapper.presentation;
using RulesEngine.Data;
using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;

namespace RulesEngineWrapper.UnitTest
{
    public class RulesEngineDatabaseSourceTests
    {
        private readonly ServiceCollection _services;
        private readonly ServiceProvider _serviceProvider;

        public RulesEngineDatabaseSourceTests()
        {
            _services = new ServiceCollection();
            AddWrapperDbSqlite();
            _serviceProvider = _services.BuildServiceProvider();
        }

        [Fact]
        public async void AddWorkflowToDataSource_ShouldAddWorkflowToDatabase()
        {
            // Arrange
            var rulesEngineWrapper = _serviceProvider.GetRequiredService<IRulesEngineWrapper>();
            var workflow = new Workflow
            {
                WorkflowName = "TestWorkflow",
                Rules = new List<Rule>
                {
                    new Rule
                    {
                        RuleName = "TestRule",
                        RuleExpressionType = RuleExpressionType.LambdaExpression,
                        Expression = "x => x > 5",
                    }
                }
            };

            // Act
            var result = await rulesEngineWrapper.AddWorkflowToDataSource(new List<Workflow> { workflow });

            // Assert
            Assert.Contains(result, x => x.WorkflowName == "TestWorkflow");
            Assert.Contains(result.First().Rules, x => x.RuleName == "TestRule");
        }

        private void AddWrapperDbSqlite()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}RulesEngineWrapperTest.db";

            _services.AddRulesEngineWrapper<RulesEngineContext>(options =>
            {
                options.WrapperDbEnsureCreated = true;
                options.DbContextOptionsAction = dbContextOptionsBuilder =>
                {
                    dbContextOptionsBuilder.UseSqlite($"Data Source={dbPath}");
                };
            });
        }
    }
}