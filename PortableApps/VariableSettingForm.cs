using PortableApps.Common;
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
    public partial class VariableSettingForm : Form
    {
        #region Fields / Properties

        IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();
        private int PageSize { get; set; }
        private string sidx { get; set; }
        private string sord { get; set; }
        private int xcurrentPage { get; set; }
        int totalRecords;
        #endregion

        #region Constructor
        public VariableSettingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Pager & Binding Grid

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
            IList<VariableSetting> lstEnt = VariableSettingRepo.PagedList(pageIndex, PageSize, "Key", "Asc", out totalRecords, null);
            dgvCS.DataSource = lstEnt;
            int recordCount = Convert.ToInt32(totalRecords);
            this.PopulatePager(recordCount, pageIndex);
        }

        #endregion

        #region Functions
        private void ClearTextBoxes()
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
                    else if (control is CheckBox)
                    {
                        (control as CheckBox).Checked = false;
                    }
                    else if (control is RichTextBox)
                    {
                        (control as RichTextBox).Clear();
                    }
                    else
                        func(control.Controls);
            };

            func(Controls);
        }
        #endregion

        #region Events

        private void SettingForm_Load(object sender, EventArgs e)
        {
            //LoadGrid();
            VariableSetting varPageSize = VariableSettingRepo.GetBy("PageSize");
            if (varPageSize == null)
            {
                PageSize = 2;
            }
            else
            {
                PageSize = Convert.ToInt32(varPageSize.Value);
            }
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();
            xcurrentPage = 1;


            sidx = "KEY";
            sidx = "ASC";
            BindGrid(xcurrentPage);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool IsNew = false;
            VariableSetting VariableSetting = VariableSettingRepo.GetBy(txtKey.Text);
            if (VariableSetting == null)
            {
                IsNew = true;
                VariableSetting = new VariableSetting();
            }
            VariableSetting.Key = txtKey.Text;
            VariableSetting.Value = cbEncrypt.Checked ? WFUtils.Encrypt(txtValue.Text) : txtValue.Text;
            VariableSetting.Description = txtDescription.Text;
            VariableSetting.CanModify = cbEncrypt.Checked ? 1 : 2;
            int i = 0;
            if (IsNew)
            {
                i = VariableSettingRepo.Create(VariableSetting);
            }
            else
            {
                i = VariableSettingRepo.Edit(VariableSetting);
            }
            MessageBox.Show("Data berhasil disimpan [" + txtKey.Text + "]");
            BindGrid(xcurrentPage);
        }


        private void dgvCS_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void dgvCS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            DataGridViewRow dgvr = dgv.Rows[e.RowIndex];
            txtKey.Text = dgvr.Cells["Key"].Value.ToString();
            txtValue.Text = dgvr.Cells["Value"].Value.ToString();
            txtDescription.Text = dgvr.Cells["Description"].Value.ToString();
            bool CanModify = Convert.ToUInt32(dgvr.Cells["CanModify"].Value) == 1 ? true : false;
            cbxCanModify.Checked = CanModify;
            cbEncrypt.Checked = Convert.ToBoolean(dgvr.Cells["IsEncrypt"].Value);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            VariableSetting VariableSetting = VariableSettingRepo.GetBy(txtKey.Text);
            if (VariableSetting == null)
            {
                MessageBox.Show("Data tidak berhasil dihapus. Data tidak ada");
            }
            if (VariableSetting.CanModify == 0)
            {
                MessageBox.Show("Data tidak berhasil dihapus. Data mandatory.");
            }
            else
            {
                int i = VariableSettingRepo.Delete(txtKey.Text);
                if (i > 0)
                {
                    MessageBox.Show("Data berhasil dihapus [" + txtKey.Text + "]");
                }
                else
                {
                    MessageBox.Show("Data tidak berhasil dihapus [" + txtKey.Text + "]");
                }
            }
            BindGrid(xcurrentPage);
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }


        private void dgvCS_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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

        #endregion
    }
}
