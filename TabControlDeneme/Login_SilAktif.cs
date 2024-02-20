using System;
using System.Collections;
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
    public partial class Login_SilAktif : Form
    {
        public Login_SilAktif()
        {
            InitializeComponent();
        }

        DAL budak;
        //int kayit_varmi = 0;

        private void Login_SilAktif_Load(object sender, EventArgs e)
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

            try
            {
                string baglanti_bilgisi = "Data Source=" + Application.StartupPath + "\\StokTakip.s3db";
                //DAL nesnemize, oluşturduğumuz bağlantı dizesini parametre olarak atıyoruz.
                //MessageBox.Show(baglanti_bilgisi);
                budak = new DAL(baglanti_bilgisi);
            }
            catch(Exception hata) 
            {
                string problem = hata.ToString();
            }

            string sorgu_kayit_varmi = "SELECT KullaniciAdi FROM yetkiler";
            bool kayit_varmi = budak.Kayit_var_mi(sorgu_kayit_varmi);
            if (kayit_varmi)
            {
               // MessageBox.Show("veri var!");
                btn_yeni_kullanici_ekle.Visible = false;
            }
            else
            {
                btn_yeni_kullanici_ekle.Visible = true;
            }

            #endregion
        }
        private void btnParolaDegistir_Click(object sender, EventArgs e)
        {
            //frmParolaDegistir frmParoladegis = new frmParolaDegistir();
            //frmParoladegis.ShowDialog();
        }

        private void btnParolaGiris_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT YetkiID,YetkiDuzeyi FROM yetkiler WHERE KullaniciAdi=@ad AND Parola=@parola";
            DataTable dt_uye_bilgileri = budak.Sorgu_DataTable(sorgu, txtKullaniciAdi.Text, txtParola.Text);
            if (dt_uye_bilgileri.Rows.Count > 0) //Girilen bilgilerle uyuşan bir üye kaydı bulunmuşsa, üye girişi yapılıyor.
            {
                int uye_id = Convert.ToInt32(dt_uye_bilgileri.Rows[0][0].ToString());
                string yetki_duzeyi = dt_uye_bilgileri.Rows[0][1].ToString();
                Form1 frm_anasayfa = new Form1(uye_id,yetki_duzeyi);
                frm_anasayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bilgilerinizi hatalı girdiniz.");
            }
        }

        #region ENTER tuşu ile giriş yapma
        private void txtKullaniciAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ENTER butonuna basınca, GİRİŞ butonunun tetiklenmesi sağlanıyor.
            if (e.KeyChar == (13)) //ENTER butonunun karakter kodu 13.
            {
                btnParolaGiris_Click(sender, e);
            }
        }

        private void txtParola_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ENTER butonuna basınca, GİRİŞ butonunun tetiklenmesi sağlanıyor.
            if (e.KeyChar == (13)) //ENTER butonunun karakter kodu 13.
            {
                btnParolaGiris_Click(sender, e);
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkış yapmak istediğinize emin misiniz?","PROGRAM KAPATILIYOR",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_yeni_kullanici_ekle_Click(object sender, EventArgs e)
        {
            TESTKullaniciEkle kull_ekle = new TESTKullaniciEkle();
            kull_ekle.Show();
            this.Hide();
        }
        
    }
}
