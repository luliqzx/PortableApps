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
    public partial class makkebunform : Form
    {
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();
        IVariablesRepo VariablesRepo = new VariablesRepo();
        IMakkebunRepo MakkebunRepo = new MakkebunRepo();

        public int appinfo_id { get; set; }
        public string refno { get; set; }
        public int id_makkebun { get; set; }

        public class KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public makkebunform()
        {
            InitializeComponent();
        }

        private void makkebunform_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();

            txttarikhtebang.CustomFormat = "dd-MM-yyyy";

            groupBox2.Text = groupBox2.Text + " " + refno;

            BindMaklumatPemohon(appinfo_id);

            BindSyaratTanah();

            BindPemilikan();

            BindJenisHakMilikTanah();

            BindPengurusan();
        }

        private void BindPengurusan()
        {
            List<KeyValue> lstKV = new List<KeyValue>();
            lstKV.Add(new KeyValue() { Key = "Sendiri", Value = "Sendiri" });
            lstKV.Add(new KeyValue() { Key = "MPOB", Value = "MPOB" });
            cbpengurusan.DataSource = lstKV;
            cbpengurusan.ValueMember = "Key";
            cbpengurusan.DisplayMember = "Value";
            cbpengurusan.SelectedIndex = -1;
        }

        private void BindJenisHakMilikTanah()
        {
            List<KeyValue> lstKV = new List<KeyValue>();
            lstKV.Add(new KeyValue() { Key = "GK", Value = "Geran Kekal" });
            lstKV.Add(new KeyValue() { Key = "TNCR", Value = "Tanah NCR" });
            lstKV.Add(new KeyValue() { Key = "TPT", Value = "Tanah PT" });
            lstKV.Add(new KeyValue() { Key = "ROA", Value = "Rizab Orang Asli" });
            lstKV.Add(new KeyValue() { Key = "LL", Value = "Lain-Lain" });
            cbjenishakmiliktanah.DataSource = lstKV;
            cbjenishakmiliktanah.ValueMember = "Key";
            cbjenishakmiliktanah.DisplayMember = "Value";
            cbjenishakmiliktanah.SelectedIndex = -1;
        }

        private void BindPemilikan()
        {
            List<KeyValue> lstKV = new List<KeyValue>();
            lstKV.Add(new KeyValue() { Key = "Berkongsi", Value = "Berkongsi" });
            lstKV.Add(new KeyValue() { Key = "Kekal", Value = "Kekal" });
            lstKV.Add(new KeyValue() { Key = "Sementara", Value = "Sementara" });
            lstKV.Add(new KeyValue() { Key = "Pajakans", Value = "Pajakans" });
            cbpemilikan.DataSource = lstKV;
            cbpemilikan.ValueMember = "Key";
            cbpemilikan.DisplayMember = "Value";
            cbpemilikan.SelectedIndex = -1;
            //cbpemilikan.Items.Add("Berkongsi");
            //cbpemilikan.Items.Add("Kekal");
            //cbpemilikan.Items.Add("Sementara");
            //cbpemilikan.Items.Add("Pajakans");
        }

        private void BindSyaratTanah()
        {
            List<KeyValue> lstKV = new List<KeyValue>();
            lstKV.Add(new KeyValue() { Key = "SAWIT", Value = "SAWIT" });
            lstKV.Add(new KeyValue() { Key = "PERTANIAN", Value = "PERTANIAN" });
            lstKV.Add(new KeyValue() { Key = "TIADA SEKATAN", Value = "TIADA SEKATAN" });
            cbsyarattanah.DataSource = lstKV;
            cbsyarattanah.ValueMember = "Key";
            cbsyarattanah.DisplayMember = "Value";
            cbsyarattanah.SelectedIndex = -1;
            //cbsyarattanah.Items.Clear();
            //cbsyarattanah.Items.Add("SAWIT");
            //cbsyarattanah.Items.Add("PERTANIAN");
            //cbsyarattanah.Items.Add("TIADA SEKATAN");
        }

        private void BindMaklumatPemohon(int appinfo_id)
        {
            appinfo appinfo = AppInfoRepo.GetBy(appinfo_id);
            if (appinfo == null)
            {
                appinfo = new appinfo();
            }
            lblappdate.Text = appinfo.appdate;
            lblnama.Text = appinfo.nama;
            lblbangsa.Text = appinfo.bangsa;
            lbladdr1.Text = appinfo.addr1;
            lbladdr2.Text = appinfo.addr2;
            lbladdr3.Text = appinfo.addr3;
            lblnegeri.Text = appinfo.negeri;
            lbldaerah.Text = appinfo.daerah;
            lblnokp.Text = appinfo.icno;
            lblnolesen.Text = appinfo.nolesen;
            lblbandar.Text = appinfo.bandar;
            lblposkod.Text = appinfo.poskod;
            LoadWilayah(appinfo.negeri);
        }

        private void LoadWilayah(string negeri)
        {
            variables variables = VariablesRepo.GetVariableNegeri("NEGERI").FirstOrDefault(x => x.Code == negeri);
            if (variables != null)
            {
                lblwilayah.Text = GetAliasesParent(variables.Parent);
            }
        }

        private string GetAliasesParent(string parent)
        {
            if (parent == "UTR")
            {
                parent = "UTARA";
            }
            else if (parent == "TMR")
            {
                parent = "TIMUR";
            }
            else if (parent == "TGH")
            {
                parent = "TENGAH";
            }
            else if (parent == "SEL")
            {
                parent = "SELATAN";
            }
            return parent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool IsNew = false;
            makkebun makkebun = MakkebunRepo.GetBy(id_makkebun);
            if (makkebun == null)
            {
                makkebun = new makkebun();
                IsNew = true;
            }
            makkebun.addr1 = txtaddr1.Text;
            makkebun.addr2 = txtaddr2.Text;
            makkebun.addr3 = txtaddr3.Text;
            makkebun.catatan = txtcatatan.Text;
            makkebun.luaslesen = Convert.ToDouble(txtluaslesen.Text);
            makkebun.luasmatang = Convert.ToDouble(txtluasmatang.Text);
            makkebun.nolesen = txtnolesen.Text;
            makkebun.nolot = txtnolot.Text;
            makkebun.tarikhtebang = txttarikhtebang.Text;

            if (IsNew)
            {
                MakkebunRepo.Create(makkebun);
            }
            else
            {
                MakkebunRepo.Edit(makkebun);
            }
            MessageBox.Show("Data berhasil disimpan [" + refno + " | " + txtnolot.Text + "]");
        }
    }
}
