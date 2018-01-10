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
        //static void Main(string[] args)
        //{
        //    List<string> paths = new List<string>
        //    {
        //        @"C:\Windows", @"C:\Program Files", @"C:\Program Files (x86)", @"C:\Users"
        //    };
        //    List<string> files = new List<string>();

        //    foreach (string path in paths)
        //    {
        //        files.AddRange(GetFileList("*", path));
        //    }

        //    Console.WriteLine("{0} files found.", files.Count);
        //}

        //public static IEnumerable<string> GetFileList(string fileSearchPattern, string rootFolderPath)
        //{
        //    Queue<string> pending = new Queue<string>();
        //    pending.Enqueue(rootFolderPath);
        //    string[] tmp;

        //    while (pending.Count > 0)
        //    {
        //        rootFolderPath = pending.Dequeue();

        //        try
        //        {
        //            tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
        //        }
        //        catch (UnauthorizedAccessException)
        //        {
        //            continue;
        //        }

        //        for (int i = 0; i < tmp.Length; i++)
        //        {
        //            yield return tmp[i];
        //        }

        //        tmp = Directory.GetDirectories(rootFolderPath);

        //        for (int i = 0; i < tmp.Length; i++)
        //        {
        //            pending.Enqueue(tmp[i]);
        //        }
        //    }
        //}

        //static void Main(string[] args)
        //{
        //    Hasher hasher = new Hasher(new string[] { @"C:\Program Files (x86)\Notepad++" }, "*");
        //    Hashes hashes = hasher.GetHashes();
        //    hasher.CreateJsonFile(hashes);
        //}

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
            //Hasher hasher = new Hasher(new string[] { @"D:\Angsuman\Repos\Platform_IMS\Appalachian\Appalachian_Development\Source\BizDIMS\FUNBizDIMS" }, "*");

            RunTest("devenv");
        }

        private static void RunTest(string appName)
        {
            bool done = false;
            PerformanceCounter total_cpu = new PerformanceCounter("Process", "% Processor Time", "_Total");
            PerformanceCounter process_cpu = new PerformanceCounter("Process", "% Processor Time", appName);
            PerformanceCounter total_mem = new PerformanceCounter("Memory", "Available MBytes");

            while (!done)
            {
                float t = total_cpu.NextValue();
                float p = process_cpu.NextValue();
                float m = total_mem.NextValue();

                Console.WriteLine(String.Format("CPU: _Total = {0}  App = {1} {2}%\n Memory: {3}\n", t, p, p / t * 100, m));
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
