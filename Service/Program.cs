using DialogDisplay;
using ProcessMonitor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirusTotalNET;

namespace Service
{
    class Program
    {
        static EventLog monitoringLog = new EventLog();

        static void Main(string[] args)
        {
            LogExists();

            monitoringLog.Source = "AuthBaseMonitorSource";
            monitoringLog.Log = "AuthBaseMonitorLog";

            monitoringLog.WriteEntry("Monitor started");

            CheckHashes();

            //CheckVirusTotalAsync(@"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\AuthBaseMonitoringService\Resources\DialogDisplay.exe").Wait();
        }

        [STAThread]
        private static void CheckHashes()
        {
            ProcessFunctions processFunctions = new ProcessFunctions();

            monitoringLog.WriteEntry("Checking hashes of currently running processes");
            var unmatched = processFunctions.UnmatchedHash(out string filename);

            if (unmatched.Count != 0)
            {
                System.Windows.Forms.Application.Run(new DialogDisplay.DialogDisplay(new string[] { filename }));
            }
        }

        private static async Task CheckVirusTotalAsync(string path)
        {
            VirusTotal virusTotal = new VirusTotal("0b95454468af7c253a3332ca0e225f6bd53e2d087945cb58efd22bc6592c46a8")
            {
                UseTLS = true
            };

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = path.Substring(path.LastIndexOf("\\") + 1);

            VirusTotalNET.Results.ScanResult scanResult = await virusTotal.ScanFileAsync(fileBytes, fileName);
        }

        private static void LogExists()
        {
            if (!EventLog.SourceExists("AuthBaseMonitorSource"))
            {
                EventLog.CreateEventSource("AuthBaseMonitorSource", "AuthBaseMonitorLog");
            }
        }
    }
}
