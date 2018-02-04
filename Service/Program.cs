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

namespace Service
{
    class Program
    {
        static EventLog monitoringLog = new EventLog();
        static bool system_start = true;

        static void Main(string[] args)
        {
            (monitoringLog.Source, monitoringLog.Log) =  Helper.LogExists();

            monitoringLog.WriteEntry("Monitor started");

            if (system_start)
            {
                monitoringLog.WriteEntry("Hashing Sytem");
            }

            monitoringLog.WriteEntry("Checking hashes of currently running processes");
            Helper.CheckHashes();
        }
    }
}
