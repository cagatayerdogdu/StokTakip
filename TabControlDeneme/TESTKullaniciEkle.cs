using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TabControlDeneme
{
    public partial class TESTKullaniciEkle : Form
    {
        public TESTKullaniciEkle()
        {
            InitializeComponent();
        }
        DAL budak;
                
        private void TESTKullaniciEkle_Load(object sender, EventArgs e)
        {
            try
            {
                string baglanti_bilgisi = "Data Source=" + Application.StartupPath + "\\StokTakip.s3db";
                budak = new DAL(baglanti_bilgisi);
            }
            catch (Exception hata)
            {

                string problem = hata.ToString();
            }
        }

        private void btn_kullanici_kaydet_Click(object sender, EventArgs e)
        {
             string bildirim = "";
            int eklenen_uye = 0;

            string sorgu_kullanici_kaydet = "INSERT INTO yetkiler(KullaniciAdi,Parola,YetkiDuzeyi,UyeKayitTarih) VALUES (@kul_adi,@parola,@yetki_duzeyi,@tarih)";
            string formatli_tarih = tarih_formatla(dtuye_kayit_tarihi.Value.ToString());
            //MessageBox.Show(formatli_tarih);
            eklenen_uye = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_kullanici_kaydet, txt_kullanici_adi.Text.Trim(), txt_parola.Text.Trim(), cmb_yetki_duzey.SelectedItem.ToString(), formatli_tarih);
            if (eklenen_uye > 0)
            {
                bildirim = "Üye kaydı başarılı";
            }
            else
            {
                //MessageBox.Show(eklenen_uye.ToString());
                bildirim = "Üye kaydı BAŞARISIZ!";
            }
            MessageBox.Show(bildirim);
            Login_SilAktif giris = new Login_SilAktif();
            giris.Show();
            this.Hide();          
            
        }

        private string tarih_formatla(string tarih)
        {
            #region TARİH FORMATLA

            string[] dizi_tarih_full = tarih.Split(' ');
            string[] dizi_tarih = dizi_tarih_full[0].Split('.');
            string[] dizi_saat = dizi_tarih_full[1].Split(':');
            string Yil = dizi_tarih[2];
            string Ay = dizi_tarih[1];
            string Gun = dizi_tarih[0];
            string Saat = dizi_saat[0];
            string Dakika = dizi_saat[1];
            string Saniye = dizi_saat[2];
            string formatli_tarih = Yil + "-" + Ay + "-" + Gun;
            return formatli_tarih;

            #endregion
        }

    }
}
