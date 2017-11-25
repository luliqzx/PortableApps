﻿namespace PortableApps
{
    partial class VariableSettingForm
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
            this.dgvCS = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.txtDescription = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEncrypt = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCS)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCS
            // 
            this.dgvCS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCS.Location = new System.Drawing.Point(12, 138);
            this.dgvCS.Name = "dgvCS";
            this.dgvCS.Size = new System.Drawing.Size(581, 115);
            this.dgvCS.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(83, 6);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(355, 20);
            this.txtKey.TabIndex = 3;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(83, 32);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(355, 45);
            this.txtValue.TabIndex = 4;
            this.txtValue.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(518, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(518, 45);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // pnlPager
            // 
            this.pnlPager.Location = new System.Drawing.Point(12, 266);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(581, 25);
            this.pnlPager.TabIndex = 13;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(83, 83);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(355, 45);
            this.txtDescription.TabIndex = 15;
            this.txtDescription.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Description";
            // 
            // cbEncrypt
            // 
            this.cbEncrypt.AutoSize = true;
            this.cbEncrypt.Location = new System.Drawing.Point(444, 43);
            this.cbEncrypt.Name = "cbEncrypt";
            this.cbEncrypt.Size = new System.Drawing.Size(61, 17);
            this.cbEncrypt.TabIndex = 16;
            this.cbEncrypt.Text = "encrypt";
            this.cbEncrypt.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cbEncrypt.UseVisualStyleBackColor = true;
            // 
            // VariableSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 303);
            this.Controls.Add(this.cbEncrypt);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlPager);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCS);
            this.Name = "VariableSettingForm";
            this.Text = "SettingForm";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.RichTextBox txtValue;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlPager;
        private System.Windows.Forms.RichTextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbEncrypt;
    }
}