// using Npgsql.EntityFrameworkCore.PostgreSQL;
// using RulesEngine.Models;
// using RulesEngineWrapper.presentation;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using RulesEngine.Data;
// using System.Reflection;

// namespace RulesEngineWrapper.UnitTest;
// public class RulesEngineWrapperTests : IDisposable
// {
//     private ServiceProvider _serviceProvider;
//     private RulesEngineContext _context;

//     //urgently need to move these out of the test class
//     public static IEnumerable<object[]> GetDatabaseConfigurations()
//     {
//         yield return new object[] { UseSqlServer };
//         yield return new object[] { UseSqlite };
//         // yield return new object[] { UsePostgreSQL };
//         // yield return new object[] { UseMySQL };
//         yield return new object[] { UseInMemoryDatabase };
//     }
//     private static Action<RulesEngineWrapperOptions> UseSqlServer = options =>
//         options.DbContextOptionsAction = builder =>
//         {
//             builder.UseSqlServer("Data Source=localhost;Initial Catalog=RulesEngineWrapperTest;Integrated Security=SSPI;MultipleActiveResultSets=True;TrustServerCertificate=true");
//             options.WrapperDbEnsureCreated = true;
//         };

//     private static Action<RulesEngineWrapperOptions> UseSqlite = options =>
//         options.DbContextOptionsAction = builder =>
//         {
//             var folder = Environment.SpecialFolder.LocalApplicationData;
//             var path = Environment.GetFolderPath(folder);
//             var dbPath = $"{path}{Path.DirectorySeparatorChar}RulesEngineWrapperTest.db";

//             builder.UseSqlite($"Data Source={dbPath}");
//             options.WrapperDbEnsureCreated = true;
//         };


//     private static Action<RulesEngineWrapperOptions> UsePostgreSQL = options =>
//         options.DbContextOptionsAction = builder =>
//         {
//             builder.UseNpgsql("Host=localhost;Database=RulesEngineWrapperTest;Username=postgres;Password=password;Trust Server Certificate=true;");
//             options.WrapperDbEnsureCreated = true;
//         };

//     private static Action<RulesEngineWrapperOptions> UseMySQL = options =>
//     options.DbContextOptionsAction = builder =>
//     {
//         builder.UseMySql("server=localhost;database=RulesEngineWrapperTest;user=root;password=password;",
//             new MySqlServerVersion(new Version(8, 0, 21)));
//         options.WrapperDbEnsureCreated = true;
//     };

//     private static Action<RulesEngineWrapperOptions> UseInMemoryDatabase = options =>
//     options.DbContextOptionsAction = builder =>
//     {
//         builder.UseInMemoryDatabase("InMemoryTestDb");
//         options.WrapperDbEnsureCreated = true;
//     };



//     private void SetUpServices(Action<RulesEngineWrapperOptions> configureOptions)
//     {
//         var services = new ServiceCollection();
//         services.AddLogging();
//         services.AddRulesEngineWrapper<RulesEngineContext>(configureOptions, Assembly.GetExecutingAssembly());

//         _serviceProvider = services.BuildServiceProvider();
//         _context = _serviceProvider.GetRequiredService<RulesEngineContext>();
//     }

//     public void Dispose()
//     {
//         _context.Database.EnsureDeleted();
//     }

//     [Theory]
//     [MemberData(nameof(GetDatabaseConfigurations))]
//     public async void AddWorkflowToDataSource_ShouldWorkCorrectly(Action<RulesEngineWrapperOptions> configureOptions)
//     {
//         // Arrange
//         SetUpServices(configureOptions);
//         var rulesEngineWrapper = _serviceProvider.GetRequiredService<IRulesEngineWrapper>();
//         var workflows = new List<Workflow>
//         {
//             new Workflow
//             {
//                 WorkflowName = "Test Workflow",
//                 Rules = new List<Rule>
//                 {
//                     new Rule
//                     {
//                         RuleName = "Test Rule",
//                         RuleExpressionType = RuleExpressionType.LambdaExpression,
//                         Expression = "1 < 5",
//                     }
//                 }
//             }
//         };

//         // Act
//         var result = await rulesEngineWrapper.AddWorkflowToDataSource(workflows);

//         // Assert
//         Assert.NotNull(result);

//         var workflow = await rulesEngineWrapper.GetWorkflowFromDataSource("Test Workflow");
//         Assert.Single(result);
//         Assert.Equal("Test Workflow", result.First().WorkflowName);
//     }
// }
