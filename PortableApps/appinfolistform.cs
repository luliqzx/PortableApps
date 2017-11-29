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
        IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();
        private int PageSize { get; set; }
        private string sidx { get; set; }
        private string sord { get; set; }
        private int xcurrentPage { get; set; }
        int totalRecords;


        public appinfolistform()
        {
            InitializeComponent();

        }

        private void appinfolistform_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd-MM-yyyy";
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
            sidx = "ID";
            sord = "ASC";
            BindGrid(xcurrentPage);
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
            IList<appinfoDTO> lstEnt = AppInfoRepo.PagedListDTO(pageIndex, PageSize, sidx, sord, out totalRecords, null);
            dgvMakPer.DataSource = lstEnt;
            int recordCount = Convert.ToInt32(totalRecords);
            this.PopulatePager(recordCount, pageIndex);

            dgvMakPer.Columns[0].HeaderText = "Nama Pemohon";
            dgvMakPer.Columns[1].HeaderText = "No Kad. Pengenalan";
            dgvMakPer.Columns[2].HeaderText = "Negeri";
            dgvMakPer.Columns[3].HeaderText = "No. Lesen";
            dgvMakPer.Columns[4].HeaderText = "No. Rujukan";
            dgvMakPer.Columns[5].HeaderText = "Tarikh Permohonan";
            dgvMakPer.Columns[6].HeaderText = "Dibuat Oleh";
            dgvMakPer.Columns[7].HeaderText = "Dibuat Tanggal";
            dgvMakPer.Columns[7].DefaultCellStyle.Format = "dd-MM-yyyy HH:mm:ss";
            dgvMakPer.Columns[8].HeaderText = "Id";
            dgvMakPer.Columns[8].Visible = false;
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

            string snumber = ((PageSize * (xcurrentPage - 1)) + Convert.ToInt32(rowIdx)).ToString();

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(snumber, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

        }

        private void dgvMakPer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
            BindGrid(xcurrentPage);
        }
    }
}
