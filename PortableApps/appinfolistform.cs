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
    public partial class appinfolistform : Form
    {
        public appinfolistform()
        {
            InitializeComponent();
        }

        private void appinfolistform_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Form f in ParentForm.MdiChildren)
            {
                if (f.GetType() == typeof(appinfosaveform))
                {
                    f.Activate();
                    return;
                }
            }
            Form form = new appinfosaveform();
            form.MdiParent = ParentForm;
            form.Show();
        }
    }
}
