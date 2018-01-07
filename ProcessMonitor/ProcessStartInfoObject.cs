using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security;
using System.Text;

namespace ProcessMonitor
{
    public class ProcessStartInfoObject
    {
        public bool ErrorDialog { get; set; }
        public string WorkingDirectory { get; set; }
        public string FileName { get; set; }
        public bool LoadUserProfile { get; set; }
        public string Domain { get; set; }
        public string PasswordInClearText { get; set; }
        public SecureString Password { get; set; }
        public string UserName { get; set; }
        public string[] Verbs { get; set; }
        public bool UseShellExecute { get; set; }
        //public Encoding StandardOutputEncoding { get; set; }
        //public Encoding StandardErrorEncoding { get; set; }
        public bool RedirectStandardError { get; set; }
        public bool RedirectStandardOutput { get; set; }
        public bool RedirectStandardInput { get; set; }
        //public IDictionary<string, string> Environment { get; set; }
        public StringDictionary EnvironmentVariables { get; set; }
        public bool CreateNoWindow { get; set; }
        public string Arguments { get; set; }
        public string Verb { get; set; }
        public IntPtr ErrorDialogParentHandle { get; set; }
        public ProcessWindowStyle WindowStyle { get; set; }
    }
}
