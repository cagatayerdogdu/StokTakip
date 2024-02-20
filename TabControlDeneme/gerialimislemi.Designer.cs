namespace TabControlDeneme
{
    partial class gerialimislemi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gerialimislemi));
            this.rdyeni_urun = new System.Windows.Forms.RadioButton();
            this.rdcikis = new System.Windows.Forms.RadioButton();
            this.btn_geri_alim_islem_yap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdyeni_urun
            // 
            this.rdyeni_urun.AutoSize = true;
            this.rdyeni_urun.Location = new System.Drawing.Point(12, 25);
            this.rdyeni_urun.Name = "rdyeni_urun";
            this.rdyeni_urun.Size = new System.Drawing.Size(96, 17);
            this.rdyeni_urun.TabIndex = 2;
            this.rdyeni_urun.TabStop = true;
            this.rdyeni_urun.Text = "Yeni Stok Girişi";
            this.rdyeni_urun.UseVisualStyleBackColor = true;
            this.rdyeni_urun.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rdyeni_urun_KeyPress);
            // 
            // rdcikis
            // 
            this.rdcikis.AutoSize = true;
            this.rdcikis.Location = new System.Drawing.Point(12, 58);
            this.rdcikis.Name = "rdcikis";
            this.rdcikis.Size = new System.Drawing.Size(112, 17);
            this.rdcikis.TabIndex = 3;
            this.rdcikis.TabStop = true;
            this.rdcikis.Text = "Stok Çıkış İşlemleri";
            this.rdcikis.UseVisualStyleBackColor = true;
            this.rdcikis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rdcikis_KeyPress);
            // 
            // btn_geri_alim_islem_yap
            // 
            this.btn_geri_alim_islem_yap.Image = global::TabControlDeneme.Properties.Resources.make_to_process;
            this.btn_geri_alim_islem_yap.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_geri_alim_islem_yap.Location = new System.Drawing.Point(144, 25);
            this.btn_geri_alim_islem_yap.Name = "btn_geri_alim_islem_yap";
            this.btn_geri_alim_islem_yap.Size = new System.Drawing.Size(75, 50);
            this.btn_geri_alim_islem_yap.TabIndex = 1;
            this.btn_geri_alim_islem_yap.Text = "İşlem Yap";
            this.btn_geri_alim_islem_yap.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_geri_alim_islem_yap.UseVisualStyleBackColor = true;
            this.btn_geri_alim_islem_yap.Click += new System.EventHandler(this.btn_geri_alim_islem_yap_Click);
            // 
            // gerialimislemi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 101);
            this.Controls.Add(this.btn_geri_alim_islem_yap);
            this.Controls.Add(this.rdcikis);
            this.Controls.Add(this.rdyeni_urun);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "gerialimislemi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GERİ ALIM İŞLEMİ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdyeni_urun;
        private System.Windows.Forms.RadioButton rdcikis;
        private System.Windows.Forms.Button btn_geri_alim_islem_yap;
    }
}