using System;
using Microsoft.Extensions.Logging;

namespace Core.UnitTests.Utils;

public class SimpleLoggerProvider : ILoggerProvider
{
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new SimpleLogger(categoryName);
    }
}

public class SimpleLogger : ILogger
{
    private readonly string _name;
    public SimpleLogger(string name) => _name = name;
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (formatter == null)
        {
            throw new ArgumentNullException(nameof(formatter));
        }

        var message = formatter(state, exception);

        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        message = $"{ logLevel }: {message}";

        if (exception != null)
        {
            message += Environment.NewLine + Environment.NewLine + exception;
        }
        
        Console.WriteLine($"[{_name}]: {message}");
    }
    
    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => true;
}