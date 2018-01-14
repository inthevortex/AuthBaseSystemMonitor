using System.Collections.Generic;
using System.Diagnostics;

namespace VirusTotal
{
    public class VirusTotal
    {
        public List<SigCheckOutput> InvokeSigCheck(string filepath)
        {
            List<SigCheckOutput> outputs = new List<SigCheckOutput>();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                FileName = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\VirusTotal\Resources\sigcheck64.exe",
                Arguments = "-nobanner -h -v -vt -c " + filepath
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
                            vals.Add(value);
                        }

                        outputs.Add(SigCheckOutput.MapObject(vals));
                    }
                }
                process.WaitForExit();
            }

            return outputs;
        }
    }
}
