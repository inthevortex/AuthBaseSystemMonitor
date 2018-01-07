using System.ServiceProcess;

namespace AuthBaseMonitoringService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new AuthBaseMonitoringService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
