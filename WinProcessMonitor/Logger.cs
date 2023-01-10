using System.Configuration;

namespace WinProcessMonitor
{
    public class Logger
    {
        private readonly string? _logPath;

        public Logger()
        {
            _logPath = ConfigurationManager.AppSettings["logPath"];
        }

        public void WriteLog(string logMessage)
        {
            if (!string.IsNullOrEmpty(_logPath))
            {
                using var sw = new StreamWriter(_logPath, true);
                sw.WriteLine($"{logMessage}");
            }
        }
    }
}
