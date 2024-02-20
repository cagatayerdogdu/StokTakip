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
using System.Xml;
namespace TabControlDeneme
{
    public partial class KullaniciAdi_Degistirme : Form
    {
        int uye_id;
        //public KullaniciAdi_Degistirme()
        //{
        //    InitializeComponent();
            
        //}
        public KullaniciAdi_Degistirme(int _uye_id)
        {
            InitializeComponent();
            uye_id = _uye_id;
        }
       
        DAL budak;
        int secilen_kullanici = 0;
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

        private void KullaniciAdi_Degistirme_Load(object sender, EventArgs e)
        {
            #region veritabanı bağlantısı

            //XmlDocument xml_belgesi = new XmlDocument();

            //xml_belgesi.Load(@"ayarlar\baglanti.xml");

            //string baglanti_bilgisayar = "";
            //string veritabanı_baglantisi = "";
            //string baglanti_ek_bilgiler = "";

            //XmlNodeList baglanti_taglari = xml_belgesi.SelectNodes("/Baglanti");

            //foreach (XmlNode baglanti_tag in baglanti_taglari)
            //{
            //    baglanti_bilgisayar = baglanti_tag["Kaynak_Bilgisayar"].InnerText;
            //    veritabanı_baglantisi = baglanti_tag["Veritabani"].InnerText;
            //    baglanti_ek_bilgiler = baglanti_tag["Ek_Bilgiler"].InnerText;
            //}

            //string baglanti_bilgisi = "Data Source=" + baglanti_bilgisayar + ";Initial Catalog=" + veritabanı_baglantisi + ";" + baglanti_ek_bilgiler;
            
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
        }

        private void btn_kullanici_degistir_Click(object sender, EventArgs e)
        {
            epHataDedektoru.Clear();
            
            string Eski_kullanici_adi = txt_kullaniciadi_Degis.Text.Trim();
            string Yeni_Kullanici_Adi = txt_yenikullanici_Degis.Text.Trim();
            string bildirim = "";
            if (Eski_kullanici_adi.Length==0)
            {
                epHataDedektoru.SetError(txt_kullaniciadi_Degis, "Şuan ki kullanıcı adınızı girmediniz.");
            }
            else if (Yeni_Kullanici_Adi.Length==0)
            {
                epHataDedektoru.SetError(txt_yenikullanici_Degis, "Yeni kullanıcı adınız girmediniz.");
            }
            else
            {
                string sorgu_kullanici_id_bul = "SELECT YetkiID FROM yetkiler WHERE KullaniciAdi='" + txt_kullaniciadi_Degis.Text + "'";

                DataTable kullanici_id_dt = budak.Sorgu_DataTable(sorgu_kullanici_id_bul);
                int duzeltme_kullanici_id = Convert.ToInt32(kullanici_id_dt.Rows[0][0].ToString()); // kullanıcı yoksa uyarı versin, çünkü program patlıyor.
                //if (duzeltme_kullanici_id<0)
                //{
                //    bildirim = "Girdiğiniz kullanıcı adı ile eşleşen kayıt bulunamadı.\n"+
                //            "Lütfen bilgileriniz kontrol edip tekrar deneyiniz." ; 
                //}
                ////MessageBox.Show(bildirim);
                if (true)
                {
                    string sorgu_update_kullaniciadi_degis = "UPDATE yetkiler SET KullaniciAdi=@kullaniciadi WHERE YetkiID=@yetkiid";

                    int duzeltilen_kullanici_adi = budak.Sorgu_Calistir(sorgu_update_kullaniciadi_degis,
                        txt_yenikullanici_Degis.Text, duzeltme_kullanici_id.ToString());

                    string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih)"+ 
                    "VALUES (@islem_ad,@islem_tip,@islem_kayit_id,@kisi,@tarih)";
                    string log_tarih = tarih_formatla(DateTime.Now.ToString());
                    int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "giris", "update",  uye_id.ToString(), log_tarih);

                    if (duzeltme_kullanici_id>0)
                    {
                        bildirim = "Kullanıcı adınızı başarıyla değiştirdiniz.";
                    }
                    else
                    { 
                        bildirim = "Girdiğiniz kullanıcı adı ile eşleşen kayıt bulunamadı.\n"+
                            "Lütfen bilgileriniz kontrol edip tekrar deneyiniz." ;                      
                    }
                    MessageBox.Show(bildirim);
                }
                                

                                
            }

            //---------------------------------------------------------------------------------------------

            //if (Eski_kullanici_adi.Length==0)
            //{
            //    epHataDedektoru.SetError(txt_kullaniciadi_Degis, "Şuan ki kullanıcı adınızı girmediniz.");
            //}
            //else if (Yeni_Kullanici_Adi.Length==0)
            //{
            //    epHataDedektoru.SetError(txt_yenikullanici_Degis, "Yeni kullanıcı adınızı girmediniz.");
            //}
            //else
            //{
            //    string sorgu_kullanici = "Select KullaniciAdi From yetkiler WHERE KullaniciAdi=@kullaniciadi";
            //    bool kullanici_var_mi = budak.Kayit_var_mi(sorgu_kullanici, Eski_kullanici_adi);
            //    if (kullanici_var_mi)
            //    {                    
            //        string sorgu_update_kulaniciadi_guncelle = "UPDATE yetkiler Set KullaniciAdi=@Yeni_kullaniciadi WHERE KullaniciAdi=@kullaniciadi";
            //        int duzeltilen_kulanici_adi = budak.Sorgu_Calistir(sorgu_update_kulaniciadi_guncelle, Yeni_Kullanici_Adi, Eski_kullanici_adi);

            //       if (duzeltilen_kulanici_adi>0)
            //        {
            //        bildirim= "Kullanıcı adınızı başarıyla güncellediniz." ;
            //        }

