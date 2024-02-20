namespace TabControlDeneme
{
    partial class Login_SilAktif
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_SilAktif));
            this.txtParola = new System.Windows.Forms.TextBox();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.lblparola = new System.Windows.Forms.Label();
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.btnParolaDegistir = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnParolaGiris = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_yeni_kullanici_ekle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtParola
            // 
            this.txtParola.Location = new System.Drawing.Point(100, 77);
            this.txtParola.Name = "txtParola";
            this.txtParola.Size = new System.Drawing.Size(140, 20);
            this.txtParola.TabIndex = 2;
            this.txtParola.UseSystemPasswordChar = true;
            this.txtParola.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtParola_KeyPress);
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.Location = new System.Drawing.Point(100, 35);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(140, 20);
            this.txtKullaniciAdi.TabIndex = 1;
            this.txtKullaniciAdi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKullaniciAdi_KeyPress);
            // 
            // lblparola
            // 
            this.lblparola.AutoSize = true;
            this.lblparola.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblparola.ForeColor = System.Drawing.Color.Black;
            this.lblparola.Location = new System.Drawing.Point(12, 80);
            this.lblparola.Name = "lblparola";
            this.lblparola.Size = new System.Drawing.Size(43, 13);
            this.lblparola.TabIndex = 7;
            this.lblparola.Text = "Parola";
            // 
            // lblKullaniciAdi
            // 
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullaniciAdi.ForeColor = System.Drawing.Color.Black;
            this.lblKullaniciAdi.Location = new System.Drawing.Point(12, 38);
            this.lblKullaniciAdi.Name = "lblKullaniciAdi";
            this.lblKullaniciAdi.Size = new System.Drawing.Size(77, 13);
            this.lblKullaniciAdi.TabIndex = 5;
            this.lblKullaniciAdi.Text = "Kullanıcı Adı";
            // 
            // btnParolaDegistir
            // 
            this.btnParolaDegistir.ForeColor = System.Drawing.Color.Black;
            this.btnParolaDegistir.Location = new System.Drawing.Point(299, 77);
            this.btnParolaDegistir.Name = "btnParolaDegistir";
            this.btnParolaDegistir.Size = new System.Drawing.Size(91, 40);
            this.btnParolaDegistir.TabIndex = 4;
            this.btnParolaDegistir.Text = "Parolayı Değiştir";
            this.btnParolaDegistir.UseVisualStyleBackColor = true;
            this.btnParolaDegistir.Visible = false;
            this.btnParolaDegistir.Click += new System.EventHandler(this.btnParolaDegistir_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TabControlDeneme.Properties.Resources.key_2;
            this.pictureBox1.Location = new System.Drawing.Point(242, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(194, 188);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // btnParolaGiris
            // 
            this.btnParolaGiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnParolaGiris.ForeColor = System.Drawing.Color.Black;
            this.btnParolaGiris.Image = global::TabControlDeneme.Properties.Resources.add_key;
            this.btnParolaGiris.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnParolaGiris.Location = new System.Drawing.Point(172, 130);
            this.btnParolaGiris.Name = "btnParolaGiris";
            this.btnParolaGiris.Size = new System.Drawing.Size(70, 54);
            this.btnParolaGiris.TabIndex = 3;
            this.btnParolaGiris.Text = "Giriş";
            this.btnParolaGiris.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnParolaGiris.UseVisualStyleBackColor = true;
            this.btnParolaGiris.Click += new System.EventHandler(this.btnParolaGiris_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Image = global::TabControlDeneme.Properties.Resources.exit;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(15, 130);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 54);
            this.button1.TabIndex = 9;
            this.button1.Text = "Çıkış";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_yeni_kullanici_ekle
            // 
            this.btn_yeni_kullanici_ekle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_yeni_kullanici_ekle.ForeColor = System.Drawing.Color.Black;
            this.btn_yeni_kullanici_ekle.Location = new System.Drawing.Point(91, 130);
            this.btn_yeni_kullanici_ekle.Name = "btn_yeni_kullanici_ekle";
            this.btn_yeni_kullanici_ekle.Size = new System.Drawing.Size(75, 54);
            this.btn_yeni_kullanici_ekle.TabIndex = 10;
            this.btn_yeni_kullanici_ekle.Text = "Yeni Kullanıcı Ekle";
            this.btn_yeni_kullanici_ekle.UseVisualStyleBackColor = true;
            this.btn_yeni_kullanici_ekle.Visible = false;
            this.btn_yeni_kullanici_ekle.Click += new System.EventHandler(this.btn_yeni_kullanici_ekle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(237, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Copyright © by Akyüzlüdede";
            // 
            // Login_SilAktif
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 231);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_yeni_kullanici_ekle);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnParolaDegistir);
            this.Controls.Add(this.txtParola);
            this.Controls.Add(this.txtKullaniciAdi);
            this.Controls.Add(this.btnParolaGiris);
            this.Controls.Add(this.lblparola);
            this.Controls.Add(this.lblKullaniciAdi);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login_SilAktif";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "STOK TAKİP İŞLEMLERİ KULLANICI GİRİŞİ";
            this.Load += new System.EventHandler(this.Login_SilAktif_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtParola;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.Label lblparola;
        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.Button btnParolaDegistir;
        private System.Windows.Forms.Button btnParolaGiris;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_yeni_kullanici_ekle;
        private System.Windows.Forms.Label label1;
    }
}