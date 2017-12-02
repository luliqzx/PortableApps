using PortableApps.Model;
using PortableApps.Model.DTO;
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
        ISemakTapakRepo SemakTapakRepo = new SemakTapakRepo();
        IMakkebunRepo MakkebunRepo = new MakkebunRepo();

        public int appinfo_id { get; set; }
        public string refno { get; set; }
        public int id_makkebun { get; set; }
        public int? semak_tapak_id;

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
            txttarikh_lawat.Format = DateTimePickerFormat.Custom;
            txttarikh_lawat.CustomFormat = "dd-MM-yyyy";

            BindKaedah();

            BindJenisTanah();

            groupBox1.Text = groupBox1.Text + " " + refno;
            groupBox2.Text = groupBox2.Text + " " + refno;

            cbnolot.SelectedIndexChanged -= cbnolot_SelectedIndexChanged;
            BindNoLot();
            cbnolot.SelectedIndexChanged += cbnolot_SelectedIndexChanged;
            BindFormLawatanPengesahanKebun(appinfo_id, id_makkebun, semak_tapak_id);

        }

        private void BindNoLot()
        {
            IList<makkebunDTO> lstmakkebun = MakkebunRepo.GetAllAppInfoBy(appinfo_id);
            cbnolot.DataSource = lstmakkebun;
            cbnolot.DisplayMember = "nolot";
            cbnolot.ValueMember = "id_makkebun";
            cbnolot.SelectedIndex = -1;
        }

        private void BindFormLawatanPengesahanKebun(int appinfo_id, int id_makkebun, int? semak_tapak_id)
        {
            semak_tapak semak_tapak = null;
            if (semak_tapak_id == null || semak_tapak_id < 1)
            {
                semak_tapak = new semak_tapak();
            }
            else
            {
                semak_tapak = SemakTapakRepo.GetBy(appinfo_id, id_makkebun, semak_tapak_id);
            }

            if (id_makkebun > 0)
            {
                cbnolot.SelectedValue = id_makkebun;
            }

            if (!string.IsNullOrEmpty(semak_tapak.jenis_tanah))
            {
                cbjenis_tanah.SelectedValue = semak_tapak.jenis_tanah;
            }
            txtbantuan.Text = semak_tapak.bantuan;
            txtbil_pokok_tua.Text = semak_tapak.bil_pokok_tua;
            txtcatatan.Text = semak_tapak.catatan;
            //cbjenis_tanah.SelectedValue = semak_tapak.ganoderma;
            txthasil.Text = semak_tapak.hasil;
            //cbjenis_tanah.SelectedValue = semak_tapak.jentera;
            if (!string.IsNullOrEmpty(semak_tapak.kaedah))
            {
                cbkaedah.SelectedValue = semak_tapak.kaedah;
            }
            //txtker.SelectedValue = semak_tapak.kecerunan;
            //cbjenis_tanah.SelectedValue = semak_tapak.lampiran;
            txtluas.Text = semak_tapak.luas.ToString("#.##");
            txtperatusan_serangan.Text = semak_tapak.peratusan_serangan.ToString("#.##");
            txtptk_lawat.Text = semak_tapak.ptk_lawat;
            txttarikh_lawat.Text = semak_tapak.tarikh_lawat;
            txtumr_pokok_tua.Text = semak_tapak.umr_pokok_tua;

            BindRBForm(semak_tapak);
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

        private void button1_Click(object sender, EventArgs e)
        {
            bool IsNew = false;
            semak_tapak semak_tapak = SemakTapakRepo.GetBy((int)semak_tapak_id);
            if (semak_tapak == null)
            {
                semak_tapak = new semak_tapak();
                IsNew = true;
            }


            semak_tapak.id = (int)semak_tapak_id;
            semak_tapak.makkebun_id = id_makkebun;
            semak_tapak.appinfo_id = appinfo_id;
            semak_tapak.kaedah = cbkaedah.SelectedValue.ToString();
            semak_tapak.bantuan = txtbantuan.Text;
            semak_tapak.jenis_tanah = cbjenis_tanah.SelectedValue.ToString();
            semak_tapak.kecerunan = rbLebih30Darjah.Checked ? "Lebih 25 Darjah" : rbKurarng30Darjah.Checked ? "Kurang 25 Darjah" : "";
            semak_tapak.jentera = rbJenteraYa.Checked ? "Ya" : rbJenteraTidak.Checked ? "Tidak" : "";
            semak_tapak.ganoderma = rbGenodermaYa.Checked ? "Ya" : rbJenteraTidak.Checked ? "Tidak" : "";
            semak_tapak.peratusan_serangan = Convert.ToDouble(txtperatusan_serangan.Text);
            semak_tapak.umr_pokok_tua = txtumr_pokok_tua.Text;
            semak_tapak.hasil = txthasil.Text;
            semak_tapak.bil_pokok_tua = txtbil_pokok_tua.Text;
            semak_tapak.tarikh_lawat = txttarikh_lawat.Text;
            semak_tapak.ptk_lawat = txtptk_lawat.Text;
            semak_tapak.luas = Convert.ToDouble(txtluas.Text);
            semak_tapak.catatan = txtcatatan.Text;
            //semak_tapak.created = txtcreated.Text;
            //semak_tapak.createdby = txtcreatedby.Text;
            //semak_tapak.lampiran = txtlampiran.Text;
            BindRBForm(semak_tapak);

            if (IsNew)
            {
                semak_tapak.created = DateTime.Now; ;
                semak_tapak.createdby = new VariableSettingRepo().GetBy("UserKeyIn").Value;
                semak_tapak.lampiran = "";// txtlampiran.Text;
                SemakTapakRepo.Create(semak_tapak);
            }
            else
            {
                SemakTapakRepo.Edit(semak_tapak);
            }

            MessageBox.Show("Data berhasil disimpan [" + refno + " | " + MakkebunRepo.GetBy(id_makkebun).nolot + "]");
        }

        private void BindRBForm(semak_tapak semak_tapak)
        {
            if (!string.IsNullOrEmpty(semak_tapak.kecerunan))
            {
                if (semak_tapak.kecerunan == "Lebih 25 Darjah")
                {
                    rbLebih30Darjah.Checked = true;
                }
                else if (semak_tapak.kecerunan == "Kurang 25 Darjah")
                {
                    rbKurarng30Darjah.Checked = true;
                }
            }
            else
            {
                rbLebih30Darjah.Checked = false;
                rbKurarng30Darjah.Checked = false;
            }

            if (!string.IsNullOrEmpty(semak_tapak.jentera))
            {
                if (semak_tapak.jentera == "Ya")
                {
                    rbJenteraYa.Checked = true;
                }
                else if (semak_tapak.jentera == "Tidak")
                {
                    rbJenteraTidak.Checked = true;
                }
            }
            else
            {
                rbJenteraYa.Checked = false;
                rbJenteraTidak.Checked = false;
            }


            if (!string.IsNullOrEmpty(semak_tapak.ganoderma))
            {
                if (semak_tapak.ganoderma == "Ya")
                {
                    rbGenodermaYa.Checked = true;
                }
                else if (semak_tapak.ganoderma == "Tidak")
                {
                    rbGenodermaTidak.Checked = true;
                }
            }
            else
            {
                rbGenodermaYa.Checked = false;
                rbGenodermaTidak.Checked = false;
            }
        }

        private void cbnolot_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            id_makkebun = Convert.ToInt32(cbx.SelectedValue);
            semak_tapak semak_tapak = SemakTapakRepo.GetBy(appinfo_id, id_makkebun);
            semak_tapak_id = semak_tapak == null ? 0 : semak_tapak.id;
            BindFormLawatanPengesahanKebun(appinfo_id, id_makkebun, semak_tapak_id);
        }
    }
}
