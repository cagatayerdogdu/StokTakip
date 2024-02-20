namespace TabControlDeneme
{
    partial class uye_yonetimi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uye_yonetimi));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtuye_kullanici_adi = new System.Windows.Forms.TextBox();
            this.txtuye_parola = new System.Windows.Forms.TextBox();
            this.cmbyetki_duzeyi = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtuye_kayit_tarihi = new System.Windows.Forms.DateTimePicker();
            this.epHataDedektoru = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnTum_uye_liste = new System.Windows.Forms.Button();
            this.btn_Kaydet_Sil = new System.Windows.Forms.Button();
            this.btnKullaniciSil = new System.Windows.Forms.Button();
            this.btnuye_kaydet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(20, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Parola";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(20, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Yetki Düzeyi";
            // 
            // txtuye_kullanici_adi
            // 
            this.txtuye_kullanici_adi.Location = new System.Drawing.Point(117, 20);
            this.txtuye_kullanici_adi.Name = "txtuye_kullanici_adi";
            this.txtuye_kullanici_adi.Size = new System.Drawing.Size(121, 20);
            this.txtuye_kullanici_adi.TabIndex = 1;
            this.txtuye_kullanici_adi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtuye_kullanici_adi_KeyPress);
            // 
            // txtuye_parola
            // 
            this.txtuye_parola.Location = new System.Drawing.Point(117, 54);
            this.txtuye_parola.Name = "txtuye_parola";
            this.txtuye_parola.Size = new System.Drawing.Size(121, 20);
            this.txtuye_parola.TabIndex = 2;
            this.txtuye_parola.UseSystemPasswordChar = true;
            this.txtuye_parola.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtuye_parola_KeyPress);
            // 
            // cmbyetki_duzeyi
            // 
            this.cmbyetki_duzeyi.FormattingEnabled = true;
            this.cmbyetki_duzeyi.Items.AddRange(new object[] {
            "Yonetici",
            "Kullanici"});
            this.cmbyetki_duzeyi.Location = new System.Drawing.Point(117, 87);
            this.cmbyetki_duzeyi.Name = "cmbyetki_duzeyi";
            this.cmbyetki_duzeyi.Size = new System.Drawing.Size(121, 21);
            this.cmbyetki_duzeyi.TabIndex = 3;
            this.cmbyetki_duzeyi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbyetki_duzeyi_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(20, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Kayıt Tarihi";
            // 
            // dtuye_kayit_tarihi
            // 
            this.dtuye_kayit_tarihi.Location = new System.Drawing.Point(117, 124);
            this.dtuye_kayit_tarihi.Name = "dtuye_kayit_tarihi";
            this.dtuye_kayit_tarihi.Size = new System.Drawing.Size(121, 20);
            this.dtuye_kayit_tarihi.TabIndex = 4;
            // 
            // epHataDedektoru
            // 
            this.epHataDedektoru.ContainerControl = this;
            // 
            // btnTum_uye_liste
            // 
            this.btnTum_uye_liste.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTum_uye_liste.Location = new System.Drawing.Point(23, 224);
            this.btnTum_uye_liste.Name = "btnTum_uye_liste";
            this.btnTum_uye_liste.Size = new System.Drawing.Size(215, 31);
            this.btnTum_uye_liste.TabIndex = 8;
            this.btnTum_uye_liste.Text = "Tüm Kullanıcıları Listele";
            this.btnTum_uye_liste.UseVisualStyleBackColor = true;
            this.btnTum_uye_liste.Click += new System.EventHandler(this.btnTum_uye_liste_Click);
            // 
            // btn_Kaydet_Sil
            // 
            this.btn_Kaydet_Sil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Kaydet_Sil.Image = global::TabControlDeneme.Properties.Resources.save;
            this.btn_Kaydet_Sil.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Kaydet_Sil.Location = new System.Drawing.Point(116, 163);
            this.btn_Kaydet_Sil.Name = "btn_Kaydet_Sil";
            this.btn_Kaydet_Sil.Size = new System.Drawing.Size(75, 53);
            this.btn_Kaydet_Sil.TabIndex = 7;
            this.btn_Kaydet_Sil.Text = "KAYDET";
            this.btn_Kaydet_Sil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Kaydet_Sil.UseVisualStyleBackColor = true;
            this.btn_Kaydet_Sil.Visible = false;
            this.btn_Kaydet_Sil.Click += new System.EventHandler(this.btn_Kaydet_Sil_Click);
            // 
            // btnKullaniciSil
            // 
            this.btnKullaniciSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKullaniciSil.Image = global::TabControlDeneme.Properties.Resources.delete;
            this.btnKullaniciSil.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnKullaniciSil.Location = new System.Drawing.Point(23, 163);
            this.btnKullaniciSil.Name = "btnKullaniciSil";
            this.btnKullaniciSil.Size = new System.Drawing.Size(87, 53);
            this.btnKullaniciSil.TabIndex = 6;
            this.btnKullaniciSil.Text = "Kullanıcı Sil";
            this.btnKullaniciSil.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnKullaniciSil.UseVisualStyleBackColor = true;
            this.btnKullaniciSil.Click += new System.EventHandler(this.btnKullaniciSil_Click);
            // 
            // btnuye_kaydet
            // 
            this.btnuye_kaydet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnuye_kaydet.Image = global::TabControlDeneme.Properties.Resources.save;
            this.btnuye_kaydet.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnuye_kaydet.Location = new System.Drawing.Point(163, 163);
            this.btnuye_kaydet.Name = "btnuye_kaydet";
            this.btnuye_kaydet.Size = new System.Drawing.Size(75, 53);
            this.btnuye_kaydet.TabIndex = 5;
            this.btnuye_kaydet.Text = "KAYDET";
            this.btnuye_kaydet.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnuye_kaydet.UseVisualStyleBackColor = true;
            this.btnuye_kaydet.Click += new System.EventHandler(this.button1_Click);
            // 
            // uye_yonetimi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 267);
            this.Controls.Add(this.btnuye_kaydet);
            this.Controls.Add(this.btnTum_uye_liste);
            this.Controls.Add(this.btn_Kaydet_Sil);
            this.Controls.Add(this.btnKullaniciSil);
            this.Controls.Add(this.dtuye_kayit_tarihi);
            this.Controls.Add(this.cmbyetki_duzeyi);
            this.Controls.Add(this.txtuye_parola);
            this.Controls.Add(this.txtuye_kullanici_adi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "uye_yonetimi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YÖNETİCİ PANELİ";
            this.Load += new System.EventHandler(this.uye_yonetimi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider epHataDedektoru;
        public System.Windows.Forms.TextBox txtuye_kullanici_adi;
        public System.Windows.Forms.ComboBox cmbyetki_duzeyi;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtuye_parola;
        public System.Windows.Forms.Button btnuye_kaydet;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.DateTimePicker dtuye_kayit_tarihi;
        public System.Windows.Forms.Button btnKullaniciSil;
        public System.Windows.Forms.Button btn_Kaydet_Sil;
        public System.Windows.Forms.Button btnTum_uye_liste;
    }
}