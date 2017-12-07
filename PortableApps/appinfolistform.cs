using PortableApps.Common;
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
    public partial class appinfolistform : Form
    {
        #region Fields & Properties
        IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
        IBangsaRepo BangsaRepo = new BangsaRepo();
        IParlimenRepo ParlimenRepo = new ParlimenRepo();
        IDaerahRepo DaerahRepo = new DaerahRepo();
        IDunRepo DunRepo = new DunRepo();
        IVariablesRepo VariablesRepo = new VariablesRepo();
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();

        public int page { get; set; }
        public int rowcount { get; set; }
        public int pagesize { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public int pagecount { get; set; }
        #endregion

        #region Functions
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


        #endregion

        #region Constructor
        public appinfolistform()
        {
            InitializeComponent();

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
        //    appinfo pappinfo = new appinfo();
        //    pappinfo.refno = txtrefno.Text;
        //    pappinfo.negeri = cbnegeri.SelectedValue == null ? "" : cbnegeri.SelectedValue.ToString();
        //    pappinfo.bangsa = cbbangsa.SelectedValue == null ? "" : cbbangsa.SelectedValue.ToString();
        //    pappinfo.daerah = cbdaerah.SelectedValue == null ? "" : cbdaerah.SelectedValue.ToString();
        //    pappinfo.dun = cbdun.SelectedValue == null ? "" : cbdun.SelectedValue.ToString();
        //    pappinfo.parlimen = Convert.ToInt32(cbparlimen.SelectedValue == null ? 0 : cbparlimen.SelectedValue);

        //    IList<appinfoDTO> lstEnt = AppInfoRepo.PagedListDTO(pageIndex, PageSize, sidx, sord, out totalRecords, pappinfo);
        //    dgvMakPer.DataSource = lstEnt;
        //    int recordCount = Convert.ToInt32(totalRecords);
        //    this.PopulatePager(recordCount, pageIndex);

        //}

        //#endregion

        #region Events

        private void appinfolistform_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();

            LoadNegeri();
            LoadTBangsa();
            LoadParlimen();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";
            dateTimePicker1.Checked = true;
            //VariableSetting varPageSize = VariableSettingRepo.GetBy("PageSize");
            //if (varPageSize == null)
            //{
            //    pagesize = 2;
            //}
            //else
            //{
            //    pagesize = Convert.ToInt32(varPageSize.Value);
            //}
            //page = 1;
            //sidx = "ID";
            //sord = "ASC";
            //BindGrid(page);
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            BindingPageSize();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            txtPageIndex.Text = "1";

            page = 1;
            pagesize = Convert.ToInt32(comboBox1.SelectedValue);
            sidx = "id";
            sord = "asc";
            BindGrid(page);
            lblRowView.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
            FocusChangeBackColor();
        }

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
            page = pagecount;
            txtPageIndex.Text = page.ToString();
            BindGrid(page);
            lblRowView.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page = page + 1 > pagecount ? pagecount : page + 1;
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

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Form f in ParentForm.MdiChildren)
            {
                if (f.GetType() == typeof(appinfosaveform))
                {
                    //f.Activate();
                    //return;
                    f.Close();
                }
            }
            Form form = new appinfosaveform();
            form.MdiParent = ParentForm;
            form.Show();
        }

        private void dgvMakPer_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dgvMakPer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridView dgv = sender as DataGridView;

                foreach (Form f in ParentForm.MdiChildren)
                {
                    if (f.GetType() == typeof(makkebunform))
                    {
                        //f.Activate();
                        //return;
                        f.Close();
                    }
                }
                makkebunform form = new makkebunform();
                form.appinfo_id = Convert.ToInt32(dgv["id", e.RowIndex].Value);
                form.refno = Convert.ToString(dgv["refno", e.RowIndex].Value);
                form.MdiParent = ParentForm;
                form.Show();
            }
        }

        private void dgvMakPer_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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

        private void dgvMakPer_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvMakPer.Rows)
            {
                row.Cells["refno"] = new DataGridViewLinkCell();
            }

            DataGridViewImageColumn Edit = new DataGridViewImageColumn();
            Edit.Name = "Edit";
            Edit.Frozen = true;
            Edit.Image = Properties.Resources.b_edit;
            Edit.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //Edit.Text = "Edit";
            Edit.HeaderText = "";
            //Edit.UseColumnTextForButtonValue = true;
            if (dgvMakPer.Columns["Edit"] == null)
            {
                dgvMakPer.Columns.Insert(0, Edit);
            }

            DataGridViewImageColumn Delete = new DataGridViewImageColumn();
            Delete.Name = "Delete";
            Delete.Frozen = true;
            //Delete.Text = "Delete";
            Delete.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Delete.Image = Properties.Resources.b_drop;
            Delete.HeaderText = "";
            //Delete.UseColumnTextForButtonValue = true;
            if (dgvMakPer.Columns["Delete"] == null)
            {
                dgvMakPer.Columns.Insert(1, Delete);
            }


            dgvMakPer.Columns["nama"].HeaderText = "Nama Pemohon";
            dgvMakPer.Columns["icno"].HeaderText = "No Kad. Pengenalan";
            dgvMakPer.Columns["negeri"].HeaderText = "Negeri";
            dgvMakPer.Columns["nolesen"].HeaderText = "No. Lesen";
            dgvMakPer.Columns["refno"].HeaderText = "No. Rujukan";
            dgvMakPer.Columns["appdate"].HeaderText = "Tarikh Permohonan";
            dgvMakPer.Columns["createdby"].HeaderText = "Dibuat Oleh";
            dgvMakPer.Columns["created"].HeaderText = "Dibuat Tanggal";
            dgvMakPer.Columns["created"].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss";
            dgvMakPer.Columns["id"].HeaderText = "Id";
            dgvMakPer.Columns["id"].Visible = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            dtp.ShowCheckBox = true;
            if (dtp.Value > DateTime.MinValue)
            {
                dtp.CustomFormat = "dd-MM-yyyy";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            page = 1;
            BindGrid(page);
        }

        private void dgvMakPer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dgvMakPer.Columns["Edit"].Index)
                {
                    foreach (Form f in ParentForm.MdiChildren)
                    {
                        if (f.GetType() == typeof(appinfosaveform))
                        {
                            //f.Activate();
                            //return;
                            f.Close();
                        }
                    }
                    appinfosaveform form = new appinfosaveform();
                    form.appinfo_id = Convert.ToInt32(dgvMakPer["id", e.RowIndex].Value);
                    form.refno = Convert.ToString(dgvMakPer["refno", e.RowIndex].Value);
                    form.MdiParent = ParentForm;
                    form.Show();
                    //SetupFormMakKebun(pappinfo_id, pid_makkebun);
                    //Do something with your button.
                }

                if (e.ColumnIndex == dgvMakPer.Columns["Delete"].Index)
                {
                    MessageBox.Show("Currently event not use");
                    return;
                }

                if (e.ColumnIndex == dgvMakPer.Columns["refno"].Index)
                {
                    foreach (Form f in ParentForm.MdiChildren)
                    {
                        if (f.GetType() == typeof(makkebunform))
                        {
                            //f.Activate();
                            //return;
                            f.Close();
                        }
                    }
                    makkebunform form = new makkebunform();
                    form.appinfo_id = Convert.ToInt32(dgvMakPer["id", e.RowIndex].Value);
                    form.refno = Convert.ToString(dgvMakPer["refno", e.RowIndex].Value);
                    form.MdiParent = ParentForm;
                    form.Show();
                    //SetupFormMakKebun(pappinfo_id, pid_makkebun);
                    //Do something with your button.
                }
            }
        }

        #endregion

        #region Pager & Binding Grid
        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            //int startIndex, endIndex;
            //int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pagesize));
            pagecount = (int)Math.Ceiling(dblPageCount);
            lblPageView.Text = "of " + pagecount;
            //startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            //endIndex = pagecount > pagerSpan ? pagerSpan : pagecount;
            //if (currentPage > pagerSpan % 2)
            //{
            //    if (currentPage == 2)
            //    {
            //        endIndex = 5;
            //    }
            //    else
            //    {
            //        endIndex = currentPage + 2;
            //    }
            //}
            //else
            //{
            //    endIndex = (pagerSpan - currentPage) + 1;
            //}

            //if (endIndex - (pagerSpan - 1) > startIndex)
            //{
            //    startIndex = endIndex - (pagerSpan - 1);
            //}

            //if (endIndex > pagecount)
            //{
            //    endIndex = pagecount;
            //    startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            //}

            ////Add the First Page Button.
            //if (currentPage > 1)
            //{
            //    pages.Add(new Page { Text = "First", Value = "1" });
            //}

            ////Add the Previous Button.
            //if (currentPage > 1)
            //{
            //    pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            //}

            //for (int i = startIndex; i <= endIndex; i++)
            //{
            //    pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            //}

            ////Add the Next Button.
            //if (currentPage < pagecount)
            //{
            //    pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            //}

            ////Add the Last Button.
            //if (currentPage != pagecount)
            //{
            //    pages.Add(new Page { Text = "Last", Value = pagecount.ToString() });
            //}

            ////Clear existing Pager Buttons.
            //pnlPager.Controls.Clear();

            ////Loop and add Buttons for Pager.
            //int count = 0;
            //Label lblPage = new Label();
            //lblPage.Location = new System.Drawing.Point(0, 5);
            //lblPage.Size = new System.Drawing.Size(60, 20);
            //lblPage.Name = "lblPage";
            //lblPage.Text = "Halaman";
            //pnlPager.Controls.Add(lblPage);
            //foreach (Page page in pages)
            //{
            //    Button btnPage = new Button();
            //    btnPage.Location = new System.Drawing.Point(60 + (40 * count), 5);
            //    btnPage.Size = new System.Drawing.Size(40, 20);
            //    btnPage.Name = page.Value;
            //    btnPage.Text = page.Text;
            //    btnPage.Enabled = !page.Selected;
            //    btnPage.Click += new System.EventHandler(this.Page_Click);
            //    pnlPager.Controls.Add(btnPage);
            //    count++;
            //}
        }

        private void Page_Click(object sender, EventArgs e)
        {
            //Button btnPager = (sender as Button);
            //xcurrentPage = int.Parse(btnPager.Name);
            //this.BindGrid(xcurrentPage);
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
            appinfo pappinfo = new appinfo();
            pappinfo.refno = txtrefno.Text;
            pappinfo.negeri = cbnegeri.SelectedValue == null ? "" : cbnegeri.SelectedValue.ToString();
            pappinfo.bangsa = cbbangsa.SelectedValue == null ? "" : cbbangsa.SelectedValue.ToString();
            pappinfo.daerah = cbdaerah.SelectedValue == null ? "" : cbdaerah.SelectedValue.ToString();
            pappinfo.dun = cbdun.SelectedValue == null ? "" : cbdun.SelectedValue.ToString();
            pappinfo.parlimen = Convert.ToInt32(cbparlimen.SelectedValue == null ? 0 : cbparlimen.SelectedValue);

            int rowcount = 0;
            IList<appinfoDTO> lstEnt = AppInfoRepo.PagedListDTO(pageIndex, pagesize, sidx, sord, out rowcount, pappinfo);
            this.rowcount = rowcount;
            dgvMakPer.DataSource = lstEnt;
            this.PopulatePager(rowcount, pageIndex);
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
