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

namespace TabControlDeneme
{
    public partial class frmIstekYapilanUrunler : Form
    {
        private string urun_cinsi;
        public int istek_id = 0;
        public frmIstekYapilanUrunler(string _urun_cinsi)
        {
            InitializeComponent();
            urun_cinsi = _urun_cinsi;
        }

        DAL budak;
        private void frmIstekYapilanUrunler_Load(object sender, EventArgs e)
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

            lblAciklama.Text = @"
                Giriş yaptığınız ürün, aşağıda listelenen ürünler arasında ise listeden 
                seçerek hangi ürünün istenmiş olduğunu belirtebilirsiniz. Eğer giriş yapılan ürün listede 
                bulunmuyorsa istek tablosunda değişiklik yapılmaması için ürünün listede bulunmadığını 
                belirtiniz.";

            #region Modeli seçilen ürüne ait yapıldığı tahmin edilen istekler tespit ediliyor.
            string sorgu_urunleri_listele = "SELECT istek_id,malzeme_turu,istek_urun_sayisi,istek_tarih," +
                "istek_aciklama FROM istek WHERE malzeme_turu LIKE @urun_cinsi OR istek_aciklama LIKE @urun_aciklama";
            DataTable tablo_urunleri_listele = budak.Sorgu_DataTable(sorgu_urunleri_listele, "%" + urun_cinsi + "%",
                "%" + urun_cinsi + "%");
            if (tablo_urunleri_listele.Rows.Count > 0)
            {
                dgv_istek_tahminler.DataSource = tablo_urunleri_listele;
            }
            else
            {
                MessageBox.Show("Giriş yapılan ürünün cinsi ile eşleşen bir istek tespit edilemedi.");
                btnIstekTamam.Enabled = false;
            }
            #endregion
        }

        private void btnIstekTamam_Click(object sender, EventArgs e)
        {
            istek_id = Convert.ToInt32(dgv_istek_tahminler.CurrentRow.Cells[0].Value.ToString());
            this.Hide();
        }

        private void btnIstekYapilmadi_Click(object sender, EventArgs e)
        {
            istek_id = 0;
            this.Hide();
        }
    }
}
