using MinegamesSandbox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinegamesSandboxAPP
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            FileHandlesHooks.Initialize();
            ProcessHooks.Initialize();
            ServicesHooks.Initialize();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new RemoteProcessForm().ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new CurrentProcessForm().ShowDialog();
        }
    }
}