namespace TabControlDeneme
{
    partial class TESTKullaniciEkle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TESTKullaniciEkle));
            this.dtuye_kayit_tarihi = new System.Windows.Forms.DateTimePicker();
            this.cmb_yetki_duzey = new System.Windows.Forms.ComboBox();
            this.txt_parola = new System.Windows.Forms.TextBox();
            this.txt_kullanici_adi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_kullanici_kaydet = new System.Windows.Forms.Button();
            this.epHataDedektoru = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).BeginInit();
            this.SuspendLayout();
            // 
            // dtuye_kayit_tarihi
            // 
            this.dtuye_kayit_tarihi.Location = new System.Drawing.Point(117, 124);
            this.dtuye_kayit_tarihi.Name = "dtuye_kayit_tarihi";
            this.dtuye_kayit_tarihi.Size = new System.Drawing.Size(121, 20);
            this.dtuye_kayit_tarihi.TabIndex = 10;
            // 
            // cmb_yetki_duzey
            // 
            this.cmb_yetki_duzey.FormattingEnabled = true;
            this.cmb_yetki_duzey.Items.AddRange(new object[] {
            "Yonetici",
            "Kullanici"});
            this.cmb_yetki_duzey.Location = new System.Drawing.Point(117, 87);
            this.cmb_yetki_duzey.Name = "cmb_yetki_duzey";
            this.cmb_yetki_duzey.Size = new System.Drawing.Size(121, 21);
            this.cmb_yetki_duzey.TabIndex = 8;
            // 
            // txt_parola
            // 
            this.txt_parola.Location = new System.Drawing.Point(117, 54);
            this.txt_parola.Name = "txt_parola";
            this.txt_parola.Size = new System.Drawing.Size(121, 20);
            this.txt_parola.TabIndex = 7;
            this.txt_parola.UseSystemPasswordChar = true;
            // 
            // txt_kullanici_adi
            // 
            this.txt_kullanici_adi.Location = new System.Drawing.Point(117, 20);
            this.txt_kullanici_adi.Name = "txt_kullanici_adi";
            this.txt_kullanici_adi.Size = new System.Drawing.Size(121, 20);
            this.txt_kullanici_adi.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(20, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Kayıt Tarihi";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(20, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Yetki Düzeyi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(20, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Parola";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // btn_kullanici_kaydet
            // 
            this.btn_kullanici_kaydet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_kullanici_kaydet.Image = global::TabControlDeneme.Properties.Resources.save;
            this.btn_kullanici_kaydet.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_kullanici_kaydet.Location = new System.Drawing.Point(163, 163);
            this.btn_kullanici_kaydet.Name = "btn_kullanici_kaydet";
            this.btn_kullanici_kaydet.Size = new System.Drawing.Size(75, 53);
            this.btn_kullanici_kaydet.TabIndex = 15;
            this.btn_kullanici_kaydet.Text = "KAYDET";
            this.btn_kullanici_kaydet.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_kullanici_kaydet.UseVisualStyleBackColor = true;
            this.btn_kullanici_kaydet.Click += new System.EventHandler(this.btn_kullanici_kaydet_Click);
            // 
            // epHataDedektoru
            // 
            this.epHataDedektoru.ContainerControl = this;
            // 
            // TESTKullaniciEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 228);
            this.Controls.Add(this.btn_kullanici_kaydet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtuye_kayit_tarihi);
            this.Controls.Add(this.cmb_yetki_duzey);
            this.Controls.Add(this.txt_parola);
            this.Controls.Add(this.txt_kullanici_adi);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TESTKullaniciEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YENİ KULLANICI KAYDI";
            this.Load += new System.EventHandler(this.TESTKullaniciEkle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtuye_kayit_tarihi;
        private System.Windows.Forms.ComboBox cmb_yetki_duzey;
        private System.Windows.Forms.TextBox txt_parola;
        private System.Windows.Forms.TextBox txt_kullanici_adi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_kullanici_kaydet;
        private System.Windows.Forms.ErrorProvider epHataDedektoru;
    }
}