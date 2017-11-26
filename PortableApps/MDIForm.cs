using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PortableApps
{
    public partial class MDIForm : RibbonForm
    {
        public MDIForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            //if (this.ActiveMdiChild != null)
            while (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
        }
        
        private void rbtnmaklumatpemohon_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(appinfosaveform))
                {
                    f.Activate();
                    return;
                }
            }
            Form form = new appinfosaveform();
            form.MdiParent = this;
            form.Show();
        }

        private void ribbonOrbMenuItem1_Click(object sender, EventArgs e)
        {
            //if (this.ActiveMdiChild != null)
            while (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
        }

        private void ribbonOrbOptionButton1_Click(object sender, EventArgs e)
        {
            //if (this.ActiveMdiChild != null)
            while (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
        }

    }
}