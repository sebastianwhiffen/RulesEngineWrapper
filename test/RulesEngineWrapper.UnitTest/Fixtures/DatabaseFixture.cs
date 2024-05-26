// using DotNet.Testcontainers.Containers;
// using Testcontainers.MsSql;

// public class TestContainersFixture : IAsyncLifetime
// {
//     public static List<IContainer> _containers { get; private set; } = new List<IContainer>();
//     private static bool isInitialized = false;
//     private static readonly object initLock = new object();

//     public static async Task EnsureInitializedAsync()
//     {
//         if (!isInitialized)
//         {
//             if (!isInitialized)
//             {
//                 await InitializeDatabases();
//                 isInitialized = true;
//             }
//         }
//     }

//     public Task InitializeAsync() => EnsureInitializedAsync();

//     public Task DisposeAsync()
//     {
//         // Dispose of containers and other resources
//         return Task.CompletedTask;
//     }
//     private static async Task InitializeDatabases()
//     {
//         var containers = new List<IContainer>()
//         {
//             new MsSqlBuilder().Build(),
//             // new MySqlBuilder().Build(),
//             // new PostgreSqlBuilder().Build()
//         };

//         foreach (IContainer container in containers)
//         {
//             await container.StartAsync();
//             _containers.Add(container);
//         }
//     }

//     [CollectionDefinition("Database collection")]
//     public class DatabaseCollection : ICollectionFixture<TestContainersFixture>
//     {
//         // This class has no code, and is never created. Its purpose is simply
//         // to be the place to apply [CollectionDefinition] and all the
//         // ICollectionFixture<> interfaces.
//     }

// }

