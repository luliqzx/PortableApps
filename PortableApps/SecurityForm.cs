using PortableApps.Common;
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
            if (textBox1.Text == "mpob1234")
            {
                try
                {
                    WFUtils.SyncToServer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFullMessage());
                }
                //MessageBox.Show("OK");
            }
        }
    }
}