            //        //else
            //        //{
            //        //    bildirim = "Girdiğiniz kullanıcı adı ile eşleşen kayıt bulunamadı.\n"+
            //        //    "Lütfen bilgileriniz kontrol edip tekrar deneyiniz." ;
            //        //}
            //        //MessageBox.Show(bildirim);
            //    }
            //    else
            //    {
            //        bildirim = "Girdiğiniz kullanıcı adı ile eşleşen kayıt bulunamadı.\n" +
            //             "Lütfen bilgileriniz kontrol edip tekrar deneyiniz.";
            //    }
            //    MessageBox.Show(bildirim);

            //---------------------------------------------------------------------------------------------

         //  string sorgu_kullanici_id_bul = "SELECT YetkiID FROM yetkiler WHERE KullaniciAdi='" + txt_kullaniciadi_Degis.Text + "'";

         //DataTable kullanici_id_dt = budak.Sorgu_DataTable(sorgu_kullanici_id_bul);
         //int duzeltme_kullanici_id = Convert.ToInt32(kullanici_id_dt.Rows[0][0].ToString());

         ////int kullanici_bul = budak.Sorgu_Calistir(sorgu_kullanici_id_bul);

         //string sorgu_update_kullaniciadi_degis = "UPDATE yetkiler SET KullaniciAdi=@kullaniciadi WHERE YetkiID=@yetkiid";
         
         //int duzeltilen_kullanici_adi = budak.Sorgu_Calistir(sorgu_update_kullaniciadi_degis,
         //     txt_yenikullanici_Degis.Text, duzeltme_kullanici_id.ToString());

            
           
          

           
                 

            //////////////////////////////////////////////////////////// BURADAN AŞAĞISI AYRI DÜZENDE KOD \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            //if (budak.Kayit_var_mi("SELECT YetkiID FROM yetkiler where KullaniciAdi=@kulllaniciadi",Eski_kullanici_adi))
            //{
            //    int degistirilen_kullanici_adi = 0;

            //    if (secilen_kullanici>0)
            //    {
            //        if (MessageBox.Show("Kullanıcı adı değiştirilecektir. Değiştirmek istediğinize emin misiniz?","Kullanıcı Adı Değiştiriliyor", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            string sorgu_update_kadidegis = "UPDATE yetkiler SET KullaniciAdi=@kullaniciadi WHERE YetkiID=@yetkiid";
            //            degistirilen_kullanici_adi = budak.Sorgu_Calistir(sorgu_update_kadidegis, secilen_kullanici.ToString());

            //            string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES (@islem_ad,@islem_tip,@islem_kayit_id,@kisi,@tarih)";
            //            string log_tarihi = tarih_formatla(DateTime.Now.ToString());
            //            int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "giris", "update", secilen_kullanici.ToString(), uye_id.ToString(), log_tarihi);
            //        }
            //    }
            //    else
            //    {
            //        string sorgu_kullanici_id = "SELECT YetkiID FROM yetkiler WHERE KullaniciAdi=@kullaniciadi";

            //        DataTable dt = budak.Sorgu_DataTable(sorgu_kullanici_id, Eski_kullanici_adi);
            //        string kullanici_id = dt.Rows[0][0].ToString();
                    
            //    }
            //}


            //////////////////////////////////////////////////////////// BURADAN AŞAĞISI AYRI DÜZENDE KOD \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            
            ////burada kullanıcı adını db den seçmeye ve girilen txtbxa eşitlemeye çalıştım.ama devamında tıkandım =)
            //DataTable dt_uye_bilgileri = budak.Sorgu_DataTable("SELECT KullaniciAdi FROM yetkiler WHERE KullaniciAdi=@kullaniciadidegis",Eski_kullanici_adi);
        
            //if (Eski_kullanici_adi.Length==0)
            //{
            //    epHataDedektoru.SetError(txt_kullaniciadi_Degis, "Şuan ki kullanıcı adınızı girmediniz.");
            //}
            //else if (Yeni_Kullanici_Adi.Length==0)
            //{
            //    epHataDedektoru.SetError(txt_yenikullanici_Degis, "Yeni kullanıcı adınızı girmediniz.");
            //}
            //else
            //{
            //    string sorgu_kullanici = "Select KullaniciAdi From yetkiler WHERE KullaniciAdi=@kullaniciadi";
            //    bool kullanici_var_mi = budak.Kayit_var_mi(sorgu_kullanici, Eski_kullanici_adi);
            //    if (kullanici_var_mi)
            //    {                    
            //        string sorgu_update_kulaniciadi_guncelle = "UPDATE yetkiler Set KullaniciAdi=@Yeni_kullaniciadi WHERE KullaniciAdi=@kullaniciadi";
            //        int duzeltilen_kulanici_adi = budak.Sorgu_Calistir(sorgu_update_kulaniciadi_guncelle, Yeni_Kullanici_Adi, Eski_kullanici_adi);
                    
            //       if (duzeltilen_kulanici_adi>0)
            //        {
            //        bildirim= "Kullanıcı adınızı başarıyla güncellediniz." ;
            //        }

            //        //else
            //        //{
            //        //    bildirim = "Girdiğiniz kullanıcı adı ile eşleşen kayıt bulunamadı.\n"+
            //        //    "Lütfen bilgileriniz kontrol edip tekrar deneyiniz." ;
            //        //}
            //        //MessageBox.Show(bildirim);
            //    }
            //    else
            //    {
            //        bildirim = "Girdiğiniz kullanıcı adı ile eşleşen kayıt bulunamadı.\n" +
            //             "Lütfen bilgileriniz kontrol edip tekrar deneyiniz.";
            //    }
            //    MessageBox.Show(bildirim);
                
                

            }
            
        }

    }


