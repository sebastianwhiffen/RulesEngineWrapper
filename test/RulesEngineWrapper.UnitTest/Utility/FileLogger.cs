using Microsoft.Extensions.Logging;
using System;
using System.IO;

public class CustomFileLoggerProvider : ILoggerProvider
{
    private readonly StreamWriter _logFileWriter;
    private readonly LogLevel _minLogLevel;

    public CustomFileLoggerProvider(StreamWriter logFileWriter, LogLevel minLogLevel = LogLevel.Information)
    {
        _logFileWriter = logFileWriter ?? throw new ArgumentNullException(nameof(logFileWriter));
        _minLogLevel = minLogLevel;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new CustomFileLogger(categoryName, _logFileWriter, _minLogLevel);
    }

    public void Dispose()
    {
        _logFileWriter?.Dispose();
    }
}


public class CustomFileLogger : ILogger
{
    private readonly string _categoryName;
    private readonly StreamWriter _logFileWriter;
    private readonly LogLevel _minLogLevel;

    public CustomFileLogger(string categoryName, StreamWriter logFileWriter, LogLevel minLogLevel)
    {
        _categoryName = categoryName;
        _logFileWriter = logFileWriter;
        _minLogLevel = minLogLevel;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _minLogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var message = formatter(state, exception);
        _logFileWriter.WriteLine($"[{logLevel}] [{_categoryName}] {message}");
        _logFileWriter.Flush();
    }
}
