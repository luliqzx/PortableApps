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

        public Form RequestForm { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "abcde")
            {
                MDIParent2 mdiPage = MdiParent as MDIParent2;
                //mdiPage.SyncToServer();
                MessageBox.Show("OK");
            }
        }
    }
}
