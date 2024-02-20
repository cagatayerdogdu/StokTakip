namespace TabControlDeneme
{
    partial class HurdayaAyrilanAciklama
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
            this.txtHurdaAyir_Aciklama = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHurdaya_Ayir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtHurdaAyir_Aciklama
            // 
            this.txtHurdaAyir_Aciklama.Location = new System.Drawing.Point(15, 51);
            this.txtHurdaAyir_Aciklama.Multiline = true;
            this.txtHurdaAyir_Aciklama.Name = "txtHurdaAyir_Aciklama";
            this.txtHurdaAyir_Aciklama.Size = new System.Drawing.Size(236, 136);
            this.txtHurdaAyir_Aciklama.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hurdaya Ayrılması İçin Açıklama Giriniz";
            // 
            // btnHurdaya_Ayir
            // 
            this.btnHurdaya_Ayir.Location = new System.Drawing.Point(15, 200);
            this.btnHurdaya_Ayir.Name = "btnHurdaya_Ayir";
            this.btnHurdaya_Ayir.Size = new System.Drawing.Size(75, 40);
            this.btnHurdaya_Ayir.TabIndex = 2;
            this.btnHurdaya_Ayir.Text = "HURDAYA AYIR";
            this.btnHurdaya_Ayir.UseVisualStyleBackColor = true;
            // 
            // HurdayaAyrilanAciklama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 245);
            this.Controls.Add(this.btnHurdaya_Ayir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHurdaAyir_Aciklama);
            this.Name = "HurdayaAyrilanAciklama";
            this.Text = "HURDA AÇIKLAMASI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHurdaAyir_Aciklama;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHurdaya_Ayir;
    }
}