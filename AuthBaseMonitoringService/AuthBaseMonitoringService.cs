using ProcessMonitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AuthBaseMonitoringService
{
    public partial class AuthBaseMonitoringService : ServiceBase
    {
        private int eventId = 1;

        public AuthBaseMonitoringService()
        {
            InitializeComponent();
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;
            monitoringLog = new EventLog();
            
            if (!EventLog.SourceExists("AuthBaseMonitorSource"))
            {
                EventLog.CreateEventSource("AuthBaseMonitorSource", "AuthBaseMonitorLog");
            }

            monitoringLog.Source = "AuthBaseMonitorSource";
            monitoringLog.Log = "AuthBaseMonitorLog";
        }

        protected override void OnStart(string[] args)
        {
            ProcessFunctions processFunctions = new ProcessFunctions();

            monitoringLog.WriteEntry("Monitor started");

            monitoringLog.WriteEntry("Checking hashes of currently running processes");
            var unmatched = processFunctions.UnmatchedHash(out string filename);

            if (unmatched.Count != 0)
            {
                //ProcessStartInfo startInfo = new ProcessStartInfo
                //{
                //    UseShellExecute = true,
                //    CreateNoWindow = false,
                //    WindowStyle = ProcessWindowStyle.Normal,
                //    FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\AuthBaseMonitoringService\Resources\DialogDisplay.exe",
                //    Arguments = filename
                //};
                //Process.Start(startInfo);

                System.Windows.Forms.Application.Run(new DialogDisplay.DialogDisplay(new string[] { filename }));
            }

            Timer readProcessesTimer = new Timer
            {
                Interval = 60000
            };
            readProcessesTimer.Elapsed += new ElapsedEventHandler(OnReadProcessesTimer);
            readProcessesTimer.Start();
        }

        private void OnReadProcessesTimer(object sender, ElapsedEventArgs e)
        {
            monitoringLog.WriteEntry("Reading processes", EventLogEntryType.Information, eventId++);

            
        }

        protected override void OnStop()
        {
            monitoringLog.WriteEntry("Monitor stopped");
        }

        protected override void OnPause()
        {
            monitoringLog.WriteEntry("Monitor paused");
        }

        protected override void OnContinue()
        {
            monitoringLog.WriteEntry("Monitor resumed");
        }

        protected override void OnShutdown()
        {
            monitoringLog.WriteEntry("System shutdown");
        }
    }
}
