namespace PortableApps
{
    partial class InitializeForm
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
            this.btnCreateAppInfo = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCreateMakkebun = new System.Windows.Forms.Button();
            this.btnCreateDun = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateAppInfo
            // 
            this.btnCreateAppInfo.Location = new System.Drawing.Point(12, 23);
            this.btnCreateAppInfo.Name = "btnCreateAppInfo";
            this.btnCreateAppInfo.Size = new System.Drawing.Size(260, 23);
            this.btnCreateAppInfo.TabIndex = 0;
            this.btnCreateAppInfo.Text = "Create AppInfo";
            this.btnCreateAppInfo.UseVisualStyleBackColor = true;
            this.btnCreateAppInfo.Click += new System.EventHandler(this.btnCreateAppInfo_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 110);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(260, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Create Parlimen";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnCreateMakkebun
            // 
            this.btnCreateMakkebun.Location = new System.Drawing.Point(12, 81);
            this.btnCreateMakkebun.Name = "btnCreateMakkebun";
            this.btnCreateMakkebun.Size = new System.Drawing.Size(260, 23);
            this.btnCreateMakkebun.TabIndex = 2;
            this.btnCreateMakkebun.Text = "Create MakKebun";
            this.btnCreateMakkebun.UseVisualStyleBackColor = true;
            // 
            // btnCreateDun
            // 
            this.btnCreateDun.Location = new System.Drawing.Point(12, 52);
            this.btnCreateDun.Name = "btnCreateDun";
            this.btnCreateDun.Size = new System.Drawing.Size(260, 23);
            this.btnCreateDun.TabIndex = 3;
            this.btnCreateDun.Text = "Create Dun";
            this.btnCreateDun.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 139);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(260, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Create Variables";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // InitializeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 183);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnCreateDun);
            this.Controls.Add(this.btnCreateMakkebun);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCreateAppInfo);
            this.Name = "InitializeForm";
            this.Text = "InitializeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateAppInfo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCreateMakkebun;
        private System.Windows.Forms.Button btnCreateDun;
        private System.Windows.Forms.Button button5;
    }
}