namespace PortableApps
{
    partial class appinfolistform
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.cbparlimen = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbdun = new System.Windows.Forms.ComboBox();
            this.cbbangsa = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbdaerah = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbnegeri = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtrefno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvMakPer = new System.Windows.Forms.DataGridView();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMakPer)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.gbFilter);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(840, 120);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(928, 120);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.panel1);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Location = new System.Drawing.Point(3, 3);
            this.gbFilter.MinimumSize = new System.Drawing.Size(840, 102);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(900, 102);
            this.gbFilter.TabIndex = 0;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cbparlimen);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cbdun);
            this.panel1.Controls.Add(this.cbbangsa);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbdaerah);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbnegeri);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtrefno);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.MinimumSize = new System.Drawing.Size(826, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 72);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(526, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cari";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(559, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Bangsa";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(452, 13);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(101, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(346, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tarikh Permohonan";
            // 
            // cbparlimen
            // 
            this.cbparlimen.FormattingEnabled = true;
            this.cbparlimen.Location = new System.Drawing.Point(399, 41);
            this.cbparlimen.Name = "cbparlimen";
            this.cbparlimen.Size = new System.Drawing.Size(121, 21);
            this.cbparlimen.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(346, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Parlimen";
            // 
            // cbdun
            // 
            this.cbdun.FormattingEnabled = true;
            this.cbdun.Location = new System.Drawing.Point(212, 41);
            this.cbdun.Name = "cbdun";
            this.cbdun.Size = new System.Drawing.Size(128, 21);
            this.cbdun.TabIndex = 2;
            // 
            // cbbangsa
            // 
            this.cbbangsa.FormattingEnabled = true;
            this.cbbangsa.Location = new System.Drawing.Point(608, 13);
            this.cbbangsa.Name = "cbbangsa";
            this.cbbangsa.Size = new System.Drawing.Size(121, 21);
            this.cbbangsa.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Dun";
            // 
            // cbdaerah
            // 
            this.cbdaerah.FormattingEnabled = true;
            this.cbdaerah.Location = new System.Drawing.Point(52, 41);
            this.cbdaerah.Name = "cbdaerah";
            this.cbdaerah.Size = new System.Drawing.Size(121, 21);
            this.cbdaerah.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Daerah";
            // 
            // cbnegeri
            // 
            this.cbnegeri.FormattingEnabled = true;
            this.cbnegeri.Location = new System.Drawing.Point(219, 13);
            this.cbnegeri.Name = "cbnegeri";
            this.cbnegeri.Size = new System.Drawing.Size(121, 21);
            this.cbnegeri.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Negeri";
            // 
            // txtrefno
            // 
            this.txtrefno.Location = new System.Drawing.Point(69, 14);
            this.txtrefno.Name = "txtrefno";
            this.txtrefno.Size = new System.Drawing.Size(104, 20);
            this.txtrefno.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "No Rujukan";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvMakPer, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlPager, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 120);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.84489F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.15511F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(928, 456);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // dgvMakPer
            // 
            this.dgvMakPer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvMakPer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMakPer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMakPer.Location = new System.Drawing.Point(3, 53);
            this.dgvMakPer.MinimumSize = new System.Drawing.Size(845, 259);
            this.dgvMakPer.Name = "dgvMakPer";
            this.dgvMakPer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMakPer.Size = new System.Drawing.Size(922, 354);
            this.dgvMakPer.TabIndex = 0;
            this.dgvMakPer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMakPer_CellDoubleClick);
            this.dgvMakPer.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMakPer_ColumnHeaderMouseClick);
            this.dgvMakPer.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMakPer_DataBindingComplete);
            this.dgvMakPer.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvMakPer_RowPostPaint);
            // 
            // pnlPager
            // 
            this.pnlPager.AutoScroll = true;
            this.pnlPager.AutoSize = true;
            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPager.Location = new System.Drawing.Point(3, 413);
            this.pnlPager.MinimumSize = new System.Drawing.Size(845, 30);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(922, 40);
            this.pnlPager.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.MinimumSize = new System.Drawing.Size(845, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(922, 44);
            this.panel2.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Image = global::PortableApps.Properties.Resources.b_add;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(3, 0);
            this.button2.MinimumSize = new System.Drawing.Size(75, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(175, 44);
            this.button2.TabIndex = 0;
            this.button2.Text = "Buat Maklumat Permohonan";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // appinfolistform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(928, 576);
            this.ClientSize = new System.Drawing.Size(912, 537);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "appinfolistform";
            this.Text = "Daftar Maklumat Permohonan";
            this.Load += new System.EventHandler(this.appinfolistform_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbFilter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMakPer)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtrefno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbparlimen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbdun;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbdaerah;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbnegeri;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cbbangsa;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvMakPer;
        private System.Windows.Forms.Panel pnlPager;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;

    }
}