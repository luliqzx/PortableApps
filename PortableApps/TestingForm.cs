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
    public partial class TestingForm : Form
    {
        public TestingForm()
        {
            InitializeComponent();
        }

        private void TestingForm_Load(object sender, EventArgs e)
        {
            MDIParent2 newMDIParent2 = new MDIParent2();
            newMDIParent2.SyncToServer();
        }
    }
}
