using PortableApps.Model;
using PortableApps.Model.DTO;
using PortableApps.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
        IParlimenRepo ParlimenRepo = new ParlimenRepo();
        IDaerahRepo DaerahRepo = new DaerahRepo();
        IDunRepo DunRepo = new DunRepo();

        private int PageSize { get; set; }
        private string sidx { get; set; }
        private string sord { get; set; }
        private int xcurrentPage { get; set; }
        int totalRecords;

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

            VariableSetting varPageSize = VariableSettingRepo.GetBy("PageSize");
            if (varPageSize == null)
            {
                PageSize = 2;
            }
            else
            {
                PageSize = Convert.ToInt32(varPageSize.Value);
            }
            xcurrentPage = 1;
            sidx = "id_makkebun";
            sord = "ASC";
            BindGrid(xcurrentPage);

            LoadNegeri();
            LoadParlimen();
        }


        private void LoadParlimen()
        {
            IList<parlimen> lstEnt = ParlimenRepo.GetAll();
            cbparlimen.Items.Clear();
            cbparlimen.DataSource = lstEnt;
            cbparlimen.DisplayMember = "kawasan";
            cbparlimen.ValueMember = "negeri";
            cbparlimen.SelectedIndex = -1;
        }

        private void LoadDaerah(string negeri)
        {
            IList<tdaerah> lstEnt = DaerahRepo.GetDaerahBy(negeri);
            //cbdaerah.Items.Clear();
            cbdaerah.DataSource = lstEnt;
            cbdaerah.DisplayMember = "daerah";
            cbdaerah.ValueMember = "kod_daerah";
            cbdaerah.SelectedIndex = -1;
        }

        private void LoadDun(string negeri)
        {
            IList<dun> lstEnt = DunRepo.GetDunBy(negeri);
            //cbdaerah.Items.Clear();
            cbdun.DataSource = lstEnt;
            cbdun.DisplayMember = "dun_desc";
            cbdun.ValueMember = "kod_dun";
            cbdun.SelectedIndex = -1;
        }

        private void LoadNegeri()
        {
            IList<variables> lstEnt = VariablesRepo.GetVariableNegeri("negeri");
            //cbdaerah.Items.Clear();
            cbnegeri.DataSource = lstEnt;
            cbnegeri.DisplayMember = "value";
            cbnegeri.ValueMember = "code";
            cbnegeri.SelectedIndex = -1;
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
            makkebun.appinfo_id = appinfo_id;
            makkebun.addr1 = txtaddr1.Text;
            makkebun.addr2 = txtaddr2.Text;
            makkebun.addr3 = txtaddr3.Text;
            makkebun.catatan = txtcatatan.Text;
            makkebun.luaslesen = Convert.ToDouble(txtluaslesen.Text);
            makkebun.luasmatang = Convert.ToDouble(txtluasmatang.Text);
            makkebun.nolesen = txtnolesen.Text;
            makkebun.nolot = txtnolot.Text;
            makkebun.tarikhtebang = txttarikhtebang.Text;
            makkebun.negeri = cbnegeri.SelectedValue.ToString();
            makkebun.daerah = cbdaerah.SelectedValue.ToString();
            makkebun.dun = cbdun.SelectedValue.ToString();
            makkebun.parlimen = ParlimenRepo.GetParlimenIDBy(cbparlimen.SelectedValue.ToString());
            makkebun.syarattanah = cbsyarattanah.SelectedValue.ToString();
            makkebun.hakmiliktanah = cbjenishakmiliktanah.SelectedValue.ToString();
            makkebun.pemilikan = cbpemilikan.SelectedValue.ToString();
            makkebun.pengurusan = cbpengurusan.SelectedValue.ToString();
            makkebun.tncr = cbtncr.SelectedValue.ToString();

            if (IsNew)
            {
                MakkebunRepo.Create(makkebun);
            }
            else
            {
                MakkebunRepo.Edit(makkebun);
            }
            BindGrid(xcurrentPage);
            MessageBox.Show("Data berhasil disimpan [" + refno + " | " + txtnolot.Text + "]");
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            Label lblPage = new Label();
            lblPage.Location = new System.Drawing.Point(0, 5);
            lblPage.Size = new System.Drawing.Size(60, 20);
            lblPage.Name = "lblPage";
            lblPage.Text = "Halaman";
            pnlPager.Controls.Add(lblPage);
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(60 + (40 * count), 5);
                btnPage.Size = new System.Drawing.Size(40, 20);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                pnlPager.Controls.Add(btnPage);
                count++;
            }
        }

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            xcurrentPage = int.Parse(btnPager.Name);
            this.BindGrid(xcurrentPage);
        }

        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }

        private void BindGrid(int pageIndex)
        {
            IList<makkebunDTO> lstEnt = MakkebunRepo.PagedListDTO(pageIndex, PageSize, sidx, sord, out totalRecords, new makkebun { appinfo_id = appinfo_id });
            dgvMakKebun.DataSource = lstEnt;
            int recordCount = Convert.ToInt32(totalRecords);
            this.PopulatePager(recordCount, pageIndex);

            //dgvMakKebun.Columns.Add("Edit", "Edit");
            //dgvMakKebun.Columns.Add("Delete", "Delete");

            for (int i = 0; i < dgvMakKebun.Columns.Count; i++)
            {
                dgvMakKebun.Columns[i].Visible = false;
            }

            DataGridViewButtonColumn Edit = new DataGridViewButtonColumn();
            Edit.Name = "Edit";
            Edit.Text = "Edit";
            Edit.HeaderText = "";
            Edit.UseColumnTextForButtonValue = true;
            if (dgvMakKebun.Columns["Edit"] == null)
            {
                dgvMakKebun.Columns.Insert(0, Edit);
            }

            DataGridViewButtonColumn Delete = new DataGridViewButtonColumn();
            Delete.Name = "Delete";
            Delete.Text = "Delete";
            Delete.HeaderText = "";
            Delete.UseColumnTextForButtonValue = true;
            if (dgvMakKebun.Columns["Delete"] == null)
            {
                dgvMakKebun.Columns.Insert(1, Delete);
            }

            //dgvMakKebun.Columns["id_makkebun"].Visible = false;
            //dgvMakKebun.Columns["appinfo_id"].Visible = false;

            dgvMakKebun.Columns["addr1"].DisplayIndex = 2;
            dgvMakKebun.Columns["addr2"].DisplayIndex = 3;
            dgvMakKebun.Columns["addr3"].DisplayIndex = 4;
            dgvMakKebun.Columns["nolot"].DisplayIndex = 5;
            dgvMakKebun.Columns["luaslesen"].DisplayIndex = 6;
            dgvMakKebun.Columns["nolesen"].DisplayIndex = 7;
            dgvMakKebun.Columns["pemilikan"].DisplayIndex = 8;
            dgvMakKebun.Columns["tarikhtebang"].DisplayIndex = 9;
            dgvMakKebun.Columns["pengurusan"].DisplayIndex = 10;

            dgvMakKebun.Columns["nolot"].HeaderText = "No. Lot";
            dgvMakKebun.Columns["luaslesen"].HeaderText = "Kaw. Tanaman Sawit";
            dgvMakKebun.Columns["nolesen"].HeaderText = "No. Lesen";
            dgvMakKebun.Columns["pemilikan"].HeaderText = "Taraf Pemilikan";

            dgvMakKebun.Columns["tarikhtebang"].HeaderText = "Lawatan Pengesahan Kebun";
            dgvMakKebun.Columns["pengurusan"].HeaderText = "Pengurusan";

            dgvMakKebun.Columns[0].Visible = true;
            dgvMakKebun.Columns[1].Visible = true;
            dgvMakKebun.Columns["addr1"].Visible = true;
            dgvMakKebun.Columns["addr2"].Visible = true;
            dgvMakKebun.Columns["addr3"].Visible = true;
            dgvMakKebun.Columns["nolot"].Visible = true;
            dgvMakKebun.Columns["luaslesen"].Visible = true;
            dgvMakKebun.Columns["nolesen"].Visible = true;
            dgvMakKebun.Columns["pemilikan"].Visible = true;
            dgvMakKebun.Columns["tarikhtebang"].Visible = true;
            dgvMakKebun.Columns["pengurusan"].Visible = true;

        }

        private void cbnegeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.SelectedValue != null)
            {
                LoadDaerah(cbx.SelectedValue.ToString());
                LoadDun(cbx.SelectedValue.ToString());
            }
        }

        private void dgvMakKebun_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.ColumnIndex == dgvMakKebun.Columns["Edit"].Index)
            {

                SetupFormMakKebun(Convert.ToInt32(dgv["appinfo_id", e.RowIndex].Value), Convert.ToInt32(dgv["id_makkebun", e.RowIndex].Value));
                //Do something with your button.
            }
        }

        private void ClearMakKebunForm()

        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else if (control is ComboBox)
                    {
                        (control as ComboBox).SelectedIndex = -1;
                    }
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void SetupFormMakKebun(int appinfo_id, int id_makkebun)
        {
            this.appinfo_id = appinfo_id;
            this.id_makkebun = id_makkebun;
            makkebun makkebun = MakkebunRepo.GetBy(id_makkebun);

            txtaddr1.Text = makkebun.addr1;
            txtaddr2.Text = makkebun.addr2;
            txtaddr3.Text = makkebun.addr3;
            txtcatatan.Text = makkebun.catatan;
            txtluaslesen.Text = makkebun.luaslesen.ToString("#.##");
            txtluasmatang.Text = makkebun.luasmatang.ToString("#.##");
            txtnolesen.Text = makkebun.nolesen;
            txtnolot.Text = makkebun.nolot;
            DateTime dttarikhtebang = DateTime.ParseExact(makkebun.tarikhtebang, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            txttarikhtebang.Text = dttarikhtebang.ToString();
            cbnegeri.SelectedValue = makkebun.negeri;
            cbdaerah.SelectedValue = makkebun.daerah;
            cbdun.SelectedValue = makkebun.dun;
            cbparlimen.SelectedValue = ParlimenRepo.GetBy(makkebun.parlimen).Negeri;
            cbsyarattanah.SelectedValue = makkebun.syarattanah;
            cbjenishakmiliktanah.SelectedValue = makkebun.hakmiliktanah;
            cbpemilikan.SelectedValue = makkebun.pemilikan;
            cbpengurusan.SelectedValue = makkebun.pengurusan;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearMakKebunForm();
            id_makkebun = 0;
        }

        private void cbjenishakmiliktanah_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbjenishakmiliktanah.SelectedValue != null && cbjenishakmiliktanah.SelectedValue.ToString() == "TNCR")
            {
                List<KeyValue> lstKV = new List<KeyValue>();
                lstKV.Add(new KeyValue() { Key = "tanah_ukur_warta", Value = "Tanah Yang Diukur Dan Diwarta" });
                lstKV.Add(new KeyValue() { Key = "tanah_ukur_belum_warta", Value = "Tanah Yang Diukur Dan Belum Diwarta" });
                lstKV.Add(new KeyValue() { Key = "tanah_geran", Value = "Tanah Geran" });
                lstKV.Add(new KeyValue() { Key = "tanah_luar_kategori", Value = "Tanah Diluar Kategori 1,2 Dan 3" });
                cbtncr.DataSource = lstKV;
                cbtncr.ValueMember = "Key";
                cbtncr.DisplayMember = "Value";
                cbtncr.SelectedIndex = -1;
                cbtncr.Enabled = true;
            }
            else
            {
                List<KeyValue> lstKV = new List<KeyValue>();
                lstKV.Add(new KeyValue() { Key = "", Value = "" });
                cbtncr.DataSource = lstKV;
                cbtncr.ValueMember = "Key";
                cbtncr.DisplayMember = "Value";
                cbtncr.SelectedIndex = 0;
                cbtncr.Enabled = false;
            }
        }

        private void dgvMakKebun_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvMakKebun.Rows)
            {
                row.Cells["tarikhtebang"] = new DataGridViewLinkCell();
            }
        }
    }
}