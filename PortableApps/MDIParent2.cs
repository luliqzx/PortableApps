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
    public partial class MDIParent2 : Form
    {
        public MDIParent2()
        {
            InitializeComponent();
        }

        private void rbMaklumatPemohon_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(appinfosaveform))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new appinfosaveform();
            form.MdiParent = this;
            form.Show();
        }

        private void rbmakkebun_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(makkebunform))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new makkebunform();
            form.MdiParent = this;
            form.Show();
        }

        private void rbSetting_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(VariableSettingForm))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new VariableSettingForm();
            form.MdiParent = this;
            form.Show();
        }

        private void rbDaftarMaklumatPermohonan_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(appinfolistform))
                {
                    f.Close();
                    //f.Activate();
                    //return;
                }
            }
            Form form = new appinfolistform();
            form.MdiParent = this;
            form.Show();
        }
    }
}
