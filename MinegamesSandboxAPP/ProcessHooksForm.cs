using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MinegamesSandbox;

namespace MinegamesSandboxAPP
{
    public partial class ProcessHooksForm : Form
    {
        public ProcessHooksForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                if(File.Exists(textBox1.Text))
                {
                    Process.Start(textBox1.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessHooks.PreventProcessCreation(Process.GetCurrentProcess().Id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProcessHooks.UnPreventProcessCreation(Process.GetCurrentProcess().Id);
        }
    }
}
