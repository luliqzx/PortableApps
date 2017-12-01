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
    public partial class InitializeForm : Form
    {
        ISqLiteBaseRepository SqLiteBaseRepository = new SqLiteBaseRepository();

        public InitializeForm()
        {
            InitializeComponent();
        }

        private void btnCreateAppInfo_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupAppInfo(cnn);
            }
        }

        private void btnCreateDun_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupDun(cnn);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupTBangsa(cnn);

            }

            IBangsaRepo BangsaRepo = new BangsaRepo();
            BangsaRepo.SyncBangsaFromAppInfoMySQL();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupVariableSetting(cnn);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupVariables(cnn);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupDaerah(cnn);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupParlimen(cnn);

            }
        }

        private void btnCreateMakkebun_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupMakkebun(cnn);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupSemakTapak(cnn);
            }
        }
    }
}
