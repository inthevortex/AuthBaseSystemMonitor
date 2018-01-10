using FileHasher;
using Newtonsoft.Json;
using ProcessMonitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogDisplay
{
    public partial class DialogDisplay : Form
    {
        private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();

        public DialogDisplay()
        {
            InitializeComponent();
            SetSource();
            InitializeDataGridView();
            Controls.Add(dataGridView1);
        }

        public DialogDisplay(string[] args)
        {
            InitializeComponent();
            SetSource(args);
            InitializeDataGridView();
            Controls.Add(dataGridView1);
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSize = true;
            dataGridView1.DataSource = bindingSource1;

            DataGridViewColumn col1 = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProcessName",
                Name = "Process Name",
                ReadOnly = true
            };
            dataGridView1.Columns.Add(col1);

            DataGridViewColumn col2 = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MainWindowTitle",
                Name = "Main Window Title",
                ReadOnly = true
            };
            dataGridView1.Columns.Add(col2);

            DataGridViewColumn col3 = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "File",
                Name = "File Path",
                ReadOnly = true
            };
            dataGridView1.Columns.Add(col3);

            DataGridViewColumn col4 = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Responding",
                Name = "Responding",
                ReadOnly = true
            };
            dataGridView1.Columns.Add(col4);
        }

        private void SetSource(string[] args)
        {
            var data = System.IO.File.ReadAllText(@"C:\hashes.json");
            List<ProcessObject> unmatched = JsonConvert.DeserializeObject<List<ProcessObject>>(data);

            foreach (var process in unmatched)
            {
                bindingSource1.Add(new DataObj
                {
                    ProcessName = process.ProcessName,
                    MainWindowTitle = process.MainWindowTitle,
                    File = process.MainModule.FileName,
                    Responding = process.Responding
                });
            }
        }

        private void SetSource()
        {
            var data = System.IO.File.ReadAllText(@"C:\hashes.json");
            List<File> unmatched = JsonConvert.DeserializeObject<List<File>>(data);

            foreach (var process in unmatched)
            {
                bindingSource1.Add(new DataObj
                {
                    ProcessName = process.Name,
                    MainWindowTitle = process.Name,
                    File = process.Path,
                    Responding = true
                });
            }
        }

        private class DataObj
        {
            public string ProcessName { get; set; }
            public string MainWindowTitle { get; set; }
            public string File { get; set; }
            public bool Responding { get; set; }
        }
    }
}
