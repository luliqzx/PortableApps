using PortableApps.Model;
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
    public partial class appinfosaveform : Form
    {
        IBangsaRepo BangsaRepo = new BangsaRepo();
        public appinfosaveform()
        {
            InitializeComponent();
        }

        private void appinfosaveform_Load(object sender, EventArgs e)
        {
            OnLoad(e);
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();

            LoadTBangsa();
            LoadParlimen();
            LoadDaerah();
            LoadDun();
        }

        private void LoadTBangsa()
        {
            IList<TBangsa> lstBangsa = BangsaRepo.GetAll();
            cbbangsa.Items.Clear();
            cbbangsa.DataSource = lstBangsa;
            cbbangsa.DisplayMember = "Bangsa";
            cbbangsa.ValueMember = "Bangsa";
            cbbangsa.SelectedIndex = -1;
        }

        private void LoadParlimen()
        {
        }

        private void LoadDaerah()
        {
        }

        private void LoadDun()
        {
        }
    }
}
