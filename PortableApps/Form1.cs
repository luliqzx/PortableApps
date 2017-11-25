using PortableApps.Repo;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISqLiteBaseRepository sqlRepo = new SqLiteBaseRepository();
            if (!sqlRepo.CheckExistingDB(sqlRepo.DbFile))
            {
                using (var cnn = sqlRepo.MySQLiteConnection())
                {
                    cnn.Open();
                    sqlRepo.ResetDefault(cnn);
                    sqlRepo.SetupAppInfo(cnn);
                    sqlRepo.SetupDun(cnn);
                    sqlRepo.SetupMakkebun(cnn);
                    sqlRepo.SetupParlimen(cnn);
                    sqlRepo.SetupVariables(cnn);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
