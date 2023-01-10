using System.Diagnostics;

namespace WinProcessMonitor
{
    public class ProcessMonitor
    {
        public bool StartProcessMonitoring(IReadOnlyList<string> args)
        {
            var logger = new Logger();

            if (args.Count == 3)
            {
                try
                {
                    var prcName = !string.IsNullOrEmpty(args[0])
                        ? args[0]
                        : throw new Exception("Invalid Process name");
                    var maxLifetime = !string.IsNullOrEmpty(args[1])
                        ? int.Parse(args[1])
                        : throw new Exception("Invalid Process lifetime");
                    var monitoringFrequency = !string.IsNullOrEmpty(args[2])
                        ? int.Parse(args[2])
                        : throw new Exception("Invalid process monitoring frequency");

                    if (maxLifetime == 0 || monitoringFrequency == 0)
                        return true;

                    return CheckAndExitProcess(prcName, maxLifetime, monitoringFrequency, logger);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    logger.WriteLog($"{DateTime.Now}: {e.Message}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid number of arguments");
                logger.WriteLog($"{DateTime.Now}: Invalid number of arguments");
                return false;
            }
        }

        private bool CheckAndExitProcess(string prcName, int maxLifetime, int monitoringFrequency, Logger logger)
        {
            var t = new Thread(() =>
            {
                Console.WriteLine($"Monitoring: {prcName} started");

                var minuteInSeconds = maxLifetime * 60;
                var sleepTime = monitoringFrequency * 60 * 1000;

                while (true)
                {
                    var allProcess = Process.GetProcesses();

                    foreach (var process in allProcess)
                    {
                        if (process.ProcessName.ToLower() == prcName &&
                            (DateTime.Now - process.StartTime).TotalSeconds >= minuteInSeconds)
                        {
                            var logMessage =
                                $"{DateTime.Now}: Process {prcName}({process.Id}) is running for more than {maxLifetime} minutes";
                            Console.WriteLine(logMessage);
                            logger.WriteLog(logMessage);
                            process.Kill();
                        }
                    }
                    Thread.Sleep(sleepTime);
                }
            });

            t.Start();

            Console.WriteLine("Press 'Q' key to exit ");

            while (true)
            {
                if (Console.ReadKey(true).Key != ConsoleKey.Q) continue;

                return true;
            }
        }
    }
}
