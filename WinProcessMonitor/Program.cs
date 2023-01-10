namespace WinProcessMonitor;

internal class Program
{
    private static void Main(string[] args)
    {
        var processMonitor = new ProcessMonitor();
        
        if (processMonitor.StartProcessMonitoring(args))
        {
            Console.WriteLine("\nClosing WinProcessMonitor");
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("\nWinProcessMonitor closed due to error. Check logs");
            Environment.Exit(0);
        }
    }
}