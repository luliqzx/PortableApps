namespace PortableApps
{
    partial class MDIParent2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent2));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.roobClose = new System.Windows.Forms.RibbonOrbOptionButton();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.rbPanelPemohon = new System.Windows.Forms.RibbonPanel();
            this.rbDaftarMaklumatPermohonan = new System.Windows.Forms.RibbonButton();
            this.rbMaklumatPemohon = new System.Windows.Forms.RibbonButton();
            this.rbmakkebun = new System.Windows.Forms.RibbonButton();
            this.rbLawatanPengesahanKebun = new System.Windows.Forms.RibbonButton();
            this.ribbonTab4 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.rbUpdateServer = new System.Windows.Forms.RibbonButton();
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.rbInitData = new System.Windows.Forms.RibbonButton();
            this.rbSetting = new System.Windows.Forms.RibbonButton();
            this.ribbonTab3 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribbonButton1 = new System.Windows.Forms.RibbonButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssUserKeyIn = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.CaptionBarVisible = false;
            this.ribbon1.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.OptionItems.Add(this.roobClose);
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.PanelCaptionHeight = 5;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbon1.Size = new System.Drawing.Size(737, 115);
            this.ribbon1.TabIndex = 4;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.Tabs.Add(this.ribbonTab4);
            this.ribbon1.Tabs.Add(this.ribbonTab2);
            this.ribbon1.Tabs.Add(this.ribbonTab3);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 2, 20, 0);
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            // 
            // roobClose
            // 
            this.roobClose.Image = ((System.Drawing.Image)(resources.GetObject("roobClose.Image")));
            this.roobClose.LargeImage = ((System.Drawing.Image)(resources.GetObject("roobClose.LargeImage")));
            this.roobClose.Name = "roobClose";
            this.roobClose.SmallImage = ((System.Drawing.Image)(resources.GetObject("roobClose.SmallImage")));
            this.roobClose.Text = "Close";
            this.roobClose.Click += new System.EventHandler(this.roobClose_Click);
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add(this.rbPanelPemohon);
            this.ribbonTab1.Text = "Pemohon";
            // 
            // rbPanelPemohon
            // 
            this.rbPanelPemohon.Items.Add(this.rbDaftarMaklumatPermohonan);
            this.rbPanelPemohon.Items.Add(this.rbMaklumatPemohon);
            this.rbPanelPemohon.Items.Add(this.rbmakkebun);
            this.rbPanelPemohon.Items.Add(this.rbLawatanPengesahanKebun);
            this.rbPanelPemohon.Name = "rbPanelPemohon";
            this.rbPanelPemohon.Text = "";
            // 
            // rbDaftarMaklumatPermohonan
            // 
            this.rbDaftarMaklumatPermohonan.Image = global::PortableApps.Properties.Resources.rz25list_512_1_;
            this.rbDaftarMaklumatPermohonan.LargeImage = global::PortableApps.Properties.Resources.rz25list_512_1_;
            this.rbDaftarMaklumatPermohonan.MinimumSize = new System.Drawing.Size(80, 0);
            this.rbDaftarMaklumatPermohonan.Name = "rbDaftarMaklumatPermohonan";
            this.rbDaftarMaklumatPermohonan.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbDaftarMaklumatPermohonan.SmallImage")));
            this.rbDaftarMaklumatPermohonan.Text = "Senarai Pemohon";
            this.rbDaftarMaklumatPermohonan.ToolTip = "SENARAI PEMOHON";
            this.rbDaftarMaklumatPermohonan.ToolTipTitle = "SENARAI PEMOHON";
            this.rbDaftarMaklumatPermohonan.Click += new System.EventHandler(this.rbDaftarMaklumatPermohonan_Click);
            // 
            // rbMaklumatPemohon
            // 
            this.rbMaklumatPemohon.Image = global::PortableApps.Properties.Resources.rz25Policy_Icon2_1_;
            this.rbMaklumatPemohon.LargeImage = global::PortableApps.Properties.Resources.rz25Policy_Icon2_1_;
            this.rbMaklumatPemohon.Name = "rbMaklumatPemohon";
            this.rbMaklumatPemohon.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbMaklumatPemohon.SmallImage")));
            this.rbMaklumatPemohon.Text = "Maklumat Pemohon";
            this.rbMaklumatPemohon.ToolTip = "Maklumat Pemohon";
            this.rbMaklumatPemohon.ToolTipTitle = "Maklumat Pemohon";
            this.rbMaklumatPemohon.Click += new System.EventHandler(this.rbMaklumatPemohon_Click);
            // 
            // rbmakkebun
            // 
            this.rbmakkebun.Image = global::PortableApps.Properties.Resources.rz25farm_2_filled1600_1_;
            this.rbmakkebun.LargeImage = global::PortableApps.Properties.Resources.rz25farm_2_filled1600_1_;
            this.rbmakkebun.Name = "rbmakkebun";
            this.rbmakkebun.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbmakkebun.SmallImage")));
            this.rbmakkebun.Text = "Maklumat Kebun";
            this.rbmakkebun.Click += new System.EventHandler(this.rbmakkebun_Click);
            // 
            // rbLawatanPengesahanKebun
            // 
            this.rbLawatanPengesahanKebun.Image = global::PortableApps.Properties.Resources.rz25images;
            this.rbLawatanPengesahanKebun.LargeImage = global::PortableApps.Properties.Resources.rz25images;
            this.rbLawatanPengesahanKebun.MinimumSize = new System.Drawing.Size(120, 0);
            this.rbLawatanPengesahanKebun.Name = "rbLawatanPengesahanKebun";
            this.rbLawatanPengesahanKebun.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbLawatanPengesahanKebun.SmallImage")));
            this.rbLawatanPengesahanKebun.Text = "Lawatan Pengesahan Kebun";
            this.rbLawatanPengesahanKebun.Click += new System.EventHandler(this.rbLawatanPengesahanKebun_Click);
            // 
            // ribbonTab4
            // 
            this.ribbonTab4.Name = "ribbonTab4";
            this.ribbonTab4.Panels.Add(this.ribbonPanel3);
            this.ribbonTab4.Text = "Sinkronisasi Data";
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.Image = global::PortableApps.Properties.Resources.mpob_small;
            this.ribbonPanel3.Items.Add(this.rbUpdateServer);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Text = "";
            // 
            // rbUpdateServer
            // 
            this.rbUpdateServer.Image = global::PortableApps.Properties.Resources.rz25sync_512;
            this.rbUpdateServer.LargeImage = global::PortableApps.Properties.Resources.rz25sync_512;
            this.rbUpdateServer.Name = "rbUpdateServer";
            this.rbUpdateServer.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbUpdateServer.SmallImage")));
            this.rbUpdateServer.Text = "Update Server";
            this.rbUpdateServer.Click += new System.EventHandler(this.rbUpdateServer_Click);
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Panels.Add(this.ribbonPanel2);
            this.ribbonTab2.Text = "Setting";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.rbInitData);
            this.ribbonPanel2.Items.Add(this.rbSetting);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "";
            // 
            // rbInitData
            // 
            this.rbInitData.Image = global::PortableApps.Properties.Resources.b_edit;
            this.rbInitData.LargeImage = global::PortableApps.Properties.Resources.b_edit;
            this.rbInitData.Name = "rbInitData";
            this.rbInitData.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbInitData.SmallImage")));
            this.rbInitData.Text = "Reset To Default";
            this.rbInitData.Click += new System.EventHandler(this.rbInitData_Click);
            // 
            // rbSetting
            // 
            this.rbSetting.Image = global::PortableApps.Properties.Resources.rz25setting;
            this.rbSetting.LargeImage = global::PortableApps.Properties.Resources.rz25setting;
            this.rbSetting.Name = "rbSetting";
            this.rbSetting.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSetting.SmallImage")));
            this.rbSetting.Text = "Setting";
            this.rbSetting.Click += new System.EventHandler(this.rbSetting_Click);
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Panels.Add(this.ribbonPanel1);
            this.ribbonTab3.Text = "Window";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.Items.Add(this.ribbonButton1);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Text = "";
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.Image = global::PortableApps.Properties.Resources.b_drop;
            this.ribbonButton1.LargeImage = global::PortableApps.Properties.Resources.b_drop;
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.SmallImage = global::PortableApps.Properties.Resources.b_drop;
            this.ribbonButton1.Text = "Close";
            this.ribbonButton1.Click += new System.EventHandler(this.ribbonButton1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssUserKeyIn});
            this.statusStrip1.Location = new System.Drawing.Point(0, 466);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(737, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssUserKeyIn
            // 
            this.tssUserKeyIn.Name = "tssUserKeyIn";
            this.tssUserKeyIn.Size = new System.Drawing.Size(48, 17);
            this.tssUserKeyIn.Text = "Nama : ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MDIParent2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PortableApps.Properties.Resources.mpob_small;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(737, 488);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ribbon1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "MDIParent2";
            this.Text = "Sistem Pengurusan Skim Tanam Baru Sawit Pekebun Kecil (TBSPK)";
            this.Load += new System.EventHandler(this.MDIParent2_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonPanel rbPanelPemohon;
        private System.Windows.Forms.RibbonButton rbMaklumatPemohon;
        private System.Windows.Forms.RibbonTab ribbonTab2;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton rbmakkebun;
        private System.Windows.Forms.RibbonButton rbSetting;
        private System.Windows.Forms.RibbonButton rbDaftarMaklumatPermohonan;
        private System.Windows.Forms.RibbonOrbOptionButton roobClose;
        private System.Windows.Forms.RibbonTab ribbonTab3;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton ribbonButton1;
        private System.Windows.Forms.RibbonButton rbLawatanPengesahanKebun;
        private System.Windows.Forms.RibbonButton rbInitData;
        private System.Windows.Forms.RibbonTab ribbonTab4;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton rbUpdateServer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssUserKeyIn;
        private System.Windows.Forms.Timer timer1;
    }
}



