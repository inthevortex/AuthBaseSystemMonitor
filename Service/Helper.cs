using ProcessMonitor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Helper
    {
        protected internal static (string source, string log) LogExists()
        {
            string source = "AuthBaseMonitorSource";
            string log = "AuthBaseMonitorLog";

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
            }

            return (source, log);
        }

        protected internal static void CheckHashes()
        {
            ProcessFunctions processFunctions = new ProcessFunctions();

            var unmatched = processFunctions.UnmatchedHash(out string filename);

            if (unmatched.Count != 0)
            {
                System.Windows.Forms.Application.Run(new DialogDisplay.DialogDisplay(new string[] { filename }));
            }
        }
    }
}
