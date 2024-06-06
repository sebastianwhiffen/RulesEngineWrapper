using CodenameGenerator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RulesEngineWrapper.Presentation;

namespace RulesEngineWrapper.UnitTest;
public class LoggingTests
{
    [Fact]
    public void RulesEngineWrapperLoggerDisabled_ShouldNotLog()
    {
        // Arrange
        var generator = new Generator();

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string logFilePath = Path.Combine(folderPath, generator.Generate());

        StreamWriter logFileWriter = new StreamWriter(logFilePath, append: true);
        var logLevel = LogLevel.None;

        var settings = new RulesEngineWrapperSettings
        {
            UseDatabase = true,
            Logger = options => options.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(logLevel);
                builder.AddProvider(new CustomFileLoggerProvider(logFileWriter, logLevel));

            })
        };

        var re = new RulesEngineWrapper(settings);

        // Act
        using (logFileWriter)
        {
            re.AddWorkflow(RulesEngineWrapperUtility.NewWorkflow());
        }

        // Assert
        Assert.Empty(File.ReadAllText(logFilePath));
    }

    [Fact]
    public void RulesEngineWrapperLoggerEnabled_ShouldLog()
    {
        // Arrange
        var generator = new Generator();

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string logFilePath = Path.Combine(folderPath, generator.Generate());

        StreamWriter logFileWriter = new StreamWriter(logFilePath, append: true);
        var logLevel = LogLevel.Trace;

        var settings = new RulesEngineWrapperSettings
        {
            UseDatabase = true,
            Logger = options => options.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(logLevel);
                builder.AddProvider(new CustomFileLoggerProvider(logFileWriter, logLevel));
                builder.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
            })
        };

        var re = new RulesEngineWrapper(settings);

        // Act
        using (logFileWriter)
        {
            re.AddWorkflow(RulesEngineWrapperUtility.NewWorkflow());
        }
        // Assert
        Assert.Contains("Adding workflow", File.ReadAllText(logFilePath));
    }

}