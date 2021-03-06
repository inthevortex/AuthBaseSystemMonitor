﻿using System.Collections.Generic;
using System.Diagnostics;

namespace FileHasher.VirusTotalService
{
    public class VirusTotalService
    {
        // Scan a single file
        public SigCheckOutput SigCheckFile(string filepath)
        {
            SigCheckOutput output = new SigCheckOutput();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\FileHasher\Resources\sigcheck64.exe",
                Arguments = "-accepteula -nobanner -h -v -vt -c " + filepath
            };

            using (Process process = Process.Start(startInfo))
            {
                bool flag = false;

                using (var reader = process.StandardOutput)
                {
                    if (!flag && !reader.EndOfStream)
                    {
                        reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        List<string> vals = new List<string>();
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        foreach (string value in values)
                        {
                            vals.Add(value.TrimStart('"').TrimEnd('"'));
                        }

                        output = new SigCheckOutput(vals);
                    }
                }
                process.WaitForExit();
            }

            return output;
        }

        // Scan all executable files in a directory recursively
        public List<SigCheckOutput> SigCheckDirectory(string directory)
        {
            List<SigCheckOutput> outputs = new List<SigCheckOutput>();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\FileHasher\Resources\sigcheck64.exe",
                Arguments = "-accepteula -nobanner -h -e -s -v -vt -c " + directory
            };

            using (Process process = Process.Start(startInfo))
            {
                bool flag = false;

                using (var reader = process.StandardOutput)
                {
                    if (!flag && !reader.EndOfStream)
                    {
                        reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        List<string> vals = new List<string>();
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        foreach (string value in values)
                        {
                            vals.Add(value.TrimStart('"').TrimEnd('"'));
                        }

                        outputs.Add(new SigCheckOutput(vals));
                    }
                }
                process.WaitForExit();
            }

            return outputs;
        }

        // Scan all files in a directory recursively
        public List<SigCheckOutput> SigCheckDirectoryFull(string directory)
        {
            List<SigCheckOutput> outputs = new List<SigCheckOutput>();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\FileHasher\Resources\sigcheck64.exe",
                Arguments = "-nobanner -h -s -v -vt -c " + directory
            };

            using (Process process = Process.Start(startInfo))
            {
                bool flag = false;

                using (var reader = process.StandardOutput)
                {
                    if (!flag && !reader.EndOfStream)
                    {
                        reader.ReadLine();
                    }

                    while (!reader.EndOfStream)
                    {
                        List<string> vals = new List<string>();
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        foreach (string value in values)
                        {
                            vals.Add(value.TrimStart('"').TrimEnd('"'));
                        }

                        outputs.Add(new SigCheckOutput(vals));
                    }
                }
                process.WaitForExit();
            }

            return outputs;
        }

        // Get SHA256 hash of a single file
        public string GetHash(string filepath)
        {
            return SigCheckFile(filepath).SHA256;
        }

        //public List<T> ScanRunningProcesses()
        //{

        //}
    }
}
