using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Hurdaya_Ayir : Form
    {
        public Form1 frm1;
        public Hurdaya_Ayir(Form1 _frm1)
        {

            InitializeComponent();
            frm1 = _frm1;
        }

        DAL budak;          

        private void Hurdaya_Ayir_Load(object sender, EventArgs e)
        {
            #region veritabanı bağlantısı

            //XmlDocument xml_belgesi = new XmlDocument();

            //xml_belgesi.Load(@"ayarlar\baglanti.xml");

            //string baglanti_bilgisayar = "";
            //string veritabanı_baglantisi = "";
            //string baglanti_ek_bilgiler = "";

            //XmlNodeList baglanti_taglari = xml_belgesi.SelectNodes("/baglanti");

            //foreach (XmlNode baglanti_tag in baglanti_taglari)
            //{
            //    baglanti_bilgisayar = baglanti_tag["Kaynak_Bilgisayar"].InnerText;
            //    veritabanı_baglantisi = baglanti_tag["Veritabanı_Bilgisi"].InnerText;
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

        private void btnHurdaya_Gonder_Click(object sender, EventArgs e)
        {
            epHataDedektoru.Clear();
            txtHurdaya_Ayir_Aciklama.Text = txtHurdaya_Ayir_Aciklama.Text.ToUpper();
            
            #region Hurdaya ayrılanları veritabanına kaydediyoruz.
            try
            {

                if (txtHurdaya_Ayir_Aciklama.Text.Trim().Length == 0)
                {
                    epHataDedektoru.SetError(txtHurdaya_Ayir_Aciklama, "Ürün açıklamasını girmediniz. İlgili yeri doldurarak tekrar deneyiniz.");
                }
                else if (frm1.envanter_secili_urun > 0) //ürün seçiliyse
                {                    
                    // hurdalar tablosuna kaydımız gerçekleşiyor.
                    string sorgu_insert_hurda_ayir = "INSERT INTO hurdalar (urun_id,hurdaya_ayrilma_tarihi,hurda_aciklama) VALUES(@urun_id,@ayrilma_tarih,@aciklama)";

                    string formatli_tarih = tarih_formatla(DateTime.Now.ToString());
                    int eklenen_hurda_kaydi = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_insert_hurda_ayir, frm1.envanter_secili_urun.ToString(), formatli_tarih, txtHurdaya_Ayir_Aciklama.Text.Trim());

                    //loglar tablosuna hurda kaydını yaptığımızı kaydediyoruz.
                    string sorgu_loglar_hurda_insert = "INSERT INTO loglar (islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES (@islemad,@islemtip,@islemkayit_id,@islemyapankisi,@islemtarih)";
                    string log_hurda_bugunun_tarih = tarih_formatla(DateTime.Now.ToString());
                    int sorgu_loglar_hurda_insert_id = budak.Sorgu_Calistir(sorgu_loglar_hurda_insert, "hurda", "insert", eklenen_hurda_kaydi.ToString(), frm1.uye_id.ToString(), log_hurda_bugunun_tarih);

                    string sorgu_cikis_kayit_id = "SELECT cikis_id FROM envanter WHERE envanter_id='" + frm1.envanter_secili_kayit_id.ToString() + "'";
                    string envanter_cikis_id = budak.Sorgu_Scalar(sorgu_cikis_kayit_id);
                    string sorgu_delete_envanter = "DELETE FROM envanter WHERE envanter_id=@id";
                    budak.Sorgu_Calistir(sorgu_delete_envanter, frm1.envanter_secili_kayit_id.ToString());
                    string sorgu_delete_cikis = "DELETE FROM cikislar WHERE id=@id";
                    budak.Sorgu_Calistir(sorgu_delete_cikis, envanter_cikis_id.ToString());

                    //if (eklenen_hurda_kaydi > 0) MessageBox.Show("Kayıt başarılı.");
                    if (eklenen_hurda_kaydi > 0)
                    {
                        if (MessageBox.Show("Kayıt başarılı.","HURDA KAYDI",MessageBoxButtons.OK)==DialogResult.OK)
                        {
                            this.Hide();                            
                        }
                    }
                }
                
            #endregion

            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }

        private string tarih_formatla(string tarih)
        {
            #region TARİH FORMATLA

            string[] dizi_tarih_full = tarih.Split(' ');
            string[] dizi_tarih = dizi_tarih_full[0].Split('.');
            string[] dizi_saat = dizi_tarih_full[1].Split(':');
            string Yil = dizi_tarih[2];
            string Ay = dizi_tarih[1];
            if (Ay.Length == 1) Ay = "0" + Ay;
            string Gun = dizi_tarih[0];
            if (Gun.Length == 1) Gun = "0" + Gun;
            string Saat = dizi_saat[0];
            string Dakika = dizi_saat[1];
            string Saniye = dizi_saat[2];
            string formatli_tarih = Yil + "" + Ay + "" + Gun;
            return formatli_tarih;

            #endregion
        }
    }
}
