namespace TabControlDeneme
{
    partial class frmParolaDegistir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParolaDegistir));
            this.txtYeniParolaDegistir = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEskiParolaDegistir = new System.Windows.Forms.TextBox();
            this.lblYeniparoladegistir = new System.Windows.Forms.Label();
            this.txtKullaniciAdiDegistir = new System.Windows.Forms.TextBox();
            this.lblEskiparoladegistir = new System.Windows.Forms.Label();
            this.lblKullaniciAdiDegistir = new System.Windows.Forms.Label();
            this.btnParolaDegisKaydet = new System.Windows.Forms.Button();
            this.epHataDedektoru = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).BeginInit();
            this.SuspendLayout();
            // 
            // txtYeniParolaDegistir
            // 
            this.txtYeniParolaDegistir.Location = new System.Drawing.Point(113, 75);
            this.txtYeniParolaDegistir.Name = "txtYeniParolaDegistir";
            this.txtYeniParolaDegistir.Size = new System.Drawing.Size(136, 20);
            this.txtYeniParolaDegistir.TabIndex = 3;
            this.txtYeniParolaDegistir.UseSystemPasswordChar = true;
            this.txtYeniParolaDegistir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYeniParolaDegistir_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtYeniParolaDegistir);
            this.groupBox1.Controls.Add(this.txtEskiParolaDegistir);
            this.groupBox1.Controls.Add(this.lblYeniparoladegistir);
            this.groupBox1.Controls.Add(this.txtKullaniciAdiDegistir);
            this.groupBox1.Controls.Add(this.lblEskiparoladegistir);
            this.groupBox1.Controls.Add(this.lblKullaniciAdiDegistir);
            this.groupBox1.Controls.Add(this.btnParolaDegisKaydet);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 163);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PAROLA İŞLEMLERİ";
            // 
            // txtEskiParolaDegistir
            // 
            this.txtEskiParolaDegistir.Location = new System.Drawing.Point(113, 49);
            this.txtEskiParolaDegistir.Name = "txtEskiParolaDegistir";
            this.txtEskiParolaDegistir.Size = new System.Drawing.Size(136, 20);
            this.txtEskiParolaDegistir.TabIndex = 2;
            this.txtEskiParolaDegistir.UseSystemPasswordChar = true;
            this.txtEskiParolaDegistir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEskiParolaDegistir_KeyPress);
            // 
            // lblYeniparoladegistir
            // 
            this.lblYeniparoladegistir.AutoSize = true;
            this.lblYeniparoladegistir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblYeniparoladegistir.ForeColor = System.Drawing.Color.Black;
            this.lblYeniparoladegistir.Location = new System.Drawing.Point(13, 78);
            this.lblYeniparoladegistir.Name = "lblYeniparoladegistir";
            this.lblYeniparoladegistir.Size = new System.Drawing.Size(72, 13);
            this.lblYeniparoladegistir.TabIndex = 12;
            this.lblYeniparoladegistir.Text = "Yeni Parola";
            // 
            // txtKullaniciAdiDegistir
            // 
            this.txtKullaniciAdiDegistir.Location = new System.Drawing.Point(113, 19);
            this.txtKullaniciAdiDegistir.Name = "txtKullaniciAdiDegistir";
            this.txtKullaniciAdiDegistir.Size = new System.Drawing.Size(136, 20);
            this.txtKullaniciAdiDegistir.TabIndex = 1;
            this.txtKullaniciAdiDegistir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKullaniciAdiDegistir_KeyPress);
            // 
            // lblEskiparoladegistir
            // 
            this.lblEskiparoladegistir.AutoSize = true;
            this.lblEskiparoladegistir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEskiparoladegistir.ForeColor = System.Drawing.Color.Black;
            this.lblEskiparoladegistir.Location = new System.Drawing.Point(13, 52);
            this.lblEskiparoladegistir.Name = "lblEskiparoladegistir";
            this.lblEskiparoladegistir.Size = new System.Drawing.Size(71, 13);
            this.lblEskiparoladegistir.TabIndex = 11;
            this.lblEskiparoladegistir.Text = "Eski Parola";
            // 
            // lblKullaniciAdiDegistir
            // 
            this.lblKullaniciAdiDegistir.AutoSize = true;
            this.lblKullaniciAdiDegistir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKullaniciAdiDegistir.ForeColor = System.Drawing.Color.Black;
            this.lblKullaniciAdiDegistir.Location = new System.Drawing.Point(13, 22);
            this.lblKullaniciAdiDegistir.Name = "lblKullaniciAdiDegistir";
            this.lblKullaniciAdiDegistir.Size = new System.Drawing.Size(77, 13);
            this.lblKullaniciAdiDegistir.TabIndex = 10;
            this.lblKullaniciAdiDegistir.Text = "Kullanıcı Adı";
            // 
            // btnParolaDegisKaydet
            // 
            this.btnParolaDegisKaydet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnParolaDegisKaydet.ForeColor = System.Drawing.Color.Black;
            this.btnParolaDegisKaydet.Image = global::TabControlDeneme.Properties.Resources.save;
            this.btnParolaDegisKaydet.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnParolaDegisKaydet.Location = new System.Drawing.Point(182, 101);
            this.btnParolaDegisKaydet.Name = "btnParolaDegisKaydet";
            this.btnParolaDegisKaydet.Size = new System.Drawing.Size(67, 52);
            this.btnParolaDegisKaydet.TabIndex = 4;
            this.btnParolaDegisKaydet.Text = "Kaydet";
            this.btnParolaDegisKaydet.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnParolaDegisKaydet.UseVisualStyleBackColor = true;
            this.btnParolaDegisKaydet.Click += new System.EventHandler(this.btnParolaDegisKaydet_Click);
            // 
            // epHataDedektoru
            // 
            this.epHataDedektoru.ContainerControl = this;
            // 
            // frmParolaDegistir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 191);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmParolaDegistir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PAROLA DEĞİŞTİR";
            this.Load += new System.EventHandler(this.frmParolaDegistir_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtYeniParolaDegistir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEskiParolaDegistir;
        private System.Windows.Forms.Label lblYeniparoladegistir;
        private System.Windows.Forms.TextBox txtKullaniciAdiDegistir;
        private System.Windows.Forms.Label lblEskiparoladegistir;
        private System.Windows.Forms.Label lblKullaniciAdiDegistir;
        private System.Windows.Forms.Button btnParolaDegisKaydet;
        private System.Windows.Forms.ErrorProvider epHataDedektoru;
    }
}