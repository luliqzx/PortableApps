﻿using PortableApps.Model;
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
        IBangsaRepo BangsaRepo = new BangsaRepo();
        IParlimenRepo ParlimenRepo = new ParlimenRepo();
        IDaerahRepo DaerahRepo = new DaerahRepo();
        IDunRepo DunRepo = new DunRepo();
        IVariablesRepo VariablesRepo = new VariablesRepo();
        IAppInfoRepo AppInfoRepo = new AppInfoRepo();

        public appinfosaveform()
        {
            InitializeComponent();
        }

        private void appinfosaveform_Load(object sender, EventArgs e)
        {
            //OnLoad(e);
            ControlBox = false;
            WindowState = FormWindowState.Maximized;
            BringToFront();
            LoadNegeri();
            LoadTBangsa();
            LoadParlimen();
            txtappdate.CustomFormat = "dd-MM-yyyy";
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

        private void cbparlimen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.SelectedValue != null)
            {
                LoadDaerah(cbx.SelectedValue.ToString());
                LoadDun(cbx.SelectedValue.ToString());
            }
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
            appinfo appinfo = new appinfo();
            appinfo.id = Convert.ToInt32(DateTime.Now.Subtract(new DateTime()).TotalMinutes);
            appinfo.appdate = txtappdate.Text;
            appinfo.nama = txtnama.Text;
            //appinfo.type_id = txttype_id.Text;
            appinfo.icno = txticno.Text;
            appinfo.nolesen = txtnolesen.Text;
            appinfo.bangsa = cbbangsa.SelectedValue.ToString();
            appinfo.addr1 = txtaddr1.Text;
            appinfo.addr2 = txtaddr2.Text;
            appinfo.addr3 = txtaddr3.Text;
            appinfo.bandar = txtbandar.Text;
            appinfo.daerah = cbdaerah.SelectedValue.ToString();
            appinfo.dun = cbdun.SelectedValue.ToString();
            appinfo.parlimen = cbparlimen.SelectedValue.ToString();
            appinfo.poskod = txtposkod.Text;
            appinfo.negeri = cbnegeri.SelectedValue.ToString();
            appinfo.hometel = txthometel.Text;
            appinfo.officetel = txtofficetel.Text;
            appinfo.hptel = txthptel.Text;
            //appinfo.faks = txtfaks.Text;
            appinfo.email = txtemail.Text;
            //appinfo.kelompok = txtkelompok.Text;

            int i = AppInfoRepo.Create(appinfo);
            if (i > 0)
            {
                MessageBox.Show("Data berhasil disimpan [" + appinfo.id + "]");
            }
        }

    }
}
