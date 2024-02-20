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
    public partial class uye_yonetimi : Form
    {
        public uye_yonetimi()
        {
            InitializeComponent();
        }
        DAL budak;
        
        int secilen_uye = 0;
        private void uye_yonetimi_Load(object sender, EventArgs e)
        {
            #region veritabanı bağlantısı

            //XmlDocument xml_belgesi = new XmlDocument();

            //xml_belgesi.Load(@"ayarlar\baglanti.xml");

            //string baglanti_bilgisayar = "";
            //string baglanti_veritabani = "";
            //string baglanti_ek_bilgiler = "";

            //XmlNodeList baglanti_taglari = xml_belgesi.SelectNodes("/Baglanti");

            //foreach (XmlNode baglanti_tag in baglanti_taglari)
            //{
            //    baglanti_bilgisayar = baglanti_tag["Kaynak_Bilgisayar"].InnerText;
            //    baglanti_veritabani = baglanti_tag["Veritabani"].InnerText;
            //    baglanti_ek_bilgiler = baglanti_tag["Ek_Bilgiler"].InnerText;

            //    string baglanti_bilgisi = "Data Source=" + baglanti_bilgisayar + "; Initial Catalog=" + baglanti_veritabani + ";" + baglanti_ek_bilgiler;

            //    budak = new DAL(baglanti_bilgisi);
            //}

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
        }



        private void button1_Click(object sender, EventArgs e)
        {
            #region ÜYE KAYDEDİLİYOR

            int eklenen_uye = 0;
            string bildirim = "";
            epHataDedektoru.Clear();

            if (txtuye_kullanici_adi.Text.Trim().Length ==0)
            {
                epHataDedektoru.SetError(txtuye_kullanici_adi,"Kullanıcı adını girmediniz.");
            }
            else if (txtuye_parola.Text.Trim().Length ==0)
            {
                epHataDedektoru.SetError(txtuye_parola,"Parolayı girmediniz.");
            }
            else if (cmbyetki_duzeyi.Text.Trim().Length ==0)
            {
                epHataDedektoru.SetError(cmbyetki_duzeyi,"Yetki düzeyini seçmediniz.");
            }
            else
            {
                if (secilen_uye == 0)
                {
                    bool kullanici_adi_kontrol = budak.Kayit_var_mi("SELECT YetkiID FROM yetkiler WHERE KullaniciAdi=@kullanici_adi", txtuye_kullanici_adi.Text.Trim());
                    if (kullanici_adi_kontrol==false)
                    {
                        string sorgu_uye_kaydet = "INSERT INTO yetkiler(KullaniciAdi,Parola,YetkiDuzeyi,UyeKayitTarih) VALUES(@kullanici_adi,@parola,@yetki_duzeyi,@kayit_tarihi)";
                        string formatli_tarih = tarih_formatla(dtuye_kayit_tarihi.Value.ToString());
                        //string formatli_combo = cmbyetki_duzeyi.SelectedItem.ToString(); 
                        eklenen_uye = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_uye_kaydet, txtuye_kullanici_adi.Text.Trim(), txtuye_parola.Text.Trim(), cmbyetki_duzeyi.SelectedItem.ToString(), formatli_tarih);
                    }
                    else
                    {
                        bildirim = "Girdiğiniz kullanıcı adı bir üye tarafından kullanılıyor.\n"+
                            "Lütfen başka bir kullanıcı adı giriniz.";
                    }
                }
            }

            if (eklenen_uye > 1)
            {
                bildirim = "Üye kaydı başarıyla gerçekleştirilmiştir.";
                txtuye_kullanici_adi.Text = "";
                txtuye_parola.Text = "";
                this.Hide();
            }
            else if (bildirim.Length == 0)
            {
                bildirim = "Üye kaydı başarısız. Yeniden deneyin.";
            }
            MessageBox.Show(bildirim);

            #endregion
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

        #region Enter ile giriş yapılıyor

        private void txtuye_kullanici_adi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (13))
            {
                button1_Click(sender,e);
            }
        }

        private void txtuye_parola_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (13))
            {
                button1_Click(sender,e);
            }
        }

        private void cmbyetki_duzeyi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==(13))
            {
                button1_Click(sender,e);
            }
        }

        #endregion

        private void btnKullaniciSil_Click(object sender, EventArgs e)
        {
            #region ÜYE SİLMEK İÇİN DİZAYN AYARLANIYOR
            if (MessageBox.Show("Kullanıcıyı Silmek İstiyor musunuz?","SİLME ONAYI",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                txtuye_parola.Visible = false;
                label2.Visible = false;
                cmbyetki_duzeyi.Location = new Point(117, 54);
                label3.Location = new Point(20, 55);
                label4.Location = new Point(20, 88);
                dtuye_kayit_tarihi.Location = new Point(117, 87);
                btnuye_kaydet.Visible = false;
                btn_Kaydet_Sil.Visible = true;
                btn_Kaydet_Sil.Location = new Point(163,163);
                btnKullaniciSil.Location = new Point(23,163);
                btnKullaniciSil.Enabled = false;
            }
            #endregion
        }
       
        private void btn_Kaydet_Sil_Click(object sender, EventArgs e)
        {
            #region ÜYE KAYDI SİLİNİYOR

            string buradan_sec = txtuye_kullanici_adi.Text;

            if (txtuye_kullanici_adi.Text.Length > 0)
            {
                bool sorgu_eslestir = budak.Kayit_var_mi("SELECT KullaniciAdi From yetkiler WHERE KullaniciAdi=@isim AND YetkiDuzeyi=@yetki", txtuye_kullanici_adi.Text.Trim(), cmbyetki_duzeyi.SelectedItem.ToString());

                if (sorgu_eslestir)
                {
                    string sorgu_uye_delete = "DELETE FROM yetkiler WHERE KullaniciAdi=@isim";
                    secilen_uye = budak.Sorgu_Calistir(sorgu_uye_delete, txtuye_kullanici_adi.Text.Trim());
                    MessageBox.Show("Üye Kaydı Başarıyla Silindi.");
                    this.Hide();

                    //İşlem günlüğe kaydediliyor. ************************ loglar tablosunda işlem kayıt id ve kişi bilgisi yer almıyor.**************
                    // ayrıca üye kaydı yapılırken tarihi 0 atıyor.
                    string sorgu_uye_kayit_id = "SELECT YetkiID FROM yetkiler";
                    bool uye_id;
                    uye_id = budak.Kayit_var_mi(sorgu_uye_kayit_id);
                    string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                    string log_tarih = tarih_formatla(DateTime.Now.ToString());
                    int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "silinen_uye", "delete", uye_id.ToString(), uye_id.ToString(), log_tarih);

                }
                else
                {
                    MessageBox.Show("Üye Silinemedi.");
                }
            }
            #endregion
        }

        private void btnTum_uye_liste_Click(object sender, EventArgs e)
        {            
            Kullanicilari_Listele kull_liste = new Kullanicilari_Listele();
            kull_liste.Show();
            this.Hide();
        }
        
    }
}
