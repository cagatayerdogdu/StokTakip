using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using DAL;
using System.Xml; //XML dosya işlemleri için kullanılan Xml kütüphanesi
using System.Text.RegularExpressions; //Girilen bilgilerin formatının kontrolünü yapmaya olanak veren kütüphane (girilen değerin sayı olup olmadığının konrolü gibi)

namespace TabControlDeneme
{
    public partial class Kullanicilari_Listele : Form
    {
        public Kullanicilari_Listele()
        {
            InitializeComponent();
        }

        DAL budak;

        private void Kullanicilari_Listele_Load(object sender, EventArgs e)
        {
            #region Veritabanı Bağlantısı

            try
            {
                string baglanti_bilgisi = "Data Source=" + Application.StartupPath + "\\StokTakip.s3db";
                budak = new DAL(baglanti_bilgisi);
            }
            catch (Exception hata)
            {

                string problem = hata.ToString();
            }

            #endregion
            string sorgu_liste_kullanici = "SELECT KullaniciAdi, YetkiDuzeyi, UyeKayitTarih From yetkiler";
            dgvKullaniciListesi.DataSource = budak.Sorgu_DataTable(sorgu_liste_kullanici);
        }

        private void menuSecilenKullaniciSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aklinda_tut = "";
            string aklinda_tut2 = "";
            aklinda_tut = dgvKullaniciListesi.CurrentRow.Cells[1].Value.ToString();            
            aklinda_tut2 = dgvKullaniciListesi.CurrentRow.Cells[2].Value.ToString();
            
            uye_yonetimi uye = new uye_yonetimi();
            uye.Show();
            uye.txtuye_kullanici_adi.Text = aklinda_tut;
            uye.cmbyetki_duzeyi.Text = aklinda_tut2;
            uye.label2.Visible = false;
            uye.txtuye_parola.Visible = false;
            uye.label3.Location = new Point(20, 55);
            uye.cmbyetki_duzeyi.Location = new Point(117, 54);
            uye.label4.Location = new Point(20,88);
            uye.dtuye_kayit_tarihi.Location = new Point(117,87);
            uye.btnuye_kaydet.Visible = false;
            uye.btn_Kaydet_Sil.Visible = true;
            uye.btn_Kaydet_Sil.Location = new Point(163,163);
            uye.btnKullaniciSil.Enabled = false;
            this.Hide();
        }

        private void dgvKullaniciListesi_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                menuSecilenKullaniciSilToolStripMenuItem.Visible = true;
                MenuSag.Show(dgvKullaniciListesi.PointToScreen(e.Location));
            }
        }
    }
}
