﻿using PortableApps.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PortableApps
{
    public partial class SecurityForm : Form
    {
        public SecurityForm()
        {
            InitializeComponent();
        }

        public Type RedirectToForm { get; set; }

        public Form SourceMdiParent { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "mpob1234")
            {
                if (RedirectToForm == null)
                {
                    try
                    {
                        WFUtils.SyncToServer();
                        MessageBox.Show("Sinkronisasi data dengan Server selesai.");

                        string directory = AppDomain.CurrentDomain.BaseDirectory + string.Format(@"\Logs\AppLogs\{0}\", DateTime.Now.ToString("MM.yyyy"));
                        System.Diagnostics.Process.Start(directory);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.GetFullMessage());
                    }
                }

                else
                {
                    DisplayForm(SourceMdiParent, RedirectToForm);
                    this.Close();
                }

                //else if (RedirectToForm == typeof(VariableSettingForm))
                //{
                //    foreach (Form f in SourceMdiParent.MdiChildren)
                //    {
                //        if (f.GetType() == typeof(VariableSettingForm))
                //        {
                //            f.Close();
                //            //f.Activate();
                //            //return;
                //        }
                //    }
                //    Form form = new VariableSettingForm();
                //    form.MdiParent = SourceMdiParent;
                //    form.Show();
                //}

                //MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("Harap masukkan code dengan benar.");
            }
        }

        private void DisplayForm(Form sourceMdiParent, Type redirectToForm)
        {
            Form ToOpenForm = null;
            foreach (var f in SourceMdiParent.MdiChildren)
            {
                if (f.GetType() == RedirectToForm)
                {
                    //ToOpenForm = f;
                    f.Close();
                    break;
                    //f.Activate();
                    //return;
                }
            }
            if (ToOpenForm == null)
            {
                ToOpenForm = Activator.CreateInstance(redirectToForm, false) as Form;
            }
            Form form = ToOpenForm;
            form.MdiParent = SourceMdiParent;
            form.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
