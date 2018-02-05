using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHasher;
using Newtonsoft.Json;
using ProcessMonitor;

namespace Testing
{
    class Program
    {
        public ProcessObject[] GetProcessesInfo()
        {
            var processes = Process.GetProcesses();
            ProcessObject[] processObjects = new ProcessObject[processes.Length];
            MapProcessObject mapProcessObject = new MapProcessObject();
            int index = 0;

            foreach (var process in processes)
            {
                var otherResources = GetOtherResources(process.ProcessName);
                processObjects[index++] = mapProcessObject.TransformToProcessObject(process, otherResources);

            }

            return processObjects;
        }

        private Dictionary<string, float> GetOtherResources(string processName)
        {
            var total_cpu = new PerformanceCounter("Process", "% Processor Time", "_Total");
            var process_cpu = new PerformanceCounter("Process", "% Processor Time", processName);
            var process_mem = new PerformanceCounter("Process", "Working Set", processName);
            var process_handles = new PerformanceCounter("Process", "Handle Count", "_Total");
            var process_threads = new PerformanceCounter("Process", "Thread Count", "_Total");
            var readOpSec = new PerformanceCounter("Process", "IO Read Operations/sec", processName);
            var writeOpSec = new PerformanceCounter("Process", "IO Write Operations/sec", processName);
            var readBytesSec = new PerformanceCounter("Process", "IO Read Bytes/sec", processName);
            var writeByteSec = new PerformanceCounter("Process", "IO Write Bytes/sec", processName);
            int index = 0;
            float m, usage, h, th;
            float[] disk;
            Dictionary<string, float> otherResources = new Dictionary<string, float>();

            while (index++ <= 1)
            {
                m = process_mem.NextValue() / 1024 / 1024;
                usage = process_cpu.NextValue() / total_cpu.NextValue() * 100;
                h = process_handles.NextValue();
                th = process_threads.NextValue();
                disk = new float[] { readOpSec.NextValue(), writeOpSec.NextValue(), readBytesSec.NextValue() / 1024, writeByteSec.NextValue() / 1024 };

                if (index == 1)
                {
                    otherResources.Add("CPUUsage", usage);
                    otherResources.Add("RAMUsage", m);
                    otherResources.Add("Handles", h);
                    otherResources.Add("Threads", th);
                    otherResources.Add("ReadBytes", disk[2]);
                    otherResources.Add("WriteBytes", disk[3]);
                    otherResources.Add("ReadOps", disk[0]);
                    otherResources.Add("WriteOps", disk[1]);
                }
            }

            return otherResources;
        }

        //static void Main(string[] args)
        //{
        //var processes = Process.GetProcesses();
        //ProcessObject[] processObjects = new ProcessObject[processes.Length];
        //MapProcessObject mapProcessObject = new MapProcessObject();
        //int index = 0;

        //foreach (var process in processes)
        //{
        //    processObjects[index++] = mapProcessObject.TransformToProcessObject(process);
        //}

        //try
        //{
        //    System.IO.File.WriteAllText(@"C:\Users\AuthBase\Documents\ProcessStats.json", JsonConvert.SerializeObject(processObjects, Formatting.Indented));
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        //}

        static void Main(string[] args)
        {
            Hasher hasher = new Hasher(new string[] { @"D:\Angsuman\Repos\Platform_IMS\Appalachian\Appalachian_Development\Source\BizDIMS\FUNBizDIMS\MAPBizDIMS\bin\Debug" });//{ @"C:\Users\AuthBase\source\repos" });
            var hashes = hasher.HashSystem();
            FileHasherContext context = new FileHasherContext();
            FileRepository repository = new FileRepository(context);

            repository.SaveFile(hashes[0]);
            //var process = Process.GetProcessesByName("firefox")[0];
            //string path = process.MainModule.FileName;

            //ProcessStartInfo startInfo = new ProcessStartInfo
            //{
            //    UseShellExecute = true,
            //    CreateNoWindow = false,
            //    WindowStyle = ProcessWindowStyle.Normal,
            //    FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\AuthBaseMonitoringService\Resources\DialogDisplay.exe",
            //    //Arguments = @"C:\hashes.json"
            //};
            //Process.Start(startInfo);

            //System.Windows.Forms.Application.Run(new DialogDisplay.DialogDisplay());

            //RunTest("devenv");

            VirusTotal.VirusTotalService virusTotal = new VirusTotal.VirusTotalService();
            //var output1 = virusTotal.SigCheckDirectoryFull(@"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\packages");
            var output1 = virusTotal.SigCheckDirectory(@"D:\Angsuman\Repos\AuthBaseSystemMonitor\packages");
            //var output2 = virusTotal.SigCheckFile(@"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\AuthBaseMonitoringService\bin\Debug\AuthBaseMonitoringService.exe");
            var output2 = virusTotal.SigCheckFile(@"D:\Angsuman\Repos\AuthBaseSystemMonitor\AuthBaseMonitoringService\bin\Debug\AuthBaseMonitoringService.exe");
        }

        private static void RunTest(string appName)
        {
            bool done = false;
            PerformanceCounter total_cpu = new PerformanceCounter("Process", "% Processor Time", "_Total");
            PerformanceCounter process_cpu = new PerformanceCounter("Process", "% Processor Time", appName);
            PerformanceCounter process_mem = new PerformanceCounter("Process", "Working Set", appName);
            PerformanceCounter process_handles = new PerformanceCounter("Process", "Handle Count", "_Total");
            PerformanceCounter process_threads = new PerformanceCounter("Process", "Thread Count", "_Total");
            var readOpSec = new PerformanceCounter("Process", "IO Read Operations/sec", appName);
            var writeOpSec = new PerformanceCounter("Process", "IO Write Operations/sec", appName);
            var readBytesSec = new PerformanceCounter("Process", "IO Read Bytes/sec", appName);
            var writeByteSec = new PerformanceCounter("Process", "IO Write Bytes/sec", appName);

            while (!done)
            {
                float t = total_cpu.NextValue();
                float p = process_cpu.NextValue();
                float m = process_mem.NextValue() / 1024 / 1024;
                float usage = p / t * 100;
                float h = process_handles.NextValue();
                float th = process_threads.NextValue();
                float[] disk = new float[] { readOpSec.NextValue(), writeOpSec.NextValue(), readBytesSec.NextValue() / 1024, writeByteSec.NextValue() / 1024 };

                Console.WriteLine(String.Format("CPU: _Total = {0}  App = {1} {2}%\n Memory: {3} mb\n Handles: {4}, Threads: {5}\nDisk Operation (Read, Write): ({6}, {7})\nDisk Read (Read, Write): ({8}K, {9}K)\n", t, p, usage, m, h, th, disk[0], disk[1], disk[2], disk[3]));
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
