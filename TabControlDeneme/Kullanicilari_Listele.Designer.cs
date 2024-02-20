namespace TabControlDeneme
{
    partial class Kullanicilari_Listele
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kullanicilari_Listele));
            this.dgvKullaniciListesi = new System.Windows.Forms.DataGridView();
            this.clmYetkiId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmKullaniciAdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmYetkiDuzeyi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmKayitTarih = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MenuSag = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSecilenKullaniciSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKullaniciListesi)).BeginInit();
            this.MenuSag.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvKullaniciListesi
            // 
            this.dgvKullaniciListesi.AllowUserToAddRows = false;
            this.dgvKullaniciListesi.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvKullaniciListesi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvKullaniciListesi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKullaniciListesi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmYetkiId,
            this.clmKullaniciAdi,
            this.clmYetkiDuzeyi,
            this.clmKayitTarih});
            this.dgvKullaniciListesi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvKullaniciListesi.Location = new System.Drawing.Point(0, 0);
            this.dgvKullaniciListesi.Name = "dgvKullaniciListesi";
            this.dgvKullaniciListesi.RowHeadersVisible = false;
            this.dgvKullaniciListesi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKullaniciListesi.Size = new System.Drawing.Size(402, 237);
            this.dgvKullaniciListesi.TabIndex = 0;
            this.dgvKullaniciListesi.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvKullaniciListesi_MouseUp);
            // 
            // clmYetkiId
            // 
            this.clmYetkiId.DataPropertyName = "YetkiID";
            this.clmYetkiId.HeaderText = "YETKI ID";
            this.clmYetkiId.Name = "clmYetkiId";
            this.clmYetkiId.Visible = false;
            // 
            // clmKullaniciAdi
            // 
            this.clmKullaniciAdi.DataPropertyName = "KullaniciAdi";
            this.clmKullaniciAdi.HeaderText = "KULLANICI ADI";
            this.clmKullaniciAdi.Name = "clmKullaniciAdi";
            this.clmKullaniciAdi.Width = 200;
            // 
            // clmYetkiDuzeyi
            // 
            this.clmYetkiDuzeyi.DataPropertyName = "YetkiDuzeyi";
            this.clmYetkiDuzeyi.HeaderText = "YETKİ DÜZEYİ";
            this.clmYetkiDuzeyi.Name = "clmYetkiDuzeyi";
            // 
            // clmKayitTarih
            // 
            this.clmKayitTarih.DataPropertyName = "UyeKayitTarih";
            this.clmKayitTarih.HeaderText = "ÜYE KAYIT TARİHİ";
            this.clmKayitTarih.Name = "clmKayitTarih";
            // 
            // MenuSag
            // 
            this.MenuSag.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSecilenKullaniciSilToolStripMenuItem});
            this.MenuSag.Name = "MenuSag";
            this.MenuSag.Size = new System.Drawing.Size(184, 26);
            // 
            // menuSecilenKullaniciSilToolStripMenuItem
            // 
            this.menuSecilenKullaniciSilToolStripMenuItem.Name = "menuSecilenKullaniciSilToolStripMenuItem";
            this.menuSecilenKullaniciSilToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.menuSecilenKullaniciSilToolStripMenuItem.Text = "Seçilen Kullanıcıyı Sil";
            this.menuSecilenKullaniciSilToolStripMenuItem.Click += new System.EventHandler(this.menuSecilenKullaniciSilToolStripMenuItem_Click);
            // 
            // Kullanicilari_Listele
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 237);
            this.Controls.Add(this.dgvKullaniciListesi);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Kullanicilari_Listele";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KULLANICILAR LİSTELENİYOR";
            this.Load += new System.EventHandler(this.Kullanicilari_Listele_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKullaniciListesi)).EndInit();
            this.MenuSag.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKullaniciListesi;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmYetkiId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmKullaniciAdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmYetkiDuzeyi;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmKayitTarih;
        private System.Windows.Forms.ContextMenuStrip MenuSag;
        private System.Windows.Forms.ToolStripMenuItem menuSecilenKullaniciSilToolStripMenuItem;
    }
}