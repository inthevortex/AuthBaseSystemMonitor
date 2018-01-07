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
            monitoringLog.WriteEntry("Monitor started");

            Timer readProcessesTimer = new Timer
            {
                Interval = 60000
            };
            readProcessesTimer.Elapsed += new ElapsedEventHandler(OnReadProcessesTimer);
            readProcessesTimer.Start();
        }

        private void OnReadProcessesTimer(object sender, ElapsedEventArgs e)
        {
            monitoringLog.WriteEntry("Reading processes", EventLogEntryType.Information);

            var processes = Process.GetProcesses();
            ProcessObject[] processObjects = new ProcessObject[processes.Length];
            MapProcessObject mapProcessObject = new MapProcessObject();
            int index = 0;

            foreach (var process in processes)
            {
                processObjects[index++] = mapProcessObject.TransformToProcessObject(process);
            }

            mapProcessObject.WriteToJsonFile(processObjects);
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
