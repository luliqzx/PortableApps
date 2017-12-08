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
    public partial class appinfosaveform : Form
    {
        #region Fields / Properties
        IBangsaRepo BangsaRepo = new BangsaRepo();
        IParlimenRepo ParlimenRepo = new ParlimenRepo();
        IDaerahRepo DaerahRepo = new DaerahRepo();
        IDunRepo DunRepo = new DunRepo();
        IVariablesRepo VariablesRepo = new VariablesRepo();
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();
        IVariableSettingRepo VariableSettingRepo = new VariableSettingRepo();

        public int appinfo_id;
        public string refno;

        private IList<variables> lstWilayah = new List<variables>();
        #endregion

        #region Constructor
        public appinfosaveform()
        {
            InitializeComponent();
        }
        #endregion

        #region Functions

        private void BindForm(int appinfo_id)
        {
            appinfo appinfo = AppInfoRepo.GetBy(appinfo_id);
            if (appinfo == null)
            {
                appinfo = new appinfo();
            }
            cbnegeri.SelectedValue = appinfo.negeri;
            txtappdate.Text = appinfo.appdate;
            txtnama.Text = appinfo.nama;
            txticno.Text = appinfo.icno;
            txtnolesen.Text = appinfo.nolesen;
            cbbangsa.SelectedValue = appinfo.bangsa;
            txtaddr1.Text = appinfo.addr1;
            txtaddr2.Text = appinfo.addr2;
            txtaddr3.Text = appinfo.addr3;
            txtbandar.Text = appinfo.bandar;
            cbdaerah.SelectedValue = appinfo.daerah;
            cbdun.SelectedValue = appinfo.dun;
            cbparlimen.SelectedValue = ParlimenRepo.GetBy(appinfo.parlimen).Negeri;
            txtposkod.Text = appinfo.poskod;
            txthometel.Text = appinfo.hometel;
            txtofficetel.Text = appinfo.officetel;
            txthptel.Text = appinfo.hptel;
            txtemail.Text = appinfo.email;
        }

        private string GenerateRefNo(string negeri)
        {
            string refno = "";

            variables variables = VariablesRepo.GetBy(negeri);
            refno = @"TSSPK/" + variables.Parent + "/";

            int maxappinfo = AppInfoRepo.GetMaxAppInfoBy(refno);

            refno = refno + maxappinfo.ToString().PadLeft(5, '0');

            return refno;
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else if (control is ComboBox)
                    {
                        (control as ComboBox).SelectedIndex = -1;
                    }
                    else
                        func(control.Controls);
                }
            };

            func(Controls);

            appinfo_id = 0;
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
            IList<variables> lstEnt = VariablesRepo.GetVariableByType("negeri");
            //cbdaerah.Items.Clear();
            cbnegeri.DataSource = lstEnt;
            cbnegeri.DisplayMember = "value";
            cbnegeri.ValueMember = "code";
            cbnegeri.SelectedIndex = -1;
        }

        #endregion

        #region Events

        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            LoadDaerah("");
            LoadDun("");
            lblWilayah.Text = "";
        }

        private void txtappdate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            dtp.CustomFormat = "dd-MM-yyyy";
        }

        private void AdjustWidthComboBox_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (var s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(((ComboBox)sender).GetItemText(s), font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void GeneralTooltip_MouseUp(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (object.ReferenceEquals(sender.GetType(), typeof(ComboBox)))
            {
                ComboBox cbo = sender as ComboBox;
                this.ttpGeneral.SetToolTip(cbo, cbo.Text);
            }
            else if (object.ReferenceEquals(sender.GetType(), typeof(TextBox)))
            {
                TextBox txt = sender as TextBox;
                this.ttpGeneral.SetToolTip(txt, txt.Text);
            }
            else if (object.ReferenceEquals(sender.GetType(), typeof(Button)))
            {
                Button btn = sender as Button;
                this.ttpGeneral.SetToolTip(btn, btn.Text);
            }
            else if (object.ReferenceEquals(sender.GetType(), typeof(DateTimePicker)))
            {
                DateTimePicker dp = sender as DateTimePicker;
                this.ttpGeneral.SetToolTip(dp, dp.Text);
            }
        }

        private void GeneralTooltip_MouseHover(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(sender.GetType(), typeof(ComboBox)))
            {
                ComboBox cbo = sender as ComboBox;
                this.ttpGeneral.SetToolTip(cbo, cbo.Text);
            }
            else if (object.ReferenceEquals(sender.GetType(), typeof(TextBox)))
            {
                TextBox txt = sender as TextBox;
                this.ttpGeneral.SetToolTip(txt, txt.Text);
            }
            else if (object.ReferenceEquals(sender.GetType(), typeof(Button)))
            {
                Button btn = sender as Button;
                this.ttpGeneral.SetToolTip(btn, btn.Text);
            }
            else if (object.ReferenceEquals(sender.GetType(), typeof(DateTimePicker)))
            {
                DateTimePicker dp = sender as DateTimePicker;
                this.ttpGeneral.SetToolTip(dp, dp.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (txtnama.CheckEmpty() && txtaddr1.CheckEmpty())
            //{
            //    return;
            //}
            IList<Control> lstCtrlEmptyCheck = new List<Control>();
            lstCtrlEmptyCheck.Add(txtappdate);
            lstCtrlEmptyCheck.Add(txtnama);
            lstCtrlEmptyCheck.Add(txticno);
            lstCtrlEmptyCheck.Add(txtnolesen);
            lstCtrlEmptyCheck.Add(cbbangsa);
            lstCtrlEmptyCheck.Add(txtaddr1);
            lstCtrlEmptyCheck.Add(txtaddr2);
            //lstCtrlEmptyCheck.Add(txtaddr3);
            lstCtrlEmptyCheck.Add(txtbandar);
            lstCtrlEmptyCheck.Add(txtposkod);
            lstCtrlEmptyCheck.Add(cbnegeri);
            lstCtrlEmptyCheck.Add(cbdaerah);
            lstCtrlEmptyCheck.Add(cbdun);
            lstCtrlEmptyCheck.Add(cbparlimen);
            lstCtrlEmptyCheck.Add(txthometel);
            lstCtrlEmptyCheck.Add(txtofficetel);
            lstCtrlEmptyCheck.Add(txthptel);
            lstCtrlEmptyCheck.Add(txtemail);

            if (WFUtils.CheckControllCollectionEmpty(lstCtrlEmptyCheck))
            {
                return;
            }

            bool IsNew = false;
            appinfo appinfo = AppInfoRepo.GetBy(appinfo_id);
            if (appinfo == null)
            {
                appinfo = new appinfo();
                appinfo_id = Convert.ToInt32(WFUtils.DateTimeToUnixTimestamp(DateTime.Now));
                IsNew = true;
                refno = GenerateRefNo(cbnegeri.SelectedValue.ToString());
            }

            appinfo.id = appinfo_id;
            appinfo.appdate = txtappdate.Text;
            appinfo.nama = txtnama.Text;
            appinfo.type_id = 0;//txttype_id.Text;
            appinfo.icno = txticno.Text;
            appinfo.nolesen = txtnolesen.Text;
            appinfo.bangsa = cbbangsa.SelectedValue.ToString();
            appinfo.addr1 = txtaddr1.Text;
            appinfo.addr2 = txtaddr2.Text;
            appinfo.addr3 = txtaddr3.Text;
            appinfo.bandar = txtbandar.Text;
            appinfo.daerah = cbdaerah.SelectedValue.ToString();
            appinfo.dun = cbdun.SelectedValue.ToString();
            appinfo.parlimen = ParlimenRepo.GetParlimenIDBy(cbparlimen.SelectedValue.ToString());
            appinfo.poskod = txtposkod.Text;
            appinfo.negeri = cbnegeri.SelectedValue.ToString();
            appinfo.hometel = txthometel.Text;
            appinfo.officetel = txtofficetel.Text;
            appinfo.hptel = txthptel.Text;
            appinfo.sts_bck = 0;
            appinfo.status = 0;
            appinfo.sop = 0;
            appinfo.date_approved = DateTime.MinValue;
            appinfo.approved_by = "";
            //appinfo.faks = txtfaks.Text;
            appinfo.email = txtemail.Text;
            //appinfo.kelompok = txtkelompok.Text;
            appinfo.refno = refno;// GenerateRefNo(appinfo.negeri);
            if (IsNew)
            {
                try
                {
                    appinfo.created = DateTime.Now;
                    appinfo.createdby = VariableSettingRepo.GetBy("UserKeyIn").Value;
                    AppInfoRepo.Create(appinfo);
                    MessageBox.Show("Data berhasil disimpan [" + appinfo.refno + "]");
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
                    AppInfoRepo.Edit(appinfo);
                    MessageBox.Show("Data berhasil disimpan [" + appinfo.refno + "]");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.GetFullMessage());
                }
            }
            button2.PerformClick();
        }

        private void appinfosaveform_Load(object sender, EventArgs e)
        {
            //OnLoad(e);
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();


            string Wilayah = "wilayah";
            lstWilayah = VariablesRepo.GetVariableByType(Wilayah);


            LoadNegeri();
            LoadTBangsa();
            LoadParlimen();

            groupBox1.Text = groupBox1.Text + " " + refno;

            txtappdate.CustomFormat = "dd-MM-yyyy";

            VariableSetting vs = VariableSettingRepo.GetBy("UserKeyIn");

            txtcreatedby.Text = "System";
            if (vs != null)
            {
                txtcreatedby.Text = vs.Value;
            }

            //foreach (Control ctrl in Controls)
            //{
            //    ctrl.HookFocusChangeBackColor(Color.Khaki);
            //}

            if (appinfo_id > 0)
            {
                BindForm(appinfo_id);
            }

            FocusChangeBackColor();
        }

        private void cbnegeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.SelectedValue != null)
            {
                LoadDaerah(cbx.SelectedValue.ToString());
                LoadDun(cbx.SelectedValue.ToString());
                variables variables = VariablesRepo.GetVariableByType("NEGERI").FirstOrDefault(x => x.Code == cbx.SelectedValue.ToString());
                if (variables != null)
                {
                    lblWilayah.Text = GetAliasesParent(variables.Parent);
                }
            }
        }

        #endregion
    }
}
