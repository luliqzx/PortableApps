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
        public int page { get; set; }
        public int rowcount { get; set; }
        public int pagesize { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();

        public SamplePagingForm()
        {
            InitializeComponent();
        }

        #region Pager & Binding Grid
        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(pagesize));
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
            dataGridView1.DataSource = lstAppInfo;
            PopulatePager(rowcount, page);
            //IList<appinfoDTO> lstEnt = AppInfoRepo.PagedListDTO(pageIndex, PageSize, sidx, sord, out totalRecords, pappinfo);
            //dgvMakPer.DataSource = lstEnt;
            //int recordCount = Convert.ToInt32(totalRecords);
            //this.PopulatePager(recordCount, pageIndex);

        }

        #endregion

        private void SamplePagingForm_Load(object sender, EventArgs e)
        {
            page = 1;
            pagesize = Convert.ToInt32(comboBox1.Items[0]);
            sidx = "id";
            sord = "asc";
            BindGrid(page);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            page = 1;
            BindGrid(page);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            page = page - 1 <= 0 ? 1 : page - 1;
            BindGrid(page);
        }
    }
}
