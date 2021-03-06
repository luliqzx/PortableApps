﻿using PortableApps.Common;
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
        #region Fields/ Properties

        IAppInfoRepo AppInfoRepo = new AppInfoRepo();
        IVariablesRepo VariablesRepo = new VariablesRepo();
        IMakkebunRepo MakkebunRepo = new MakkebunRepo();
        IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
        IParlimenRepo ParlimenRepo = new ParlimenRepo();
        IDaerahRepo DaerahRepo = new DaerahRepo();
        IDunRepo DunRepo = new DunRepo();

        public int page { get; set; }
        public int rowcount { get; set; }
        public int pagesize { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public int pagecount { get; set; }

        public int appinfo_id { get; set; }
        public string refno_new { get; set; }
        public int id_makkebun { get; set; }

        IList<variables> lstWilayah = new List<variables>();

        #endregion

        #region Constructor
        public makkebunform()
        {
            InitializeComponent();
        }
        #endregion

        #region Functions

        public void RefreshGrid()
        {
            BindGrid(page);
        }

        private void BindingPageSize()
        {
            comboBox1.Items.Clear();
            List<KeyValue> lstKV = new List<KeyValue>();
            lstKV.Add(new KeyValue { Key = "15", Value = "15" });
            lstKV.Add(new KeyValue { Key = "30", Value = "30" });
            lstKV.Add(new KeyValue { Key = "45", Value = "45" });
            lstKV.Add(new KeyValue { Key = "60", Value = "60" });
            comboBox1.DataSource = lstKV;
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
            comboBox1.SelectedIndex = 0;
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
                        if (control.Name != "comboBox1")
                        {
                            (control as ComboBox).SelectedIndex = -1;
                        }
                    }
                    else if (control is RadioButton)
                    {
                        (control as RadioButton).Checked = false;
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
            if (makkebun.tebang == "SUDAH")
            {
                rbSudah.Checked = true;
                DateTime dttarikhtebang = DateTime.ParseExact(makkebun.tarikhtebang, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                txttarikhtebang.Text = dttarikhtebang.ToString();
                txttarikhtebang.Value = dttarikhtebang;
            }
            else if (makkebun.tebang == "BELUM")
            {
                rbBelum.Checked = true;
            }
            else
            {
                rbBelum.Checked = false;
                rbSudah.Checked = false;
            }

            cbnegeri.SelectedValue = makkebun.negeri;
            cbdaerah.SelectedValue = makkebun.daerah;
            cbdun.SelectedValue = makkebun.dun;
            cbparlimen.SelectedValue = makkebun.parlimen;
            cbsyarattanah.SelectedValue = makkebun.syarattanah;
            cbjenishakmiliktanah.SelectedValue = makkebun.hakmiliktanah;
            cbpemilikan.SelectedValue = makkebun.pemilikan;
            cbpengurusan.SelectedValue = makkebun.pengurusan;
            cbtncr.SelectedValue = makkebun.tncr;
        }

        private void FocusChangeBackColor()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                        control.HookFocusChangeBackColor(Color.Khaki);
                    else if (control is ComboBox)
                    {
                        control.HookFocusChangeBackColor(Color.Khaki);
                    }
                    else if (control is DateTimePicker)
                    {
                        control.HookFocusChangeBackColor(Color.Khaki);
                    }
                    else
                        func(control.Controls);
                }
            };

            func(Controls);
        }

        private void LoadParlimen(string negeri = "")
        {
            IList<parlimen> lstEnt = ParlimenRepo.GetParlimentBy(negeri);
            lstEnt.Insert(0, new parlimen { Id = 0, Kawasan = "" });
            cbparlimen.DataSource = lstEnt;
            cbparlimen.DisplayMember = "kawasan";
            cbparlimen.ValueMember = "id";
            cbparlimen.SelectedIndex = 0;
        }

        private void LoadDaerah(string negeri)
        {
            IList<tdaerah> lstEnt = DaerahRepo.GetDaerahBy(negeri);
            //cbdaerah.Items.Clear();
            lstEnt.Insert(0, new tdaerah { daerah = "", kod_daerah = "" });
            cbdaerah.DataSource = lstEnt;
            cbdaerah.DisplayMember = "daerah";
            cbdaerah.ValueMember = "kod_daerah";
            cbdaerah.SelectedIndex = 0;
        }

        private void LoadDun(string negeri)
        {
            IList<dun> lstEnt = DunRepo.GetDunBy(negeri);
            //cbdaerah.Items.Clear();
            lstEnt.Insert(0, new dun { dun_desc = "", kod_dun = "" });
            cbdun.DataSource = lstEnt;
            cbdun.DisplayMember = "dun_desc";
            cbdun.ValueMember = "kod_dun";
            cbdun.SelectedIndex = 0;
        }

        private void LoadNegeri()
        {
            IList<variables> lstEnt = VariablesRepo.GetVariableByType("negeri");
            //cbdaerah.Items.Clear();
            lstEnt.Insert(0, new variables { Code = "", Value = "" });
            cbnegeri.DataSource = lstEnt;
            cbnegeri.DisplayMember = "value";
            cbnegeri.ValueMember = "code";
            cbnegeri.SelectedIndex = 0;
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
            lstKV.Add(new KeyValue() { Key = "Pajakan", Value = "Pajakan" });
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

            lblnegeri.Text = "";
            variables varNegeri = VariablesRepo.GetNegeri("negeri", appinfo.negeri);
            if (varNegeri != null)
            {
                lblnegeri.Text = varNegeri.Value;
            }

            tdaerah daerah = DaerahRepo.GetBy(appinfo.daerah);
            string sdaerah = "";
            if (daerah != null)
            {
                sdaerah = daerah.daerah;
            }
            parlimen parlimen = ParlimenRepo.GetBy(appinfo.parlimen);
            string sparlimen = "";
            if (parlimen != null)
            {
                sparlimen = parlimen.Kawasan;
            }
            lbldaerah.Text = sdaerah + " / " + sparlimen;
            lblnokp.Text = appinfo.icno;
            lblnolesen.Text = appinfo.nolesen;
            lblbandar.Text = appinfo.bandar;
            lblposkod.Text = appinfo.poskod;
            LoadWilayah(appinfo.negeri);
        }

        private void LoadWilayah(string negeri)
        {
            variables variables = VariablesRepo.GetVariableByType("NEGERI").FirstOrDefault(x => x.Code == negeri);
            if (variables != null)
            {
                lblwilayah.Text = GetAliasesParent(variables.Parent);
            }
        }

        private string GetAliasesParent(string parent)
        {
            variables varByParent = lstWilayah.FirstOrDefault(x => x.Code == parent);
            if (varByParent != null)
            {
                parent = varByParent.Value;
            }
            //if (parent == "UTR")
            //{
            //    parent = "UTARA";
            //}
            //else if (parent == "TMR")
            //{
            //    parent = "TIMUR";
            //}
            //else if (parent == "TGH")
            //{
            //    parent = "TENGAH";
            //}
            //else if (parent == "SEL")
            //{
            //    parent = "SELATAN";
            //}
            return parent;
        }

        #endregion

        //#region Pager & Binding Grid

        //private void PopulatePager(int recordCount, int currentPage)
        //{
        //    List<Page> pages = new List<Page>();
        //    int startIndex, endIndex;
        //    int pagerSpan = 5;

        //    //Calculate the Start and End Index of pages to be displayed.
        //    double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        //    int pageCount = (int)Math.Ceiling(dblPageCount);
        //    startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        //    endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        //    if (currentPage > pagerSpan % 2)
        //    {
        //        if (currentPage == 2)
        //        {
        //            endIndex = 5;
        //        }
        //        else
        //        {
        //            endIndex = currentPage + 2;
        //        }
        //    }
        //    else
        //    {
        //        endIndex = (pagerSpan - currentPage) + 1;
        //    }

        //    if (endIndex - (pagerSpan - 1) > startIndex)
        //    {
        //        startIndex = endIndex - (pagerSpan - 1);
        //    }

        //    if (endIndex > pageCount)
        //    {
        //        endIndex = pageCount;
        //        startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        //    }

        //    //Add the First Page Button.
        //    if (currentPage > 1)
        //    {
        //        pages.Add(new Page { Text = "First", Value = "1" });
        //    }

        //    //Add the Previous Button.
        //    if (currentPage > 1)
        //    {
        //        pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
        //    }

        //    for (int i = startIndex; i <= endIndex; i++)
        //    {
        //        pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
        //    }

        //    //Add the Next Button.
        //    if (currentPage < pageCount)
        //    {
        //        pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
        //    }

        //    //Add the Last Button.
        //    if (currentPage != pageCount)
        //    {
        //        pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
        //    }

        //    //Clear existing Pager Buttons.
        //    pnlPager.Controls.Clear();

        //    //Loop and add Buttons for Pager.
        //    int count = 0;
        //    Label lblPage = new Label();
        //    lblPage.Location = new System.Drawing.Point(0, 5);
        //    lblPage.Size = new System.Drawing.Size(60, 20);
        //    lblPage.Name = "lblPage";
        //    lblPage.Text = "Halaman";
        //    pnlPager.Controls.Add(lblPage);
        //    foreach (Page page in pages)
        //    {
        //        Button btnPage = new Button();
        //        btnPage.Location = new System.Drawing.Point(60 + (40 * count), 5);
        //        btnPage.Size = new System.Drawing.Size(40, 20);
        //        btnPage.Name = page.Value;
        //        btnPage.Text = page.Text;
        //        btnPage.Enabled = !page.Selected;
        //        btnPage.Click += new System.EventHandler(this.Page_Click);
        //        pnlPager.Controls.Add(btnPage);
        //        count++;
        //    }
        //}

        //private void Page_Click(object sender, EventArgs e)
        //{
        //    Button btnPager = (sender as Button);
        //    xcurrentPage = int.Parse(btnPager.Name);
        //    this.BindGrid(xcurrentPage);
        //}

        //public class Page
        //{
        //    public string Text { get; set; }
        //    public string Value { get; set; }
        //    public bool Selected { get; set; }
        //}

        //private void BindGrid(int pageIndex)
        //{
        //    IList<makkebunDTO> lstEnt = MakkebunRepo.PagedListDTO(pageIndex, PageSize, sidx, sord, out totalRecords, new makkebun { appinfo_id = appinfo_id });
        //    dgvMakKebun.DataSource = lstEnt;
        //    int recordCount = Convert.ToInt32(totalRecords);
        //    this.PopulatePager(recordCount, pageIndex);

        //    //dgvMakKebun.Columns.Add("Edit", "Edit");
        //    //dgvMakKebun.Columns.Add("Delete", "Delete");

        //}

        //#endregion

        #region Events

        private void btnFirst_Click(object sender, EventArgs e)
        {
            page = 1;
            txtPageIndex.Text = page.ToString();
            BindGrid(page);
            lblRowView.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            page = page - 1 <= 0 ? 1 : page - 1 > pagecount ? pagecount : page - 1;
            txtPageIndex.Text = page.ToString();
            BindGrid(page);
            lblRowView.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            int ips = Convert.ToInt32(cbx.SelectedValue);
            pagesize = ips;
            BindGrid(page);
            lblRowView.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1 > rowcount ? rowcount + 1 : ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            page = pagecount > 0 ? pagecount : 1;
            txtPageIndex.Text = page.ToString();
            BindGrid(page);
            lblRowView.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page = page + 1 > pagecount ? pagecount > 0 ? pagecount : 1 : page + 1;
            txtPageIndex.Text = page.ToString();
            BindGrid(page);
            lblRowView.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                int iText = Convert.ToInt32(txtPageIndex.Text);
                if (iText < 1)
                {
                    page = 1;
                }
                else if (iText > pagecount)
                {
                    page = pagecount;
                }
                else
                {
                    page = iText;
                }
                txtPageIndex.Text = page.ToString();
                BindGrid(page);
            }
        }

        private void cbnegeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.SelectedValue != null)
            {
                LoadDaerah(cbx.SelectedValue.ToString());
                LoadDun(cbx.SelectedValue.ToString());
                LoadParlimen(cbx.SelectedValue.ToString());
            }
        }

        private void dgvMakKebun_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (e.RowIndex >= 0)
            {
                int pappinfo_id = Convert.ToInt32(dgv["appinfo_id", e.RowIndex].Value);
                int pid_makkebun = Convert.ToInt32(dgv["id_makkebun", e.RowIndex].Value);
                if (e.ColumnIndex == dgvMakKebun.Columns["Edit"].Index)
                {
                    SetupFormMakKebun(pappinfo_id, pid_makkebun);
                    //Do something with your button.
                }
                else if (e.ColumnIndex == dgvMakKebun.Columns["Delete"].Index)
                {
                    var confirmResult = MessageBox.Show("Apakah anda yakin menghapus data [" + dgvMakKebun["nolot", e.RowIndex].Value + "] ?",
                                     "Konfirmasi Hapus [" + dgvMakKebun["nolot", e.RowIndex].Value + "] !!",
                                     MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        int iDelete = MakkebunRepo.Delete(pid_makkebun);
                        if (iDelete > 0)
                        {
                            MessageBox.Show("Data berhasil dihapus. [" + dgvMakKebun["nolot", e.RowIndex].Value + "]");
                        }
                        BindGrid(page);
                        // If 'Yes', do something here.
                    }
                    else
                    {
                        // If 'No', do something here.
                    }
                }
                else if (e.ColumnIndex == dgvMakKebun.Columns["tarikh_lawat"].Index)
                {
                    foreach (Form f in ParentForm.MdiChildren)
                    {
                        if (f.GetType() == typeof(lawatanpengesahankebunform))
                        {
                            //f.Activate();
                            //return;
                            f.Close();
                        }
                    }
                    lawatanpengesahankebunform form = new lawatanpengesahankebunform();
                    form.appinfo_id = pappinfo_id;
                    form.refno_new = refno_new;
                    form.id_makkebun = pid_makkebun;
                    int? semak_tapak_id = Convert.ToInt32(dgv["semak_tapak_id", e.RowIndex].Value);
                    form.semak_tapak_id = semak_tapak_id;

                    form.MdiParent = ParentForm;
                    form.Show();
                    //SetupFormMakKebun(pappinfo_id, pid_makkebun);
                    //Do something with your button.
                }
            }
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
                row.Cells["tarikh_lawat"] = new DataGridViewLinkCell();
            }


            for (int i = 0; i < dgvMakKebun.Columns.Count; i++)
            {
                dgvMakKebun.Columns[i].Visible = false;
            }

            DataGridViewImageColumn Edit = new DataGridViewImageColumn();
            Edit.Name = "Edit";
            Edit.Frozen = true;
            Edit.Image = Properties.Resources.b_edit;
            Edit.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //Edit.Text = "Edit";
            Edit.HeaderText = "";
            //Edit.UseColumnTextForButtonValue = true;
            if (dgvMakKebun.Columns["Edit"] == null)
            {
                dgvMakKebun.Columns.Insert(0, Edit);
            }

            DataGridViewImageColumn Delete = new DataGridViewImageColumn();
            Delete.Name = "Delete";
            Delete.Frozen = true;
            //Delete.Text = "Delete";
            Delete.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Delete.Image = Properties.Resources.b_drop;
            Delete.HeaderText = "";
            //Delete.UseColumnTextForButtonValue = true;
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
            dgvMakKebun.Columns["tarikh_lawat"].DisplayIndex = 9;
            dgvMakKebun.Columns["pengurusan"].DisplayIndex = 10;

            dgvMakKebun.Columns["addr1"].HeaderText = "Alamat (Baris 1)";
            dgvMakKebun.Columns["addr2"].HeaderText = "Alamat (Baris 2)";
            dgvMakKebun.Columns["addr3"].HeaderText = "Alamat (Baris 3)";

            dgvMakKebun.Columns["nolot"].HeaderText = "No. Lot";
            dgvMakKebun.Columns["luaslesen"].HeaderText = "Kaw. Tanaman Sawit";
            dgvMakKebun.Columns["nolesen"].HeaderText = "No. Lesen";
            dgvMakKebun.Columns["pemilikan"].HeaderText = "Taraf Pemilikan";

            dgvMakKebun.Columns["tarikh_lawat"].HeaderText = "Lawatan Pengesahan Kebun";
            dgvMakKebun.Columns["pengurusan"].HeaderText = "Pengurusan";
            dgvMakKebun.Columns["syncdate"].HeaderText = "Server Sync Date";

            dgvMakKebun.Columns[0].Visible = true;
            dgvMakKebun.Columns[1].Visible = true;
            dgvMakKebun.Columns["addr1"].Visible = true;
            dgvMakKebun.Columns["addr2"].Visible = true;
            dgvMakKebun.Columns["addr3"].Visible = true;
            dgvMakKebun.Columns["nolot"].Visible = true;
            dgvMakKebun.Columns["luaslesen"].Visible = true;
            dgvMakKebun.Columns["nolesen"].Visible = true;
            dgvMakKebun.Columns["pemilikan"].Visible = true;
            dgvMakKebun.Columns["tarikh_lawat"].Visible = true;
            dgvMakKebun.Columns["pengurusan"].Visible = true;
            dgvMakKebun.Columns["syncdate"].Visible = false;
        }

        private void dgvMakKebun_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            string snumber = ((pagesize * (page - 1)) + Convert.ToInt32(rowIdx)).ToString();

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(snumber, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

        }

        private void txttarikhtebang_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            if (dtp.Value > DateTime.MinValue)
            {
                dtp.CustomFormat = "dd-MM-yyyy";
            }
        }

        private void rbBelum_CheckedChanged(object sender, EventArgs e)
        {
            txttarikhtebang.Checked = false;
            txttarikhtebang.CustomFormat = " ";
        }

        private void rbSudah_CheckedChanged(object sender, EventArgs e)
        {
            txttarikhtebang.Checked = true;
            txttarikhtebang.CustomFormat = "dd-MM-yyyy";
        }

        private void TextBoxNumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            sidx = dgv.Columns[e.ColumnIndex].Name;
            if (sord == "ASC")
            {
                sord = "DESC";
            }
            else
            {
                sord = "ASC";
            }
            BindGrid(page);
        }

        private void makkebunform_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();

            lstWilayah = VariablesRepo.GetVariableByType("wilayah");

            txttarikhtebang.CustomFormat = " ";

            groupBox1.Text = groupBox1.Text + " " + refno_new;
            groupBox2.Text = groupBox2.Text + " " + refno_new;


            BindSyaratTanah();

            BindPemilikan();

            BindJenisHakMilikTanah();

            BindPengurusan();

            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            BindingPageSize();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            txtPageIndex.Text = "1";

            page = 1;
            pagesize = Convert.ToInt32(comboBox1.SelectedValue);

            page = 1;
            sidx = "id_makkebun";
            sord = "ASC";
            BindGrid(page);

            LoadNegeri();
            LoadParlimen();

            BindMaklumatPemohon(appinfo_id);

            FocusChangeBackColor();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                if (appinfo_id <= 0)
                {
                    MessageBox.Show("Harap masukan data dari Maklumat Pemohon.");
                    return;
                }

                IList<Control> lstCtrlEmptyCheck = new List<Control>();
                lstCtrlEmptyCheck.Add(txtaddr1);
                lstCtrlEmptyCheck.Add(txtaddr2);
                lstCtrlEmptyCheck.Add(cbnegeri);
                //lstCtrlEmptyCheck.Add(cbdaerah);
                //lstCtrlEmptyCheck.Add(cbdun);
                //lstCtrlEmptyCheck.Add(cbparlimen);
                lstCtrlEmptyCheck.Add(txtnolot);
                lstCtrlEmptyCheck.Add(txtluasmatang);
                lstCtrlEmptyCheck.Add(cbsyarattanah);
                lstCtrlEmptyCheck.Add(txtnolesen);
                lstCtrlEmptyCheck.Add(cbjenishakmiliktanah);
                lstCtrlEmptyCheck.Add(cbpengurusan);
                lstCtrlEmptyCheck.Add(cbpemilikan);

                if (WFUtils.CheckControllCollectionEmpty(lstCtrlEmptyCheck))
                {
                    return;
                }

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
                makkebun.luaslesen = Convert.ToDouble(string.IsNullOrEmpty(txtluaslesen.Text) ? 0 : Convert.ToDouble(txtluaslesen.Text));
                makkebun.luasmatang = Convert.ToDouble(txtluasmatang.Text);
                makkebun.nolesen = txtnolesen.Text;
                makkebun.nolot = txtnolot.Text;
                makkebun.tarikhtebang = txttarikhtebang.Text;
                makkebun.negeri = cbnegeri.SelectedValue.ToString();
                makkebun.daerah = cbdaerah.SelectedValue == null ? "" : cbdaerah.SelectedValue.ToString();
                makkebun.dun = cbdun.SelectedValue == null ? "" : cbdun.SelectedValue.ToString();
                makkebun.parlimen = ParlimenRepo.GetParlimenIDBy(cbparlimen.SelectedValue.ToString());
                makkebun.syarattanah = cbsyarattanah.SelectedValue.ToString();
                makkebun.hakmiliktanah = cbjenishakmiliktanah.SelectedValue.ToString();
                makkebun.pemilikan = cbpemilikan.SelectedValue.ToString();
                makkebun.pengurusan = cbpengurusan.SelectedValue.ToString();
                makkebun.tncr = cbtncr.SelectedValue.ToString();
                makkebun.tebang = rbSudah.Checked ? "SUDAH" : rbBelum.Checked ? "BELUM" : "";


                if (IsNew)
                {
                    try
                    {
                        makkebun.created = DateTime.Now;
                        makkebun.createdby = VariableSettingRepo.GetBy("UserKeyIn").Value;
                        MakkebunRepo.Create(makkebun);
                        MessageBox.Show("Data berhasil disimpan [" + refno_new + " | " + txtnolot.Text + "]");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.GetFullMessage());
                    }

                }
                else
                {
                    try
                    {
                        MakkebunRepo.Edit(makkebun);
                        MessageBox.Show("Data berhasil disimpan [" + refno_new + " | " + txtnolot.Text + "]");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.GetFullMessage());
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetFullMessage() + " - Harap periksa data input kembali");
                return;
            }
            BindGrid(page);

            btnReset.PerformClick();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearMakKebunForm();
            id_makkebun = 0;
            comboBox1.SelectedIndex = 0;
        }
        #endregion

        #region Pager & Binding Grid

        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pagesize));
            pagecount = (int)Math.Ceiling(dblPageCount);
            lblPageView.Text = "of " + pagecount;
        }

        private void Page_Click(object sender, EventArgs e)
        {
        }

        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }

        private void BindGrid(int pageIndex)
        {
            page = pageIndex;
            int rowcount = 0;
            IList<makkebunDTO> lstEnt = MakkebunRepo.PagedListDTO(pageIndex, pagesize, sidx, sord, out rowcount, new makkebun { appinfo_id = appinfo_id });
            this.rowcount = rowcount;
            dgvMakKebun.DataSource = lstEnt;
            this.PopulatePager(rowcount, pageIndex);
            lblRowView.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        #endregion

        #region Common Class

        public class KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        #endregion
    }
}