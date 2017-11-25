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
    public partial class SettingForm : Form
    {
        ICSToDBRepo CSToDBRepo = new CSToDBRepo();

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            IList<CSToDB> lstEnt = CSToDBRepo.GetAll();
            dgvCS.DataSource = lstEnt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CSToDB CSToDB = new CSToDB();
            CSToDB.CSName = txtCSName.Text;
            CSToDB.ConnString = txtConnString.Text;
            CSToDBRepo.Create(CSToDB);
            LoadGrid();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
