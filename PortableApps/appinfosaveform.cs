﻿using PortableApps.Common;
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
        public string refno_new;

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
            if (!string.IsNullOrEmpty(appinfo.negeri))
            {
                cbnegeri.Enabled = false;
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
            parlimen parlimen = ParlimenRepo.GetBy(appinfo.parlimen);
            cbparlimen.SelectedValue = parlimen == null ? 0 : parlimen.Id;
            txtposkod.Text = appinfo.poskod;
            txthometel.Text = appinfo.hometel;
            txtofficetel.Text = appinfo.officetel;
            txthptel.Text = appinfo.hptel;
            txtemail.Text = appinfo.email;
        }

        private string GenerateRefNo(string negeri)
        {
            string refno_new = "";

            variables variables = VariablesRepo.GetBy(negeri);
            refno_new = @"TBSPK/" + variables.Parent + "/";

            //int maxappinfo = AppInfoRepo.GetMaxAppInfoBy(refno_new);
            int maxappinfo = AppInfoRepo.GetMaxAppInfoBy();

            refno_new = refno_new + maxappinfo.ToString().PadLeft(5, '0');

            return refno_new;
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
            lstBangsa.Insert(0, new TBangsa { Bangsa = "" });
            cbbangsa.DataSource = lstBangsa;
            cbbangsa.DisplayMember = "Bangsa";
            cbbangsa.ValueMember = "Bangsa";
            cbbangsa.SelectedIndex = 0;
        }

        private void LoadParlimen(string negeri = "")
        {
            IList<parlimen> lstEnt = ParlimenRepo.GetParlimentBy(negeri);
            lstEnt.Insert(0, new parlimen { Kawasan = "", Id = 0 });
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
            lstEnt.Insert(0, new variables { Value = "", Code = "" });
            //cbdaerah.Items.Clear();
            cbnegeri.DataSource = lstEnt;
            cbnegeri.DisplayMember = "value";
            cbnegeri.ValueMember = "code";
            cbnegeri.SelectedIndex = 0;
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
            try
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
                //lstCtrlEmptyCheck.Add(cbbangsa);
                lstCtrlEmptyCheck.Add(txtaddr1);
                lstCtrlEmptyCheck.Add(txtaddr2);
                //lstCtrlEmptyCheck.Add(txtaddr3);
                lstCtrlEmptyCheck.Add(txtbandar);
                lstCtrlEmptyCheck.Add(txtposkod);
                lstCtrlEmptyCheck.Add(cbnegeri);
                //lstCtrlEmptyCheck.Add(cbdaerah);
                //lstCtrlEmptyCheck.Add(cbdun);
                //lstCtrlEmptyCheck.Add(cbparlimen);
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
                    refno_new = GenerateRefNo(cbnegeri.SelectedValue.ToString());
                }

                appinfo.id = appinfo_id;
                appinfo.appdate = txtappdate.Text;
                appinfo.nama = txtnama.Text;
                appinfo.type_id = 0;//txttype_id.Text;
                appinfo.icno = txticno.Text;
                appinfo.nolesen = txtnolesen.Text;
                appinfo.bangsa = cbbangsa.SelectedValue == null ? "" : cbbangsa.SelectedValue.ToString();
                appinfo.addr1 = txtaddr1.Text;
                appinfo.addr2 = txtaddr2.Text;
                appinfo.addr3 = txtaddr3.Text;
                appinfo.bandar = txtbandar.Text;
                appinfo.daerah = cbdaerah.SelectedValue == null ? "" : cbdaerah.SelectedValue.ToString();
                appinfo.dun = cbdun.SelectedValue == null ? "" : cbdun.SelectedValue.ToString();
                int? sparlimen = (int?)cbparlimen.SelectedValue;
                parlimen parlimen = ParlimenRepo.GetBy(sparlimen);
                appinfo.parlimen = parlimen == null ? null : (int?)parlimen.Id;
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
                appinfo.refno_new = refno_new;// GenerateRefNo(appinfo.negeri);

                if (IsNew)
                {
                    try
                    {
                        appinfo.created = DateTime.Now;
                        appinfo.createdby = VariableSettingRepo.GetBy("UserKeyIn").Value;
                        AppInfoRepo.Create(appinfo);
                        MessageBox.Show("Data berhasil disimpan [" + appinfo.refno_new + "]");
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
                        MessageBox.Show("Data berhasil disimpan [" + appinfo.refno_new + "]");
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

            groupBox1.Text = groupBox1.Text + " " + refno_new;

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
                LoadParlimen(cbx.SelectedValue.ToString());
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
