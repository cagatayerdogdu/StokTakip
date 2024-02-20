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
using System.Xml;//XML dosya işlemleri için kullanılan Xml kütüphanesi
using Microsoft.Win32; 

namespace TabControlDeneme
{
    public partial class frmParolaDegistir : Form
    {
        int uye_id;
        string yetki_duzeyi;
        public frmParolaDegistir(int _uye_id, string _yetki_duzeyi )
        {
            InitializeComponent();
            uye_id = _uye_id;
            yetki_duzeyi = _yetki_duzeyi;
        }

        DAL budak;
        private void frmParolaDegistir_Load(object sender, EventArgs e)
        {
            #region Veritabanı bağlantısı

            ////Xml dosyamızı okumak için bir XmlDocument nesnesi oluşturuyoruz.
            //XmlDocument xml_belgesi = new XmlDocument();

            ////Xml nesnemize, bağlantı ayarlarını içeren xml dosyamızı yüklüyoruz.
            //xml_belgesi.Load(@"ayarlar\baglanti.xml");

            ////Bağlantı parametrelerimizi tanımlıyoruz.
            //string baglanti_bilgisayar = "";
            //string baglanti_veritabani = "";
            //string baglanti_ek_bilgiler = "";
            ////Bağlantı tag'ımızı seçiyoruz.
            //XmlNodeList baglanti_taglari = xml_belgesi.SelectNodes("/Baglanti");
            ////Xml dosyamızı okuyarak gerekli bilgileri değişkenlere atıyoruz.
            //foreach (XmlNode baglanti_tag in baglanti_taglari)
            //{
            //    baglanti_bilgisayar = baglanti_tag["Kaynak_Bilgisayar"].InnerText;
            //    baglanti_veritabani = baglanti_tag["Veritabani"].InnerText;
            //    baglanti_ek_bilgiler = baglanti_tag["Ek_Bilgiler"].InnerText;
            //}

            ////Dosyadan okuduğumuz bilgiler ile veritabanı bağlantı dizemizi oluşturuyoruz.
            //string baglanti_bilgisi = "Data Source=" + baglanti_bilgisayar + ";Initial Catalog=" + baglanti_veritabani + ";" + baglanti_ek_bilgiler;
            ////DAL nesnemize, oluşturduğumuz bağlantı dizesini parametre olarak atıyoruz.
            //budak = new DAL(baglanti_bilgisi);

            try
            {
                string baglanti_bilgisi = "Data Source=" + Application.StartupPath + "\\StokTakip.s3db";
                //DAL nesnemize, oluşturduğumuz bağlantı dizesini parametre olarak atıyoruz.
                //MessageBox.Show(baglanti_bilgisi);
                budak = new DAL(baglanti_bilgisi);
            }
            catch (Exception hata)
            {
                string problem = hata.ToString();
            }

            #endregion

            if (yetki_duzeyi!="Yonetici")
            {
                DataTable dt_uye_bilgileri = budak.Sorgu_DataTable("SELECT KullaniciAdi FROM yetkiler WHERE YetkiID=@uye_id",uye_id.ToString());
            txtKullaniciAdiDegistir.Text = dt_uye_bilgileri.Rows[0][0].ToString();
            txtKullaniciAdiDegistir.ReadOnly = true;
            }
            
            
        }

        private void btnParolaDegisKaydet_Click(object sender, EventArgs e)
        {
            epHataDedektoru.Clear();
            string kullanici_adi = txtKullaniciAdiDegistir.Text.Trim();
            string eski_parola = txtEskiParolaDegistir.Text.Trim();
            string yeni_parola = txtYeniParolaDegistir.Text.Trim();
            if (kullanici_adi.Length == 0)
            {
                epHataDedektoru.SetError(txtKullaniciAdiDegistir, "Kullanıcı adı girmediniz.");
            }
            else if (eski_parola.Length == 0)
            {
                epHataDedektoru.SetError(txtEskiParolaDegistir, "Eski parolanızı girmediniz.");
            }
            else if(yeni_parola.Length == 0)
            {
                epHataDedektoru.SetError(txtYeniParolaDegistir, "Yeni parolanızı girmediniz.");
            }
            else
            {
                string sorgu_uye_bilgileri = "UPDATE yetkiler SET Parola=@yeni_parola WHERE KullaniciAdi=@kullanici_adi AND Parola=@eski_parola";
                int uye_guncelleme_kontrol = budak.Sorgu_Calistir(sorgu_uye_bilgileri, yeni_parola, kullanici_adi, eski_parola);
                string bildirim = "";
                if (uye_guncelleme_kontrol > 0)
                {
                    bildirim = "Şifreniz başarıyla güncellenmiştir.";
                }
                else
                {
                    bildirim = "Girdiğiniz kullanıcı adı ve şifre ile eşleşen üye kaydı bulunamadı.\n"+
                            "Lütfen bilgilerinizi kontrol edip tekrar deneyiniz.";
                }
                MessageBox.Show(bildirim);
            }
        }

        #region Enter ile Giriş
        private void txtKullaniciAdiDegistir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(13))
            {
                btnParolaDegisKaydet_Click(sender,e);
            }
        }

        private void txtEskiParolaDegistir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(13))
            {
                btnParolaDegisKaydet_Click(sender,e);
            }
        }      
        private void txtYeniParolaDegistir_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(13))
            {
                btnParolaDegisKaydet_Click(sender,e);
            }
        }
        #endregion
    }
}