using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProcessMonitor
{
    public class ProcessFunctions
    {
        public List<ProcessObject> UnmatchedHash(out string filename)
        {
            var processes = Process.GetProcesses();
            List<ProcessObject> processObjects = new List<ProcessObject>();
            MapProcessObject mapProcessObject = new MapProcessObject();

            foreach (var process in processes)
            {
                try
                {
                    var po = mapProcessObject.TransformToProcessObject(process);
                    if (!po.HashMatched)
                    {
                        processObjects.Add(po);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            filename = "UnmatchedHashes.json";
            System.IO.File.WriteAllText(@"C:\" +filename, JsonConvert.SerializeObject(processObjects, Formatting.Indented));

            return processObjects;
        }
    }
}
