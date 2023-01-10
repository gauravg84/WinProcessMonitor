using System.Drawing;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace WinProcessMonitor.Tests
{
    public class ProcessMonitorTests
    {
        [Fact]
        public void StartProcessMonitoring_IncorrectArgumentType_ReturnsFalse()
        {
            var args = new[]
            {
                "firefox",
                "a",
                "1"
            };

            var processMonitor = new ProcessMonitor();
            var result = processMonitor.StartProcessMonitoring(args);

            Assert.True(!result);
        }

        [Fact]
        public void StartProcessMonitoring_IncorrectNumberOfArguments_ReturnsFalse()
        {
            var args = new[]
            {
                "firefox"
            };

            var processMonitor = new ProcessMonitor();
            var result = processMonitor.StartProcessMonitoring(args);

            Assert.True(!result);
        }

        [Fact]
        public void StartProcessMonitoring_CorrectNumberOfArguments_ReturnsFalse()
        {
            var args = new[]
            {
                "firefox",
                "0",
                "0"
            };
            var processMonitor = new ProcessMonitor();
            var result = processMonitor.StartProcessMonitoring(args);

            Assert.True(result);
        }
    }
}