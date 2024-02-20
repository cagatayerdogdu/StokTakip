namespace TabControlDeneme
{
    partial class HurdaGeriAlimi
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
            this.btnHurdayi_GeriAl = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHurda_GeriAl_Aciklama = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnHurdayi_GeriAl
            // 
            this.btnHurdayi_GeriAl.Location = new System.Drawing.Point(15, 200);
            this.btnHurdayi_GeriAl.Name = "btnHurdayi_GeriAl";
            this.btnHurdayi_GeriAl.Size = new System.Drawing.Size(75, 40);
            this.btnHurdayi_GeriAl.TabIndex = 5;
            this.btnHurdayi_GeriAl.Text = "HURDAYI GERİ AL";
            this.btnHurdayi_GeriAl.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hurdanın Geri Alımı İçin Açıklama Giriniz";
            // 
            // txtHurda_GeriAl_Aciklama
            // 
            this.txtHurda_GeriAl_Aciklama.Location = new System.Drawing.Point(15, 51);
            this.txtHurda_GeriAl_Aciklama.Multiline = true;
            this.txtHurda_GeriAl_Aciklama.Name = "txtHurda_GeriAl_Aciklama";
            this.txtHurda_GeriAl_Aciklama.Size = new System.Drawing.Size(236, 136);
            this.txtHurda_GeriAl_Aciklama.TabIndex = 3;
            // 
            // HurdaGeriAlimi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 245);
            this.Controls.Add(this.btnHurdayi_GeriAl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHurda_GeriAl_Aciklama);
            this.Name = "HurdaGeriAlimi";
            this.Text = "HURDA GERİ ALIMI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHurdayi_GeriAl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHurda_GeriAl_Aciklama;
    }
}