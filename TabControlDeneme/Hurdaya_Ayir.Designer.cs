namespace TabControlDeneme
{
    partial class Hurdaya_Ayir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hurdaya_Ayir));
            this.btnHurdaya_Gonder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHurdaya_Ayir_Aciklama = new System.Windows.Forms.TextBox();
            this.epHataDedektoru = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHurdaya_Gonder
            // 
            this.btnHurdaya_Gonder.Location = new System.Drawing.Point(211, 222);
            this.btnHurdaya_Gonder.Name = "btnHurdaya_Gonder";
            this.btnHurdaya_Gonder.Size = new System.Drawing.Size(95, 32);
            this.btnHurdaya_Gonder.TabIndex = 0;
            this.btnHurdaya_Gonder.Text = "Hurdaya Gönder";
            this.btnHurdaya_Gonder.UseVisualStyleBackColor = true;
            this.btnHurdaya_Gonder.Click += new System.EventHandler(this.btnHurdaya_Gonder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hurdaya Ayırmak için Açıklama Giriniz";
            // 
            // txtHurdaya_Ayir_Aciklama
            // 
            this.txtHurdaya_Ayir_Aciklama.Location = new System.Drawing.Point(15, 50);
            this.txtHurdaya_Ayir_Aciklama.Multiline = true;
            this.txtHurdaya_Ayir_Aciklama.Name = "txtHurdaya_Ayir_Aciklama";
            this.txtHurdaya_Ayir_Aciklama.Size = new System.Drawing.Size(291, 161);
            this.txtHurdaya_Ayir_Aciklama.TabIndex = 3;
            // 
            // epHataDedektoru
            // 
            this.epHataDedektoru.ContainerControl = this;
            // 
            // Hurdaya_Ayir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 266);
            this.Controls.Add(this.txtHurdaya_Ayir_Aciklama);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHurdaya_Gonder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Hurdaya_Ayir";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HURDAYA AYIRMA AÇIKLAMASI";
            this.Load += new System.EventHandler(this.Hurdaya_Ayir_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epHataDedektoru)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHurdaya_Gonder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHurdaya_Ayir_Aciklama;
        private System.Windows.Forms.ErrorProvider epHataDedektoru;
    }
}