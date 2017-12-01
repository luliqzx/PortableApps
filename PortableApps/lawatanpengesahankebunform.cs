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
    public partial class lawatanpengesahankebunform : Form
    {
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();
        IVariablesRepo VariablesRepo = new VariablesRepo();

        public int appinfo_id { get; set; }
        public string refno { get; set; }
        public int id_makkebun { get; set; }

        public lawatanpengesahankebunform()
        {
            InitializeComponent();
        }

        public class KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();
            BindMaklumatPemohon(appinfo_id);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";

            BindKaedah();

            BindJenisTanah();

            groupBox2.Text = groupBox2.Text + " " + refno;
        }

        private void BindJenisTanah()
        {
            List<KeyValue> lstKV = new List<KeyValue>();
            lstKV.Add(new KeyValue { Key = "Pedalaman", Value = "Pedalaman" });
            lstKV.Add(new KeyValue { Key = "Gambut", Value = "Gambut" });
            lstKV.Add(new KeyValue { Key = "Lanar Pantai", Value = "Lanar Pantai" });
            cbjenis_tanah.DataSource = lstKV;
            cbjenis_tanah.ValueMember = "Key";
            cbjenis_tanah.DisplayMember = "Value";
            cbjenis_tanah.SelectedIndex = -1;
        }

        private void BindKaedah()
        {
            List<KeyValue> lstKV = new List<KeyValue>();
            lstKV.Add(new KeyValue { Key = "Alat GPS", Value = "Alat GPS" });
            lstKV.Add(new KeyValue { Key = "Kompas & Tali Ukur", Value = "Kompas & Tali Ukur" });
            lstKV.Add(new KeyValue { Key = "Batu sepadan", Value = "Batu sepadan" });
            cbkaedah.DataSource = lstKV;
            cbkaedah.ValueMember = "Key";
            cbkaedah.DisplayMember = "Value";
            cbkaedah.SelectedIndex = -1;
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
    }
}
