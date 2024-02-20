namespace TabControlDeneme
{
    partial class frmIstekYapilanUrunler
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIstekYapilanUrunler));
            this.btnIstekTamam = new System.Windows.Forms.Button();
            this.btnIstekYapilmadi = new System.Windows.Forms.Button();
            this.lblAciklama = new System.Windows.Forms.Label();
            this.dgv_istek_tahminler = new System.Windows.Forms.DataGridView();
            this.clmIstekId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIstek_MalzemeTuru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIstek_IstenenMiktar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIstek_IstekTarihi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIstek_Aciklama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_istek_tahminler)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIstekTamam
            // 
            this.btnIstekTamam.Location = new System.Drawing.Point(13, 262);
            this.btnIstekTamam.Name = "btnIstekTamam";
            this.btnIstekTamam.Size = new System.Drawing.Size(258, 23);
            this.btnIstekTamam.TabIndex = 1;
            this.btnIstekTamam.Text = "Seçilen ürünü istek listesinden kaldır";
            this.btnIstekTamam.UseVisualStyleBackColor = true;
            this.btnIstekTamam.Click += new System.EventHandler(this.btnIstekTamam_Click);
            // 
            // btnIstekYapilmadi
            // 
            this.btnIstekYapilmadi.Location = new System.Drawing.Point(277, 262);
            this.btnIstekYapilmadi.Name = "btnIstekYapilmadi";
            this.btnIstekYapilmadi.Size = new System.Drawing.Size(201, 23);
            this.btnIstekYapilmadi.TabIndex = 2;
            this.btnIstekYapilmadi.Text = "Giriş yaptığım ürüne ait istek yapılmamış";
            this.btnIstekYapilmadi.UseVisualStyleBackColor = true;
            this.btnIstekYapilmadi.Click += new System.EventHandler(this.btnIstekYapilmadi_Click);
            // 
            // lblAciklama
            // 
            this.lblAciklama.AutoSize = true;
            this.lblAciklama.Location = new System.Drawing.Point(13, 13);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(140, 13);
            this.lblAciklama.TabIndex = 3;
            this.lblAciklama.Text = "Burada açıklamalar yazacak";
            // 
            // dgv_istek_tahminler
            // 
            this.dgv_istek_tahminler.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_istek_tahminler.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_istek_tahminler.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_istek_tahminler.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_istek_tahminler.ColumnHeadersHeight = 20;
            this.dgv_istek_tahminler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmIstekId,
            this.clmIstek_MalzemeTuru,
            this.clmIstek_IstenenMiktar,
            this.clmIstek_IstekTarihi,
            this.clmIstek_Aciklama});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_istek_tahminler.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_istek_tahminler.Location = new System.Drawing.Point(16, 88);
            this.dgv_istek_tahminler.Name = "dgv_istek_tahminler";
            this.dgv_istek_tahminler.RowHeadersVisible = false;
            this.dgv_istek_tahminler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_istek_tahminler.Size = new System.Drawing.Size(462, 168);
            this.dgv_istek_tahminler.TabIndex = 8;
            // 
            // clmIstekId
            // 
            this.clmIstekId.DataPropertyName = "istek_id";
            this.clmIstekId.HeaderText = "İSTEK ID";
            this.clmIstekId.Name = "clmIstekId";
            this.clmIstekId.Visible = false;
            // 
            // clmIstek_MalzemeTuru
            // 
            this.clmIstek_MalzemeTuru.DataPropertyName = "malzeme_turu";
            this.clmIstek_MalzemeTuru.HeaderText = "MALZEME TÜRÜ";
            this.clmIstek_MalzemeTuru.Name = "clmIstek_MalzemeTuru";
            // 
            // clmIstek_IstenenMiktar
            // 
            this.clmIstek_IstenenMiktar.DataPropertyName = "istek_urun_sayisi";
            this.clmIstek_IstenenMiktar.HeaderText = "MİKTAR";
            this.clmIstek_IstenenMiktar.Name = "clmIstek_IstenenMiktar";
            this.clmIstek_IstenenMiktar.Width = 50;
            // 
            // clmIstek_IstekTarihi
            // 
            this.clmIstek_IstekTarihi.DataPropertyName = "istek_tarih";
            this.clmIstek_IstekTarihi.HeaderText = "TARİH";
            this.clmIstek_IstekTarihi.Name = "clmIstek_IstekTarihi";
            this.clmIstek_IstekTarihi.Width = 75;
            // 
            // clmIstek_Aciklama
            // 
            this.clmIstek_Aciklama.DataPropertyName = "istek_aciklama";
            this.clmIstek_Aciklama.HeaderText = "AÇIKLAMA";
            this.clmIstek_Aciklama.Name = "clmIstek_Aciklama";
            this.clmIstek_Aciklama.Width = 235;
            // 
            // frmIstekYapilanUrunler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 293);
            this.Controls.Add(this.dgv_istek_tahminler);
            this.Controls.Add(this.lblAciklama);
            this.Controls.Add(this.btnIstekYapilmadi);
            this.Controls.Add(this.btnIstekTamam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIstekYapilanUrunler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İstek yapılan ürünler";
            this.Load += new System.EventHandler(this.frmIstekYapilanUrunler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_istek_tahminler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIstekTamam;
        private System.Windows.Forms.Button btnIstekYapilmadi;
        private System.Windows.Forms.Label lblAciklama;
        private System.Windows.Forms.DataGridView dgv_istek_tahminler;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIstekId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIstek_MalzemeTuru;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIstek_IstenenMiktar;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIstek_IstekTarihi;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIstek_Aciklama;
    }
}