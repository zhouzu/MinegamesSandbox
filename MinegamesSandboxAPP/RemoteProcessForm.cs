using MinegamesSandbox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinegamesSandboxAPP
{
    public partial class RemoteProcessForm : Form
    {
        public RemoteProcessForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    if (!FileHandlesHooks.PreventWritingFiles(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While hooking one of the functions that writes files, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!FileHandlesHooks.UnPreventWritingFiles(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While unhooking one of the functions that writes files, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }

                if (checkBox2.Checked)
                {
                    if (!FileHandlesHooks.PreventReadingFiles(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While hooking one of the functions that reads files, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!FileHandlesHooks.UnPreventReadingFiles(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While unhooking one of the functions that reads files, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }

                if (checkBox3.Checked)
                {
                    if (!ProcessHooks.PreventProcessCreation(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While hooking one of the functions that creates processes, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!ProcessHooks.UnPreventProcessCreation(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While unhooking one of the functions that creates processes, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }

                if (checkBox4.Checked)
                {
                    if (!ProcessHooks.PreventProcessFromGettingProcessHandles(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While hooking one of the functions that gets process handles, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!ProcessHooks.UnPreventProcessFromGettingProcessHandles(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While unhooking one of the functions that gets process handles, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }

                if(checkBox5.Checked)
                {
                    if(!ServicesHooks.PreventCreatingServices(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While hooking one of the functions that creates services, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!ServicesHooks.UnPreventCreatingServices(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While unhooking one of the functions that creates services, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }

                if(checkBox6.Checked)
                {
                    if(!RegistryHooks.PreventProcessFromEditingOrCreatingRegistryKeys(Convert.ToInt32(textBox1.Text)))
                    {
                        MessageBox.Show("Error While hooking one of the functions that edit or create registry keys, please make sure that the program have the same privilges of the target or higher and that the app are 32-bit arch.", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
