using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinegamesSandbox;

namespace MinegamesSandboxAPP
{
    public partial class FileHandlesHooksForm : Form
    {
        public FileHandlesHooksForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FileHandlesHooks.PreventWritingFiles(Process.GetCurrentProcess().Id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileHandlesHooks.UnPreventWritingFiles(Process.GetCurrentProcess().Id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("You Can't leave the file path or the data to write textbox empty.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                if (File.Exists(textBox1.Text))
                {
                    File.WriteAllText(textBox1.Text, textBox2.Text);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FileHandlesHooks.PreventReadingFiles(Process.GetCurrentProcess().Id);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FileHandlesHooks.UnPreventReadingFiles(Process.GetCurrentProcess().Id);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please Select a File To Get It's data.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                if(File.Exists(textBox3.Text))
                {
                    textBox4.Text = File.ReadAllText(textBox3.Text);
                }
            }
        }
    }
}
