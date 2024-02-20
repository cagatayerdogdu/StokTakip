using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;
using System.Data.SQLite;

namespace TabControlDeneme
{
    public class DAL
    {
        SQLiteConnection Baglanti = null;


        /// <summary>
        /// Veritabanı bağlantı bilgisini alarak SqlConnection nesnesini yapılandırır.
        /// </summary>
        /// <param name="Veritabani">Bağlanılacak veritabanının kodunu giriniz.</param>
        public DAL(string ConnectionString)
        {
            Baglanti = new SQLiteConnection(ConnectionString);
        }

        protected enum BaglantiDurumu
        {
            Aç,
            Kapat
        };


        /// <summary>
        /// Veritabanı ile bağlantı açar ve kapatır.
        /// </summary>
        /// <param name="durum">Bağlantı durumunu açma ve kapama işlemlerinden hangisinin yapılacağını seçiniz.</param>
        protected void BaglantiAcKapa(BaglantiDurumu durum)
        {
            switch (durum)
            {
                case BaglantiDurumu.Aç:
                    if (Baglanti.State != ConnectionState.Open)
                    {
                        Baglanti.Open();
                    }
                    break;
                case BaglantiDurumu.Kapat:
                    if (Baglanti.State != ConnectionState.Closed)
                    {
                        Baglanti.Close();
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Girilen sorguyu DataTable'a yükleyerek döndürür.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <returns>Fonksiyon, DataTable tipinde sonuç döndürür.</returns>
        public DataTable Sorgu_DataTable(string sorgu)
        {
            BaglantiAcKapa(BaglantiDurumu.Aç);
            DataTable Tablo = new DataTable();
            SQLiteDataAdapter Adaptor = new SQLiteDataAdapter(sorgu, Baglanti);
            Adaptor.Fill(Tablo);
            BaglantiAcKapa(BaglantiDurumu.Kapat);
            return Tablo;
        }

        /// <summary>
        /// Girilen sorguyu, sağlanan parametrelere göre işler ve DataTable'a yükleyerek döndürür.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <param name="parametreler">Sorgunun alacağı parametreleri, sorgudaki sırayla giriniz.</param>
        /// <returns>Fonksiyon, DataTable tipinde sonuç döndürür.</returns>
        public DataTable Sorgu_DataTable(string sorgu, params string[] parametreler)
        {
            //Sağlanan sorgunun içerdiği parametreler saptanıyor.
            string[] dizi_parametreler = parametreleri_bul(sorgu);
            BaglantiAcKapa(BaglantiDurumu.Aç);
            DataTable Tablo = new DataTable();
            SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
            //Sorgu için sağlanan parametreler, sorguya işleniyor.
            int sayac = 0;
            foreach (string parametre in parametreler)
            {
                Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                sayac++;
            }
            SQLiteDataAdapter Adaptor = new SQLiteDataAdapter(Komut);
            Adaptor.Fill(Tablo);
            BaglantiAcKapa(BaglantiDurumu.Kapat);
            return Tablo;
        }

        /// <summary>
        /// Girilen sorguları DataSet'e yükleyerek döndürür. Sorgular, aralarında boşluk bırakılarak tek bir string halinde sağlanmalıdır.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguları aralarında boşluk bırakarak tek bir string halinde giriniz.</param>
        /// <returns>Fonksiyon, DataSet tipinde sonuç döndürür.</returns>
        public DataSet Sorgu_DataSet(string sorgular)
        {
            BaglantiAcKapa(BaglantiDurumu.Aç);
            DataSet Tablolar = new DataSet();
            SQLiteDataAdapter Adaptor = new SQLiteDataAdapter(sorgular, Baglanti);
            Adaptor.Fill(Tablolar);
            BaglantiAcKapa(BaglantiDurumu.Kapat);
            return Tablolar;
        }

        /// <summary>
        /// Girilen sorguları DataSet'e yükleyerek döndürür. Arzulanan sayıda sorgu, ayrı ayrı parametreler olarak girilebilir.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguları string değişkenler olarak, ayrı parametreler halinde giriniz.</param>
        /// <returns>Fonksiyon, DataSet tipinde sonuç döndürür.</returns>
        public DataSet Sorgu_DataSet(params string[] sorgular)
        {
            string sorgu = ""; //Tüm sorguları, aralarında boşluk bırakarak tek sorgu halinde tutacak olan değişken.
            foreach (string alt_sorgu in sorgular)
            {
                sorgu += alt_sorgu + " ";
            }
            sorgu = sorgu.TrimEnd(' ');
            BaglantiAcKapa(BaglantiDurumu.Aç);
            DataSet Tablolar = new DataSet();
            SQLiteDataAdapter Adaptor = new SQLiteDataAdapter(sorgu, Baglanti);
            Adaptor.Fill(Tablolar);
            BaglantiAcKapa(BaglantiDurumu.Kapat);
            return Tablolar;
        }

        /// <summary>
        /// Girilen sorguları, sağlanan parametrelere göre işler ve DataSet'e yükleyerek döndürür. Sorguları, aralarında boşluk bırakarak, tek bir string değişken halinde giriniz.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguları, aralarında boşluk bırakarak, tek bir string değişken halinde giriniz.</param>
        /// <param name="parametreler">Sorguların alacağı parametreleri, sorgulardaki sırayla giriniz. Her parametreyi string tipte ayrı parametreler halinde giriniz.</param>
        /// <returns>Fonksiyon, DataSet tipinde sonuç döndürür.</returns>
        public DataSet Sorgu_DataSet(string sorgular, params string[] parametreler)
        {
            //Sağlanan sorgunun içerdiği parametreler saptanıyor.
            string[] dizi_parametreler = parametreleri_bul(sorgular);

            BaglantiAcKapa(BaglantiDurumu.Aç);
            DataSet Tablolar = new DataSet();
            SQLiteCommand Komut = new SQLiteCommand(sorgular, Baglanti);
            //Sorgu için sağlanan parametreler, sorguya işleniyor.
            int sayac = 0;
            foreach (string parametre in parametreler)
            {
                Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                sayac++;
            }
            SQLiteDataAdapter Adaptor = new SQLiteDataAdapter(Komut);
            Adaptor.Fill(Tablolar);
            BaglantiAcKapa(BaglantiDurumu.Kapat);
            return Tablolar;
        }

        /// <summary>
        /// Girilen sorguları, sağlanan parametrelere göre işler ve DataSet'e yükleyerek döndürür. Sorgular string tipte bir dizi olarak sağlanmalıdır.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguları dizi olarak giriniz.</param>
        /// <param name="parametreler">Sorguların alacağı parametreleri, sorgulardaki sırayla giriniz. Her parametreyi string tipte ayrı parametreler halinde giriniz.</param>
        /// <returns>Fonksiyon, DataSet tipinde sonuç döndürür.</returns>
        public DataSet Sorgu_DataSet(string[] sorgular, params string[] parametreler)
        {
            //Sağlanan sorgunun içerdiği parametreler saptanıyor.
            List<string> dizi_parametreler = new List<string>();
            string sorgu = "";
            foreach (string alt_sorgu in sorgular) //Her sorgu için döngü çevriliyor.
            {
                string[] dizi_sorgu_parametreleri;
                dizi_sorgu_parametreleri = parametreleri_bul(alt_sorgu);
                foreach (string alt_sorgu_parametresi in dizi_sorgu_parametreleri) //Sorgudaki parametreler tespit edilip diziye aktarılıyor.
                {
                    dizi_parametreler.Add(alt_sorgu_parametresi);
                }
                //Sorgular, tek bir string değişkenine dönüştürülüyor.
                sorgu += alt_sorgu + " ";
            }
            sorgu = sorgu.TrimEnd(' ');

            BaglantiAcKapa(BaglantiDurumu.Aç);
            DataSet Tablolar = new DataSet();
            SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
            //Sorgu için sağlanan parametreler, sorguya işleniyor.
            int sayac = 0;
            foreach (string parametre in parametreler)
            {
                Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                sayac++;
            }
            SQLiteDataAdapter Adaptor = new SQLiteDataAdapter(Komut);
            Adaptor.Fill(Tablolar);
            BaglantiAcKapa(BaglantiDurumu.Kapat);
            return Tablolar;
        }


        /// <summary>
        /// Girilen sorguyu çalıştırır ve sorgudan etkilenen satır sayısını döndürür. 
        /// INSERT, UPDATE ve DELETE sorguları için kullanışlıdır.
        /// Döndürülen sonuç 0'dan büyükse, sorgu başarıyla işlenmiş demektir.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <returns>Fonksiyon, int tipinde sonuç döndürür.</returns>
        public int Sorgu_Calistir(string sorgu)
        {
            int etkilenen_satir_sayisi;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
                etkilenen_satir_sayisi = Komut.ExecuteNonQuery(); //Sorgu çalıştırılıyor.
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch
            {
                etkilenen_satir_sayisi = 0;
            }
            return etkilenen_satir_sayisi;
        }

        /// <summary>
        /// Girilen sorguyu, sağlanan parametleri işleyerek çalıştırır ve sorgudan etkilenen satır sayısını döndürür. 
        /// INSERT, UPDATE ve DELETE sorguları için kullanışlıdır.
        /// Döndürülen sonuç 0'dan büyükse, sorgu başarıyla işlenmiş demektir.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <param name="parametreler">Sorgunun alacağı parametreleri, sorgudaki sırayla giriniz.</param>
        /// <returns>Fonksiyon, int tipinde sonuç döndürür.</returns>
        public int Sorgu_Calistir(string sorgu, params string[] parametreler)
        {
            //Sağlanan sorgunun içerdiği parametreler saptanıyor.
            string[] dizi_parametreler = parametreleri_bul(sorgu);
            int etkilenen_satir_sayisi;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
                //Sorgu için sağlanan parametreler, sorguya işleniyor.
                int sayac = 0;
                foreach (string parametre in parametreler)
                {
                    Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                    sayac++;
                }
                etkilenen_satir_sayisi = Komut.ExecuteNonQuery(); //Sorgu çalıştırılıyor.
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch
            {
                etkilenen_satir_sayisi = 0;
            }
            return etkilenen_satir_sayisi;
        }

        /// <summary>
        /// Girilen sorguyu çalıştırır ve eklenen kaydın id numarasını döndürür. 
        /// INSERT, UPDATE ve DELETE sorguları için kullanışlıdır.
        /// Döndürülen sonuç 0'dan büyükse, sorgu başarıyla işlenmiş demektir.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <returns>Fonksiyon, int tipinde sonuç döndürür.</returns>
        public int Sorgu_Calistir_Eklenen_Id_Dondur(string sorgu)
        {
            int eklenen_id = 0;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
                Komut.ExecuteNonQuery(); //Sorgu çalıştırılıyor.
                //Eklenen kaydın ID'si tespit ediliyor.
                SQLiteCommand Komut_eklenen = new SQLiteCommand("SELECT last_insert_rowid()", Baglanti);
                SQLiteDataReader Komut_okuyucu = Komut_eklenen.ExecuteReader();
                while (Komut_okuyucu.Read())
                {
                    eklenen_id = Convert.ToInt32(Komut_okuyucu[0].ToString());
                }
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch
            {
                eklenen_id = 0;
            }
            return eklenen_id;
        }

        /// <summary>
        /// Girilen sorguyu, sağlanan parametleri işleyerek çalıştırır ve eklenen kaydın id numarasını döndürür. 
        /// INSERT, UPDATE ve DELETE sorguları için kullanışlıdır.
        /// Döndürülen sonuç 0'dan büyükse, sorgu başarıyla işlenmiş demektir.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <param name="parametreler">Sorgunun alacağı parametreleri, sorgudaki sırayla giriniz.</param>
        /// <returns>Fonksiyon, int tipinde sonuç döndürür.</returns>
        public int Sorgu_Calistir_Eklenen_Id_Dondur(string sorgu, params string[] parametreler)
        {
            //Sağlanan sorgunun içerdiği parametreler saptanıyor.
            string[] dizi_parametreler = parametreleri_bul(sorgu);
            int eklenen_id = 0;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
                //Sorgu için sağlanan parametreler, sorguya işleniyor.
                int sayac = 0;
                foreach (string parametre in parametreler)
                {
                    Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                    sayac++;
                }
                Komut.ExecuteNonQuery(); //Sorgu çalıştırılıyor.
                //Eklenen kaydın ID'si tespit ediliyor.
                SQLiteCommand Komut_eklenen = new SQLiteCommand("SELECT last_insert_rowid()", Baglanti);
                SQLiteDataReader Komut_okuyucu = Komut_eklenen.ExecuteReader();
                while (Komut_okuyucu.Read())
                {
                    eklenen_id = Convert.ToInt32(Komut_okuyucu[0].ToString());
                }
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch (Exception Hata)
            {
                string HataString = Hata.ToString();
                MessageBox.Show(HataString);
                eklenen_id = 0;
            }
            return eklenen_id;
        }


        //Muhtelif fonksiyonlar

        /// <summary>
        /// Girilen sorguya uygun kayıt olup olmadığını konrol eder. Eğer sorgu geriye kayıt döndürdüyse true, sorgu ile eşleşen kayıt yoksa false değer döndürür.
        /// </summary>
        /// <param name="Sorgu">Kontrol edilecek sorguyu giriniz.</param>
        /// <returns>Fonksiyon, eğer sorgu geriye kayıt döndürdüyse true, sorgu ile eşleşen kayıt yoksa false değer döndürür..</returns>
        public bool Kayit_var_mi(string sorgu)
        {
            bool sonuc;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
                SQLiteDataReader Okuyucu = Komut.ExecuteReader(); //Select sorgusunun çıktısı alınıyor.
                sonuc = (Okuyucu.HasRows) ? true : false; //Sorgu geriye satır döndürmüşse true, aksi halde false yazıyoruz.
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch
            {
                sonuc = false;
            }
            return sonuc;
        }

        /// <summary>
        /// Girilen sorguya uygun kayıt olup olmadığını konrol eder. Eğer sorgu geriye kayıt döndürdüyse true, sorgu ile eşleşen kayıt yoksa false değer döndürür.
        /// </summary>
        /// <param name="Sorgu">Kontrol edilecek sorguyu giriniz.</param>
        /// <param name="parametreler">Sorgunun alacağı parametreleri, sorgudaki sırayla giriniz.</param>
        /// <returns>Fonksiyon, eğer sorgu geriye kayıt döndürdüyse true, sorgu ile eşleşen kayıt yoksa false değer döndürür..</returns>
        public bool Kayit_var_mi(string sorgu, params string[] parametreler)
        {
            bool sonuc;
            try
            {
                //Sağlanan sorgunun içerdiği parametreler saptanıyor.
                string[] dizi_parametreler = parametreleri_bul(sorgu);
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
                //Sorgu için sağlanan parametreler, sorguya işleniyor.
                int sayac = 0;
                foreach (string parametre in parametreler)
                {
                    Komut.Parameters.AddWithValue(dizi_parametreler[sayac], parametre);
                    sayac++;
                }
                SQLiteDataReader Okuyucu = Komut.ExecuteReader(); //Select sorgusunun çıktısı alınıyor.
                sonuc = (Okuyucu.HasRows) ? true : false; //Sorgu geriye satır döndürmüşse true, aksi halde false yazıyoruz.
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch
            {
                sonuc = false;
            }
            return sonuc;
        }


        /// <summary>
        /// Girilen sorguyu çalıştırır ve tespit edilen tablonun ilk satırının ilk sütununu döndürür.
        /// </summary>
        /// <param name="Sorgu">Çalıştırılacak sorguyu giriniz.</param>
        /// <returns>Fonksiyon, string tipinde sonuç döndürür.</returns>
        public string Sorgu_Scalar(string sorgu)
        {
            string cikti;
            try
            {
                BaglantiAcKapa(BaglantiDurumu.Aç);
                SQLiteCommand Komut = new SQLiteCommand(sorgu, Baglanti);
                cikti = Komut.ExecuteScalar().ToString(); //Sorgu çalıştırılıyor.
                BaglantiAcKapa(BaglantiDurumu.Kapat);
            }
            catch
            {
                cikti = "";
            }
            return cikti;
        }



        /// <summary>
        /// Sorgudaki parametreleri tespit eder ve bir dizi olarak geri döndürür.
        /// </summary>
        /// <param name="sorgu">Parametre içeren bir sorgu giriniz.</param>
        /// <returns>Bu fonksiyon, string tipinde bir dizi döndürür.</returns>
        private string[] parametreleri_bul(string sorgu)
        {
            List<string> liste = new List<string>(); //Parametreleri barındıracak olan dizi.
            string gecici_parametre; //Parametre dizisine eklenecek olan parametreyi taşıyacak olan string değişken.
            string[] gecici_dizi = sorgu.Split('@'); //Sorgunun parametrelere göre parçalanmış hali.
            //Sorguyu "@" karakterlerine göre böldükten sonra parametreleri teker teker tespit edip diziye yerleştiriyoruz.
            for (int i = 1; i < gecici_dizi.Count(); i++)
            {
                int alt_limit = 666; //En düşük indeks değerini saptayacak olan değişken.
                gecici_parametre = gecici_dizi[i]; //Dizinin, döngünün faal aşamasında işlenmekte olan elemanı.
                List<int> dizi_kelime_sonu = new List<int>(); //Parametre adının kaçıncı karakterde bittiğine dair tahminleri barındıracak olan dizi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf(' ')); //İlk boşluk karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf(',')); //İlk virgül karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('=')); //İlk eşittir karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('!')); //İlk ünlem işareti karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf(')')); //İlk parantez karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('+')); //İlk artı karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('>')); //İlk büyüktür karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('<')); //İlk küçüktür karakterinin indeksi.
                dizi_kelime_sonu.Add(gecici_parametre.IndexOf('\'')); //İlk tırnak karakterinin indeksi.
                //Karşılaştırılan karakterlerden en önce karşılaşılanı tespit ediliyor.
                foreach (int indeks in dizi_kelime_sonu)
                {
                    if (indeks > 0 && indeks < alt_limit)
                    {
                        alt_limit = indeks;
                    }
                }
                //Eğer karakterlerden birisi ile karşılaşılmışsa, o karaktere kadar olan kısım parametre olarak alınıyor.
                if (alt_limit > 0 && alt_limit != 666)
                {
                    gecici_parametre = "@" + gecici_parametre.Substring(0, alt_limit);
                }
                liste.Add(gecici_parametre);
            }
            string[] dizi = liste.ToArray();
            return dizi;
        }
    }
}
