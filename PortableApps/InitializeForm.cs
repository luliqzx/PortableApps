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
        #region Fields/ Properties
        ISqLiteBaseRepository SqLiteBaseRepository = new SqLiteBaseRepository();
        #endregion

        #region Constructor
        public InitializeForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        private void btnCreateAppInfo_Click(object sender, EventArgs e)
        {
            btnCreateAppInfo.Enabled = false;

            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupAppInfo(cnn);
            }
            MessageFinishProcess("Setup Maklumat Pemohon selesai");
        }

        private void btnCreateDun_Click(object sender, EventArgs e)
        {
            btnCreateDun.Enabled = false;

            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupDun(cnn);
            }
            MessageFinishProcess("Setup Master Dun selesai");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupTBangsa(cnn);

            }
            MessageFinishProcess("Setup Master Bangsa selesai");

            //IBangsaRepo BangsaRepo = new BangsaRepo();
            //BangsaRepo.SyncBangsaFromAppInfoMySQL();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupVariableSetting(cnn);
            }
            MessageFinishProcess("Setup Setting selesai");
        }

        private void MessageFinishProcess(string msg)
        {
            MessageBox.Show(msg);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;

            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupVariables(cnn);

            }
            MessageFinishProcess("Setup Master Variables selesai");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;

            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupDaerah(cnn);

            }
            MessageFinishProcess("Setup Master Daerah selesai");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupParlimen(cnn);

            }
            MessageFinishProcess("Setup Master Parlimen selesai");

        }

        private void btnCreateMakkebun_Click(object sender, EventArgs e)
        {
            btnCreateMakkebun.Enabled = false;

            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupMakkebun(cnn);

            }
            MessageFinishProcess("Setup Maklumat Kebun selesai");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            using (var cnn = SqLiteBaseRepository.MySQLiteConnection())
            {
                cnn.Open();
                SqLiteBaseRepository.SetupSemakTapak(cnn);
            }
            MessageFinishProcess("Setup Lawatan Pengesahan Kebun selesai");

        }

        #endregion

        private void InitializeForm_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();
        }
    }
}
