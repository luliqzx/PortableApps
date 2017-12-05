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
    public partial class SamplePagingForm : Form
    {
        #region Fields / Properties
        public int page { get; set; }
        public int rowcount { get; set; }
        public int pagesize { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public int pagecount { get; set; }
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();
        #endregion
        
        #region Common Class

        public class KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        #endregion

        #region Constructor
        public SamplePagingForm()
        {
            InitializeComponent();
        }
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
        
        #endregion

        #region Pager & Binding Grid
        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pagesize));
            pagecount = (int)Math.Ceiling(dblPageCount);
            label2.Text = "of " + pagecount;
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pagecount > pagerSpan ? pagerSpan : pagecount;
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

            if (endIndex > pagecount)
            {
                endIndex = pagecount;
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
            if (currentPage < pagecount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pagecount)
            {
                pages.Add(new Page { Text = "Last", Value = pagecount.ToString() });
            }

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
            //appinfo pappinfo = new appinfo();
            //pappinfo.refno = txtrefno.Text;
            //pappinfo.negeri = cbnegeri.SelectedValue == null ? "" : cbnegeri.SelectedValue.ToString();
            //pappinfo.bangsa = cbbangsa.SelectedValue == null ? "" : cbbangsa.SelectedValue.ToString();
            //pappinfo.daerah = cbdaerah.SelectedValue == null ? "" : cbdaerah.SelectedValue.ToString();
            //pappinfo.dun = cbdun.SelectedValue == null ? "" : cbdun.SelectedValue.ToString();
            //pappinfo.parlimen = Convert.ToInt32(cbparlimen.SelectedValue == null ? 0 : cbparlimen.SelectedValue);
            int rowcount = 0;
            IList<appinfo> lstAppInfo = AppInfoRepo.GetListMySQL(page, pagesize, sidx, sord, new appinfo(), out rowcount);
            this.rowcount = rowcount;
            dataGridView1.DataSource = lstAppInfo;
            PopulatePager(rowcount, page);
            //IList<appinfoDTO> lstEnt = AppInfoRepo.PagedListDTO(pageIndex, PageSize, sidx, sord, out totalRecords, pappinfo);
            //dgvMakPer.DataSource = lstEnt;
            //int recordCount = Convert.ToInt32(totalRecords);
            //this.PopulatePager(recordCount, pageIndex);

        }

        #endregion

        #region Events
        private void dgvGeneral_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
        
        private void SamplePagingForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            BindingPageSize();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            textBox1.Text = "1";

            page = 1;
            pagesize = Convert.ToInt32(comboBox1.SelectedValue);
            sidx = "id";
            sord = "asc";
            BindGrid(page);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            page = 1;
            textBox1.Text = page.ToString();
            BindGrid(page);
            label3.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            page = page - 1 <= 0 ? 1 : page - 1 > pagecount ? pagecount : page - 1;
            textBox1.Text = page.ToString();
            BindGrid(page);
            label3.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            int ips = Convert.ToInt32(cbx.SelectedValue);
            pagesize = ips;
            BindGrid(page);
            label3.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1 > rowcount ? rowcount + 1 : ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            page = pagecount;
            textBox1.Text = page.ToString();
            BindGrid(page);
            label3.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page = page + 1 > pagecount ? pagecount : page + 1;
            textBox1.Text = page.ToString();
            BindGrid(page);
            label3.Text = string.Format("View {0} - {1} of {2}", ((page - 1) * pagesize) + 1, page * pagesize > rowcount ? rowcount : page * pagesize, rowcount);
        }

        #endregion

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Enter )
            {
                
            int iText = Convert.ToInt32(textBox1.Text);
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
            textBox1.Text = page.ToString();
            BindGrid(page);
            }
        }

        

        
        



    }
}
