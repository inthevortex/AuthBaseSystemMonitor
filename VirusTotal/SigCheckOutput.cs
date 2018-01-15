using System.Collections.Generic;

namespace VirusTotal
{
    public class SigCheckOutput
    {
        public SigCheckOutput()
        {

        }

        public SigCheckOutput(List<string> values)
        {
            if (values.Count == 18)
            {
                Path = values[0];
                Verified = values[1];
                Date = values[2];
                Publisher = values[3];
                Company = values[4];
                Description = values[5];
                Product = values[6];
                ProductVersion = values[7];
                FileVersion = values[8];
                MachineType = values[9];
                MD5 = values[10];
                SHA1 = values[11];
                PESHA1 = values[12];
                PESHA256 = values[13];
                SHA256 = values[14];
                IMP = values[15];
                VTDetection = values[16];
                VTLink = values[17];
            }
        }

        public string Path { get; set; }
        public string Verified { get; set; }
        public string Date { get; set; }
        public string Publisher { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Product { get; set; }
        public string ProductVersion { get; set; }
        public string FileVersion { get; set; }
        public string MachineType { get; set; }
        public string MD5 { get; set; }
        public string SHA1 { get; set; }
        public string PESHA1 { get; set; }
        public string PESHA256 { get; set; }
        public string SHA256 { get; set; }
        public string IMP { get; set; }
        public string VTDetection { get; set; }
        public string VTLink { get; set; }
    }
}
