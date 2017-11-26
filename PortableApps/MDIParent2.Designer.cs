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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent2));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.rbPanelPemohon = new System.Windows.Forms.RibbonPanel();
            this.rbMaklumatPemohon = new System.Windows.Forms.RibbonButton();
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.rbmakkebun = new System.Windows.Forms.RibbonButton();
            this.rbSetting = new System.Windows.Forms.RibbonButton();
            this.rbDaftarMaklumatPermohonan = new System.Windows.Forms.RibbonButton();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbon1.Size = new System.Drawing.Size(737, 140);
            this.ribbon1.TabIndex = 4;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.Tabs.Add(this.ribbonTab2);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbon1.Text = "ribbon1";
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
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
            this.rbPanelPemohon.Name = "rbPanelPemohon";
            this.rbPanelPemohon.Text = "";
            // 
            // rbMaklumatPemohon
            // 
            this.rbMaklumatPemohon.Image = ((System.Drawing.Image)(resources.GetObject("rbMaklumatPemohon.Image")));
            this.rbMaklumatPemohon.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbMaklumatPemohon.LargeImage")));
            this.rbMaklumatPemohon.Name = "rbMaklumatPemohon";
            this.rbMaklumatPemohon.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbMaklumatPemohon.SmallImage")));
            this.rbMaklumatPemohon.Text = "Maklumat Pemohon";
            this.rbMaklumatPemohon.ToolTip = "Maklumat Pemohon";
            this.rbMaklumatPemohon.ToolTipTitle = "Maklumat Pemohon";
            this.rbMaklumatPemohon.Click += new System.EventHandler(this.rbMaklumatPemohon_Click);
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Panels.Add(this.ribbonPanel2);
            this.ribbonTab2.Text = "Setting";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.Items.Add(this.rbSetting);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Text = "";
            // 
            // rbmakkebun
            // 
            this.rbmakkebun.Image = ((System.Drawing.Image)(resources.GetObject("rbmakkebun.Image")));
            this.rbmakkebun.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbmakkebun.LargeImage")));
            this.rbmakkebun.Name = "rbmakkebun";
            this.rbmakkebun.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbmakkebun.SmallImage")));
            this.rbmakkebun.Text = "Maklumat Kebun";
            this.rbmakkebun.Click += new System.EventHandler(this.rbmakkebun_Click);
            // 
            // rbSetting
            // 
            this.rbSetting.Image = ((System.Drawing.Image)(resources.GetObject("rbSetting.Image")));
            this.rbSetting.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbSetting.LargeImage")));
            this.rbSetting.Name = "rbSetting";
            this.rbSetting.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSetting.SmallImage")));
            this.rbSetting.Text = "Setting";
            this.rbSetting.Click += new System.EventHandler(this.rbSetting_Click);
            // 
            // rbDaftarMaklumatPermohonan
            // 
            this.rbDaftarMaklumatPermohonan.Image = ((System.Drawing.Image)(resources.GetObject("rbDaftarMaklumatPermohonan.Image")));
            this.rbDaftarMaklumatPermohonan.LargeImage = ((System.Drawing.Image)(resources.GetObject("rbDaftarMaklumatPermohonan.LargeImage")));
            this.rbDaftarMaklumatPermohonan.Name = "rbDaftarMaklumatPermohonan";
            this.rbDaftarMaklumatPermohonan.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbDaftarMaklumatPermohonan.SmallImage")));
            this.rbDaftarMaklumatPermohonan.Text = "Daftar Maklumat";
            this.rbDaftarMaklumatPermohonan.ToolTip = "Daftar Maklumat Permohonan";
            this.rbDaftarMaklumatPermohonan.ToolTipTitle = "Daftar Maklumat Permohonan";
            this.rbDaftarMaklumatPermohonan.Click += new System.EventHandler(this.rbDaftarMaklumatPermohonan_Click);
            // 
            // MDIParent2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 488);
            this.Controls.Add(this.ribbon1);
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "MDIParent2";
            this.Text = "MDIParent2";
            this.ResumeLayout(false);

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

    }
}



