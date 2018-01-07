using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace AuthBaseMonitoringService
{
    [RunInstaller(true)]
    public partial class x : System.Configuration.Install.Installer
    {
        public x()
        {
            InitializeComponent();
        }
    }
}
