namespace TabControlDeneme
{
    partial class KullaniciAdi_Degistirme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KullaniciAdi_Degistirme));
            this.txt_yenikullanici_Degis = new System.Windows.Forms.TextBox();
            this.txt_kullaniciadi_Degis = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_kullanici_degistir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.epHataDedektoru = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_yenikullanici_Degis
            // 
            this.txt_yenikullanici_Degis.Location = new System.Drawing.Point(144, 56);
            this.txt_yenikullanici_Degis.Name = "txt_yenikullanici_Degis";
            this.txt_yenikullanici_Degis.Size = new System.Drawing.Size(121, 20);
            this.txt_yenikullanici_Degis.TabIndex = 6;
            // 
            // txt_kullaniciadi_Degis
            // 
            this.txt_kullaniciadi_Degis.Location = new System.Drawing.Point(144, 21);
            this.txt_kullaniciadi_Degis.Name = "txt_kullaniciadi_Degis";
            this.txt_kullaniciadi_Degis.Size = new System.Drawing.Size(121, 20);
            this.txt_kullaniciadi_Degis.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // btn_kullanici_degistir
            // 
            this.btn_kullanici_degistir.Image = global::TabControlDeneme.Properties.Resources.save;
            this.btn_kullanici_degistir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_kullanici_degistir.Location = new System.Drawing.Point(192, 82);
            this.btn_kullanici_degistir.Name = "btn_kullanici_degistir";
            this.btn_kullanici_degistir.Size = new System.Drawing.Size(73, 54);
            this.btn_kullanici_degistir.TabIndex = 7;
            this.btn_kullanici_degistir.Text = "DEĞİŞTİR";
            this.btn_kullanici_degistir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_kullanici_degistir.UseVisualStyleBackColor = true;
            this.btn_kullanici_degistir.Click += new System.EventHandler(this.btn_kullanici_degistir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(22, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Yeni Kullanıcı Adı";
            // 
            // epHataDedektoru
            // 
            this.epHataDedektoru.ContainerControl = this;
            // 
            // KullaniciAdi_Degistirme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 148);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_kullanici_degistir);
            this.Controls.Add(this.txt_yenikullanici_Degis);
            this.Controls.Add(this.txt_kullaniciadi_Degis);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KullaniciAdi_Degistirme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KULLANICI ADI DEĞİŞTİRME";
            this.Load += new System.EventHandler(this.KullaniciAdi_Degistirme_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_yenikullanici_Degis;
        private System.Windows.Forms.TextBox txt_kullaniciadi_Degis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_kullanici_degistir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider epHataDedektoru;
    }
}