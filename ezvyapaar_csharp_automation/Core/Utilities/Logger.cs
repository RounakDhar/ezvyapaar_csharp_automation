// 5. Logger
namespace ezvyapaar_csharp_automation.core.Utilities
{
    using System;
    using System.IO;
    using ezvyapaar_csharp_automation.core.Configuration;

    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }

    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string _logFilePath;
        private static readonly LogLevel _minLogLevel;

        static Logger()
        {
            string logDir = "Logs";
            Directory.CreateDirectory(logDir);
            _logFilePath = Path.Combine(logDir, $"TestLog_{DateTime.Now:yyyyMMdd}.log");

            _minLogLevel = Enum.TryParse<LogLevel>(ConfigManager.Instance.LogLevel, true, out var level)
                ? level
                : LogLevel.Info;
        }

        public static void Debug(string message) => Log(LogLevel.Debug, message);
        public static void Info(string message) => Log(LogLevel.Info, message);
        public static void Warning(string message) => Log(LogLevel.Warning, message);
        public static void Error(string message) => Log(LogLevel.Error, message);
        public static void Fatal(string message) => Log(LogLevel.Fatal, message);

        private static void Log(LogLevel level, string message)
        {
            if (level < _minLogLevel)
                return;

            string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] [{System.Threading.Thread.CurrentThread.ManagedThreadId}] {message}";

            lock (_lock)
            {
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }

            Console.WriteLine(logEntry);
        }
    }
}
