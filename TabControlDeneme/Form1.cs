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
    public partial class Form1 : Form
    {
        public int uye_id;
        string yetki_duzeyi;
        public Form1(int _uye_id, string _yetki_duzeyi)
        {
            InitializeComponent();
            uye_id = _uye_id;
            yetki_duzeyi = _yetki_duzeyi;
        }

        #region GLOBAL DEĞİŞKENLER
        int secilen_urun = 0; //Önceden kaydedilen bir ürün seçildiğinde kullanılacak olan değişken. 
        int secilen_depo_id = 0; //Düzeltilmek istenen depo giriş kaydının id numarası.
        bool filtre_acik = false;
        public int secilen_cikis_kaydi = 0;
        int secilen_arizaya_giden_kaydi = 0;
        int secilen_arizadan_donen_kaydi = 0;
        public int envanter_secili_urun = 0;
        public int envanter_secili_kayit_id = 0;
        //public int hurda_secili_cikan_urun_id = 0;
        public int cikan_urun_id = 0;

        //DAL nesnemizi global olarak oluşturuyoruz.
        DAL budak;
        #endregion

        //Form açılır açılmaz ilk iş olarak, bağlantı ayarlarımızı içeren Xml dosyamızı
        //okuyarak, bu ayarlara göre DAL nesnemizi veritabanımıza bağlayacağız.
        private void Form1_Load(object sender, EventArgs e)
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
            catch(Exception hata) 
            {
                string problem = hata.ToString();
            }

            #endregion

            
            #region Rutin ilk açılış işlemleri
            //Kullanıcının yetki düzeyine göre ilgili butonlar görüntüleniyor.
            if (yetki_duzeyi == "Yonetici")
            {
                btnDuzelt.Visible = true;
                btnSil_Giris.Visible = true;
                btnDuzeltCikis.Visible = true;
                btnSil_Cikis.Visible = true;
                //menuStrip1.Visible = true;
                //menustripParolaDegistir.Visible = true;
                uyeIslemleriToolStripMenuItem.Visible = true;
                kullanıcıAdıDeğiştirToolStripMenuItem.Visible = true;
                
                btnIstek_duzelt.Visible = true;
                btnServisGonder_Duzelt.Visible = true;
                btnServisDönen_Duzelt.Visible = true;

            }
            else
            {
                tbyeni_stok_girisi.Location = new Point(0, 0);
                btnFiltrele.Location = new Point(170, 310);
                btnGiris_ExceleAktar.Location = new Point(170, 395);
                btnistek_excel_aktar.Location = new Point(310,335);
            }

            //Gün içinde yapılan giriş işlemleri listeleniyor.
            gunun_giris_kayitlarini_listele();

            //Gün içinde yapılan çıkış işlemleri listeleniyor.
            gunu_cikis_kayitlarini_listele();

            //DEPO İÇİN LİSTELEME
            tum_depo_giris_kayitlarini_listele();

            //ENVANTER İÇİN LİSTELEME
            envanter_tum_giris_kayitlarini_listele();

            //İSTEK LİSTESİ
            istek_dgv_doldur();

            //SERVİSE GİDENLER LİSTESİ
            dgv_ariza_servise_giden_doldur();

            //SERVİSTEN DÖNENLER LİSTESİ
            dgv_ariza_servisten_donen_doldur();

            //HURDAYA AYRILANLAR LİSTESİ
            hurdalar_tum_giris_kaydini_listele();
            #endregion
        }

        private void gunun_giris_kayitlarini_listele()
        {
            #region Günün Giriş Kayıtları
            //Gün içinde yapılan giriş işlemleri listeleniyor.                                   
            string sorgu_gunun_girisleri = "SELECT d.id,u.urun_ad,u.urun_marka,u.urun_model,u.urun_serial," +
                "d.urun_sayisi,d.giris_tarihi,u.urun_cinsi,d.garanti_bilgisi " +
                    ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                    "WHERE l.islem_ad='giris' AND l.islem_tip='insert' AND l.islem_kayit_id=d.id) AS KullaniciAdi " +
                "FROM urunler AS u JOIN depo AS d ON u.urun_id=d.urun_id " +
                "WHERE d.giris_tarihi>=@gunun_baslangici";
            //Günün başlangıç anı, tarih formatında tespit ediliyor.
            //string gunun_baslangici = DateTime.Now.ToString("yyyy-MM-dd");
            string gunun_baslangici = DateTime.Now.ToString("yyyyMMdd");
            ////gunun_baslangici += " 00:00"; //Başlangıç saati geceyarısına ayarlanıyor.
            //DataGridView, sorgu sonucunda dönen DataTable içeriği ile dolduruluyor.
            dgwStokGirisi.DataSource = budak.Sorgu_DataTable(sorgu_gunun_girisleri, gunun_baslangici);
            #endregion      

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                #region EKLE BUTONU
                //Hata dedektörünün (ErrorProvider) bulduğu hatalar temizleniyor.
                epHataDedektoru.Clear();
                //Girilen değerin sayı olup olmadığını kontrol edecek olan ifade oluşturuluyor.
                Regex sayi_kontrolu = new Regex("0*[1-9][0-9]*");

                buyukharf_giris_ekle();

                //Yeni ürün girişi için gerekli alanların doldurulup doldurulmadığı kontrol ediliyor.
                if (txtUrunAdi.Text.Trim().Length == 0) //Ürün adı girilmemişse işlem yapılmıyor.
                {
                    epHataDedektoru.SetError(txtUrunAdi, "Ürünün adını girmediniz.");
                }
                else if (txtUrunMarkasi.Text.Trim().Length == 0) //Ürün markası girilmemişse işlem yapılmıyor.
                {
                    epHataDedektoru.SetError(txtUrunMarkasi, "Ürünün markasını girmediniz.");
                }
                else if (txtUrunModeli.Text.Trim().Length == 0) //Ürün modeli girilmemişse işlem yapılmıyor.
                {
                    epHataDedektoru.SetError(txtUrunModeli, "Ürünün modelini girmediniz.");
                }
                else if (txtUrunSerial.Text.Trim().Length == 0) //Ürün barkod numarası girilmemişse işlem yapılmıyor.
                {
                    epHataDedektoru.SetError(txtUrunSerial, "Ürünün barkod numarasını girmediniz.");
                }
                else if (!sayi_kontrolu.IsMatch(txtUrunSerial.Text.Trim()) && txtUrunSerial.Text.ToUpper() != "YOK") //Ürün barkod numarası doğru formatta girilmemişse işlem yapılmıyor.
                {
                    epHataDedektoru.SetError(txtUrunSerial, "Ürünün barkod numarasını doğru formatta girmediniz. Sadece sayı girişi yapabilirsiniz.");
                }
                else if (txtUrunGirisAdet.Text.Trim().Length == 0) //Giriş yapan ürün sayısı girilmemişse işlem yapılmıyor.
                {
                    epHataDedektoru.SetError(txtUrunGirisAdet, "Giriş yapan ürün sayısını girmediniz.");
                }
                else if (!sayi_kontrolu.IsMatch(txtUrunGirisAdet.Text.Trim())) //Giriş yapan ürün sayısı doğru formatta girilmemişse işlem yapılmıyor.
                {
                    epHataDedektoru.SetError(txtUrunGirisAdet, "Giriş yapan ürün sayısını doğru formatta girmediniz. Sadece sayı girişi yapabilirsiniz.");
                }
                else if (txturun_cinsi.Text.Trim().Length == 0)
                {
                    epHataDedektoru.SetError(txturun_cinsi, "Ürün cinsini girmediniz.");
                }
                else //Tüm bilgiler doğru bir şekilde girilmişse yeni kayıt girişi yapılıyor.
                {
                    //Girilen ürün barkod numarasına veritabanında rastlanmamışsa (depoya girişi yapılacak
                    //ürün, daha önceden kaydedilmemişse) yeni ürün kaydı yapılıyor.
                    int eklenen_urun = 0;
                    if (secilen_urun == 0)
                    {
                        //Ekleyeceğimiz ürünün sorgusunu hazırlıyoruz.
                        string sorgu_insert_urun = "INSERT INTO urunler(urun_ad,urun_marka,urun_model,urun_serial,urun_cinsi) VALUES(@ad,@marka,@model,@serial,@cins)";
                        //Ürünü ekleyecek olan insert sorgusunu çalıştıracak olan metodu tetikliyoruz.
                        //Bu metod, bize eklenen ürünün id numarasını döndürecek.
                        eklenen_urun = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_insert_urun, txtUrunAdi.Text.Trim(), txtUrunMarkasi.Text.Trim(), txtUrunModeli.Text.Trim(), txtUrunSerial.Text.Trim(), txturun_cinsi.Text.Trim());

                        //Ürün kaydı işlemi günlüğe kaydediliyor.
                        string sorgu_log_urun_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                        string log_urun_tarih = tarih_formatla(DateTime.Now.ToString());
                        int sorgu_log_urun_insert_id = budak.Sorgu_Calistir(sorgu_log_urun_insert, "urun", "insert", eklenen_urun.ToString(), uye_id.ToString(), log_urun_tarih);
                    }
                    else //Ürünün barkod numarası veritabanında kayıtlıysa, o ürün seçiliyor.
                    {
                        eklenen_urun = secilen_urun;
                    }
                    //Eklediğimiz ürünün depo kaydını gerçekleştirecek olan sorguyu yazıyoruz.
                    string sorgu_insert_depo = "INSERT INTO depo(urun_id,urun_sayisi,giris_tarihi,garanti_bilgisi) VALUES(@urun_id,@urun_sayisi,@giris_tarihi,@garanti_bilgisi)";
                    //Seçilen tarihi, uygun formata sokuyoruz.
                    string formatli_tarih = tarih_formatla(dtUrunGirisTarihi.Value.ToString());
                    //Ürünün depoya girişini kaydediyoruz.
                    int eklenen_depo_kaydi = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_insert_depo, eklenen_urun.ToString(), txtUrunGirisAdet.Text.Trim(), formatli_tarih, txtGiris_Garanti.Text.Trim());

                    //Depo giriş kaydı, günlüğe kaydediliyor.
                    string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                    string bugunun_tarihi = tarih_formatla(DateTime.Now.ToString());
                    int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "giris", "insert", eklenen_depo_kaydi.ToString(), uye_id.ToString(), bugunun_tarihi);

                    // İSTEK FORMUNDA GİRİŞİ YAPILAN ÜRÜNE AİT BİR KAYIT VARSA, İSTEK SAYISI
                    // GİRİŞ YAPILAN ÜRÜN SAYISI KADAR DÜŞÜRÜLECEK.
                    string eklenen_urun_serial = txtUrunSerial.Text.Trim();
                    //Eklenen ürün için serial girilmişse istekler serial numarasına göre kontrol edilecek.
                    if (eklenen_urun_serial != "YOK" && budak.Kayit_var_mi("SELECT istek_id FROM istek where istek_urun_serial=@eklenen_urun_serial", eklenen_urun_serial.ToString()))
                    {

                        string sorgu_istek_listesi = "SELECT istek_id,istek_urun_sayisi FROM istek " +
                            "WHERE istek_urun_serial=@eklenen_urun_serial ORDER BY istek_tarih LIMIT 1";
                        DataTable dt_istekler = budak.Sorgu_DataTable(sorgu_istek_listesi, eklenen_urun_serial.ToString());
                        if (dt_istekler.Rows.Count > 0) //Eğer girişi yapılan ürüne ait istek yapılmışsa işlem yapılıyor.
                        {
                            if (MessageBox.Show(txtUrunAdi.Text.Trim() + " isimli ürün için daha önce istek yapılmış. Giriş " +
                                "yaptığınız ürün istenen ürünle aynı ise, istek listesinden ilgili isteklerin kaldırılmasını " +
                                "ister misiniz?", "Ürüne ait istek yapıldığı tespit edildi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                string istek_id = dt_istekler.Rows[0][0].ToString();
                                string istek_urun_sayisi = dt_istekler.Rows[0][1].ToString();
                                if (istek_urun_sayisi == "1") //İstenen ürün sayısı 1 ise istek kaydı siliniyor.
                                {
                                    budak.Sorgu_Calistir("DELETE FROM istek WHERE istek_id=@istek_id", istek_id);
                                }
                                else //İstenen ürün sayısı birden fazla ise istenen ürün sayısı düşürülüyor ve istenen üründen geldiği işaretleniyor.
                                {
                                    string sorgu_istek_guncelle = "UPDATE istek SET istek_urun_sayisi=istek_urun_sayisi-1," +
                                        "istek_urun_geldi=1 WHERE istek_id=@istek_id";
                                    budak.Sorgu_Calistir(sorgu_istek_guncelle, istek_id);
                                }
                            }
                        }
                    }
                    else //Ürün için serial girilmemişse istekler ürün cinsine göre kontrol edilecek.
                    {
                        frmIstekYapilanUrunler form_istek_tahmin = new frmIstekYapilanUrunler(txturun_cinsi.Text.Trim());
                        form_istek_tahmin.ShowDialog();
                        int secilen_istek_id = form_istek_tahmin.istek_id;
                        if (secilen_istek_id > 0)
                        {
                            string sorgu_istek_listesi = "SELECT istek_urun_sayisi FROM istek " +
                            "WHERE istek_id=@istek_id";
                            DataTable dt_istekler = budak.Sorgu_DataTable(sorgu_istek_listesi, secilen_istek_id.ToString());
                            if (dt_istekler.Rows.Count > 0) //Eğer girişi yapılan ürüne ait istek yapılmışsa işlem yapılıyor.
                            {
                                string istek_urun_sayisi = dt_istekler.Rows[0][0].ToString();
                                if (istek_urun_sayisi == "1") //İstenen ürün sayısı 1 ise istek kaydı siliniyor.
                                {
                                    budak.Sorgu_Calistir("DELETE FROM istek WHERE istek_id=@istek_id", secilen_istek_id.ToString());
                                }
                                else //İstenen ürün sayısı birden fazla ise istenen ürün sayısı düşürülüyor ve istenen üründen geldiği işaretleniyor.
                                {
                                    string sorgu_istek_guncelle = "UPDATE istek SET istek_urun_sayisi=istek_urun_sayisi-1," +
                                        "istek_urun_geldi=1 WHERE istek_id=@istek_id";
                                    budak.Sorgu_Calistir(sorgu_istek_guncelle, secilen_istek_id.ToString());
                                }
                            }
                        }
                    }

                    string bildirim = "";
                    if (eklenen_urun > 0 && eklenen_depo_kaydi > 0)
                    {
                        bildirim = "Ürün başarıyla eklenmiştir.";
                        //Gün içinde yapılan giriş işlemlerinin listelendiği DataGridView güncelleniyor.
                        gunun_giris_kayitlarini_listele();
                    }
                    else
                    {
                        bildirim = "Ürün eklenirken bir sorun oluştu.";
                    }
                    MessageBox.Show(bildirim);
                }
                // ürünler giriş yapıldıktan sonra sayfa yenilenmiş gibi textboxları temizliyoruz.
                txtUrunAdi.Text = "";
                txtUrunModeli.Text = "";
                txtUrunMarkasi.Text = "";
                txtUrunSerial.Text = "";
                txtGiris_Garanti.Text = "";
                txturun_cinsi.Text = "";
                #endregion
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }

        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            try
            {
                #region FİLTRE BUTONU
                //Girilen kriterler düzenleniyor.
                string filtre_urun_ad = txtUrunAdi.Text.Trim().ToString();
                string filtre_urun_marka = txtUrunMarkasi.Text.Trim().ToString();
                string filtre_urun_model = txtUrunModeli.Text.Trim().ToString();
                string filtre_urun_serial = txtUrunSerial.Text.Trim().ToString();
                string filtre_urun_sayisi = txtUrunGirisAdet.Text.Trim().ToString();
                string filtre_urun_cinsi = txturun_cinsi.Text.Trim().ToString();
                string filtre_tarih_ilk = tarih_formatla(dtUrunGirisTarihi.Value.ToString());
                string filtre_tarih_son = tarih_formatla(dtUrunGirisTarihi_son.Value.ToString());
                string filtre_garanti_bilgisi = txtGiris_Garanti.Text.Trim().ToString();

                //Girilen kriterlere göre filtre oluşturuluyor.
                string filtre_bilgisi = ""; //Sorguya eklenecek olan filtre koşulu.
                string tarih_filtresi = ""; //Sorguya eklenecek olan tarih kriterleri.
                List<string> sorgu_parametreleri_list = new List<string>(); //Sorguya eklenecek parametreler

                //Seçilen tarih kriterlerine göre arama yapılıyor.
                switch (cmbFiltreTarih.SelectedIndex)
                {
                    case 1:
                        tarih_filtresi = "d.giris_tarihi>@tarih AND ";
                        sorgu_parametreleri_list.Add(filtre_tarih_ilk);
                        break;
                    case 2:
                        tarih_filtresi = "d.giris_tarihi<@tarih AND ";
                        sorgu_parametreleri_list.Add(filtre_tarih_ilk);
                        break;
                    case 3:
                        tarih_filtresi = "d.giris_tarihi BETWEEN @tarih_ilk AND @tarih_son AND ";
                        sorgu_parametreleri_list.Add(filtre_tarih_ilk);
                        sorgu_parametreleri_list.Add(filtre_tarih_son);
                        break;
                    default:
                        tarih_filtresi = "";
                        break;
                }
                //Ürünün barkod numarası girilmişse diğer ürün bilgileri kontrol edilmeyecek.
                if (filtre_urun_serial.Length > 0)
                {
                    filtre_bilgisi += "u.urun_serial=@urun_serial AND ";
                    sorgu_parametreleri_list.Add(filtre_urun_serial);
                }
                else //Ürünün barkod numarası girilmemişse, girilen diğer ürün bilgileri dikkate alınıyor.
                {
                    //Ürün adı girilmişse ürün adına göre arama yapılıyor.
                    if (filtre_urun_ad.Length > 0)
                    {
                        filtre_bilgisi += "u.urun_ad=@urun_ad AND ";
                        sorgu_parametreleri_list.Add(filtre_urun_ad);
                    }
                    //Ürün markası girilmişse ürün markasına göre arama yapılıyor.
                    if (filtre_urun_marka.Length > 0)
                    {
                        filtre_bilgisi += "u.urun_marka=@urun_marka AND ";
                        sorgu_parametreleri_list.Add(filtre_urun_marka);
                    }
                    //Ürün modeli girilmişse ürün modeline göre arama yapılıyor.
                    if (filtre_urun_model.Length > 0)
                    {
                        filtre_bilgisi += "u.urun_model=@urun_model AND ";
                        sorgu_parametreleri_list.Add(filtre_urun_model);
                    }
                    if (filtre_urun_cinsi.Length > 0)
                    {
                        filtre_bilgisi += "u.urun_cinsi=@urun_cinsi AND ";
                        sorgu_parametreleri_list.Add(filtre_urun_cinsi);
                    }
                }
                //Giriş yapılan ürün adedi girilmişse ürün adedine göre arama yapılıyor.
                if (filtre_urun_sayisi.Length > 0)
                {
                    filtre_bilgisi += "d.urun_sayisi=@urun_sayisi AND ";
                    sorgu_parametreleri_list.Add(filtre_urun_sayisi);
                }
                //Garanti bilgisi girilmişse garanti bilgisine göre arama yapılıyor.
                if (filtre_garanti_bilgisi.Length > 0)
                {
                    filtre_bilgisi += "d.garanti_bilgisi=@garanti_bilgisi AND ";
                    sorgu_parametreleri_list.Add(filtre_garanti_bilgisi);
                }

                //Sorguya eklenecek parametreler, uygun şekilde formatlanıyor.
                filtre_bilgisi = (filtre_bilgisi.Length > 0) ? filtre_bilgisi.Remove(filtre_bilgisi.Length - 6) : "";
                string[] sorgu_parametreleri = sorgu_parametreleri_list.ToArray();
                string filtre_bilgileri = "";
                if (filtre_bilgisi.Length > 0 || tarih_filtresi.Length > 0)
                {
                    filtre_bilgileri = "WHERE " + tarih_filtresi + filtre_bilgisi;
                }
                //Oluşturulan filtreye göre giriş bilgilerini listeleyecek olan sorgu oluşturuluyor.  !!!!!!!!!!!virgüle dikkat sorgudaki
                string sorgu_giris_listesi = "SELECT d.id,u.urun_ad,u.urun_marka,u.urun_model,u.urun_serial,d.urun_sayisi," +
                    "d.giris_tarihi,u.urun_cinsi,d.garanti_bilgisi" +
                    ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " + "WHERE l.islem_ad='giris' AND l.islem_tip='insert' AND l.islem_kayit_id=u.urun_id) AS KullaniciAdi " + "FROM urunler AS u JOIN depo AS d ON u.urun_id=d.urun_id " +
                    filtre_bilgileri;

                DataTable filtre_tablo = budak.Sorgu_DataTable(sorgu_giris_listesi, sorgu_parametreleri);
                //Eğer arama kriterlerine uygun bir kayıt bulunamamışsa, kullanıcı bilgilendiriliyor.
                if (filtre_tablo.Rows.Count == 0)
                {
                    MessageBox.Show("Aradığınız kriterlere uygun sonuç bulunamadı.");
                }
                else //Aranan kriterlere uygun sonuç bulunmuşsa DataGridView'a aktarılıyor.
                {
                    dgwStokGirisi.DataSource = filtre_tablo;
                }

                // ürünler filtrelendikten sonra sayfa yenilenmiş gibi textboxları temizliyoruz.
                txtUrunAdi.Text = "";
                txtUrunModeli.Text = "";
                txtUrunMarkasi.Text = "";
                txtUrunSerial.Text = "";
                txtGiris_Garanti.Text = "";
                //txtUrunGirisAdet.Text = "";
                txturun_cinsi.Text = "";
                #endregion
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
            
        }

        private void btnDuzelt_Click(object sender, EventArgs e)
        {
            #region DÜZELT BUTONU
            
                buyukharf_giris_ekle();
                //Ürün bilgileri güncelleniyor.
                string sorgu_update_urun = "UPDATE urunler SET urun_ad=@ad,urun_marka=@marka,urun_model=@model,urun_cinsi=@cins WHERE urun_id=@id";
                int duzeltilen_urun_sayisi = budak.Sorgu_Calistir(sorgu_update_urun, txtUrunAdi.Text, txtUrunMarkasi.Text, txtUrunModeli.Text, txturun_cinsi.Text, secilen_urun.ToString());
                //Depo girişi bilgileri güncelleniyor.
                string sorgu_update_depo = "UPDATE depo SET urun_sayisi=@urun_sayisi,giris_tarihi=@giris_tarihi,garanti_bilgisi=@garanti_bilgisi WHERE id=@depo_id";
                //Seçilen tarih, veritabanı için uygun formata sokuluyor.
                string formatli_tarih = tarih_formatla(dtUrunGirisTarihi.Value.ToString());
                int duzeltilen_depo_kaydi_sayisi = budak.Sorgu_Calistir(sorgu_update_depo, txtGiris_Garanti.Text, formatli_tarih, secilen_depo_id.ToString());

                //İşlem günlüğe kaydediliyor.
                string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                string log_tarihi = tarih_formatla(DateTime.Now.ToString());
                int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "giris", "update", secilen_depo_id.ToString(), uye_id.ToString(), log_tarihi);

                string duzelt_bildirim = "";
                if (duzeltilen_urun_sayisi > 0)
                {
                    duzelt_bildirim = "Düzeltme işleminiz başarıyla gerçekleşti.";
                    //Gün içinde yapılan giriş işlemlerinin listelendiği DataGridView güncelleniyor.
                    gunun_giris_kayitlarini_listele();
                    // ürünler düzeltildikten sonra sayfa yenilenmiş gibi textboxları temizliyoruz.
                    txtUrunAdi.Text = "";
                    txtUrunModeli.Text = "";
                    txtUrunMarkasi.Text = "";
                    txtUrunSerial.Text = "";
                    txtGiris_Garanti.Text = "";
                    //txtUrunGirisAdet.Text = "";
                    txturun_cinsi.Text = "";
                    txtUrunSerial.ReadOnly = false;
                    secilen_urun = 0;
                }
                else
                {
                    duzelt_bildirim = "Düzeltme işleminiz gerçekleştirilemiyor. Tekrar deneyiniz.";
                }
            MessageBox.Show(duzelt_bildirim);
                                    
            #endregion
        }

        private void btnSil_Giris_Click(object sender, EventArgs e)
        {
            #region SİL BUTONU
            if (budak.Kayit_var_mi("SELECT urun_id FROM urunler WHERE urun_serial=@serial", txtUrunSerial.Text.Trim()))
            {
                int silinen_urun_depo = 0;
                //Eğer bir kayıt satırı seçilmişse, sadece seçilen satıra ait kayıt siliniyor.
                //Bir satır seçilmemişse, barkod numarası girilen ürüne ait bütün giriş kayıtları siliniyor.
                if (secilen_depo_id > 0)
                {
                    if (MessageBox.Show("Ürün giriş kaydı silinecektir. Silmek istediğinize emin misiniz?", "Kayıt Siliniyor", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //Seçilen kayıt satırı siliniyor.
                        string sorgu_delete_depo = "DELETE FROM depo WHERE id=@id";
                        silinen_urun_depo = budak.Sorgu_Calistir(sorgu_delete_depo, secilen_depo_id.ToString());


                        //İşlem günlüğe kaydediliyor.
                        string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                        string log_tarihi = tarih_formatla(DateTime.Now.ToString());
                        int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "giris", "delete", secilen_depo_id.ToString(), uye_id.ToString(), log_tarihi);
                    }
                }
                else
                {
                    string sorgu_urun_id = "SELECT urun_id FROM urunler WHERE urun_serial = @serial";
                    string sorgu_delete_depo = "DELETE FROM depo WHERE urun_id = @urunid";
                    //SELECT sorgusu ile ürün id'sini içeren kayıt, DataTable'a atılıyor. 
                    DataTable dt = budak.Sorgu_DataTable(sorgu_urun_id, txtUrunSerial.Text);
                    //Ürün id'sini tespit etmek için DataTable'ın ilk satırının ilk sütunu okunuyor.
                    string urun_id = dt.Rows[0][0].ToString();
                    //Öğrenilen ürün id'sine bağlı bütün kayıtlar, depo tablosundan siliniyor.
                    silinen_urun_depo = budak.Sorgu_Calistir(sorgu_delete_depo, urun_id);
                }

                //Kullanıcı, işlem sonucu hakkında bilgilendiriliyor.
                string sil_bildirim = "";
                if (silinen_urun_depo > 0)
                {
                    sil_bildirim = "Depo giriş işlem kaydı başarıyla silinmiştir.";
                    //Gün içinde yapılan giriş işlemlerinin listelendiği DataGridView güncelleniyor.
                    gunun_giris_kayitlarini_listele();
                }
                else
                {
                    sil_bildirim = "Depo giriş kaydı silinememiştir. Tekrar deneyiniz.";
                }
                MessageBox.Show(sil_bildirim);
                // ürün kaydı silindikten sonra sayfa yenilenmiş gibi textboxları temizliyoruz.
                txtUrunAdi.Text = "";
                txtUrunModeli.Text = "";
                txtUrunMarkasi.Text = "";
                txtUrunSerial.Text = "";
                txtGiris_Garanti.Text = "";
                //txtUrunGirisAdet.Text = "";
                txturun_cinsi.Text = "";
                txtUrunSerial.ReadOnly = false;
                secilen_urun = 0;
            }
            else
            {
                MessageBox.Show("Girdiğiniz barkod numarası ile kayıtlı ürün bulunamadı. Lütfen barkod numarasını kontrol edip tekrar deneyiniz.");
            }
            #endregion
        }

        private void txtUrunSerial_TextChanged(object sender, EventArgs e)
        {
            #region SERİAL CHANGED
            //Ürün bilgileri ile ilgili textBox'lar aktifleştiriliyor.
            txtUrunAdi.Items.IsReadOnly.ToString();
            //txtUrunAdi.ReadOnly = false;
            txtUrunMarkasi.ReadOnly = false;
            txtUrunModeli.ReadOnly = false;
            txturun_cinsi.ReadOnly = false;
            txtUrunGirisAdet.ReadOnly = true;
            txtUrunGirisAdet.Text = "1";

            //Veritabanında girilen barkod numarasına sahip bir ürün olup olmadığı kontrol ediliyor.
            string sorgu_urun = "SELECT urun_id,urun_ad,urun_marka,urun_model,urun_cinsi FROM urunler WHERE "+
                "urun_serial=@serial AND urun_serial!='YOK'";
            bool urun_kayitli = budak.Kayit_var_mi(sorgu_urun, txtUrunSerial.Text);
            //Eğer barkod numarası girilen ürün veritabanında kayıtlı ise, seçilen ürün olarak
            //ayarlanıyor ve ürün bilgilerine bağlı textBox'ların içerikleri dolduruluyor.
            if (urun_kayitli)
            {
                DataTable dt_urun_bilgileri = budak.Sorgu_DataTable(sorgu_urun, txtUrunSerial.Text);
                int secilen_urun_id = Convert.ToInt32(dt_urun_bilgileri.Rows[0][0]);
                string secilen_urun_ad = dt_urun_bilgileri.Rows[0][1].ToString();
                string secilen_urun_marka = dt_urun_bilgileri.Rows[0][2].ToString();
                string secilen_urun_model = dt_urun_bilgileri.Rows[0][3].ToString();
                string secilen_urun_cins = dt_urun_bilgileri.Rows[0][4].ToString();
                //Ürün bilgileri ile ilgili textBox'lar pasifleştiriliyor ve dolduruluyor.
                txtUrunAdi.Items.IsReadOnly.ToString();
                txtUrunMarkasi.ReadOnly = true;
                txtUrunModeli.ReadOnly = true;
                txturun_cinsi.ReadOnly = true;
                txtUrunAdi.Text = secilen_urun_ad;
                txtUrunMarkasi.Text = secilen_urun_marka;
                txtUrunModeli.Text = secilen_urun_model;
                txturun_cinsi.Text = secilen_urun_cins;
                //Seçilen ürünün id numarası, ilgili global değişkene atanıyor.
                secilen_urun = secilen_urun_id;
            }
            else //Yazılan barkod numarası ile uyuşan bir ürün kayıtlı değilse, seçilen ürün id'sinin numarasının atandığı değişkenin değeri sıfırlanıyor.
            {
                secilen_urun = 0;
                //Barkod numarası olarak "YOK" girilmişse ürün adedi textbox'ı aktifleştiriliyor.
                if (txtUrunSerial.Text.ToUpper() == "YOK")
                {
                    txtUrunGirisAdet.ReadOnly = false;
                }
            }
            #endregion
        }

        private void dgwStokGirisi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            #region DGW GİRİŞ ÇİFT TIKLAMA
            //Kaydet butonu pasifleştiriliyor.
            btnEkle.Enabled = false;
            //Seçilen satırdaki ürünün barkod numarası tespit ediliyor.
            string secilen_urun_serial = dgwStokGirisi.CurrentRow.Cells[4].Value.ToString();
            
            //Seçilen ürün girişi kaydının id numarası tespit ediliyor.
            secilen_depo_id = Convert.ToInt32(dgwStokGirisi.CurrentRow.Cells[0].Value);
            //Barkod numarası tespit edilen ürünün id numarası öğreniliyor.
            string sorgu_secilen_urun_id = "SELECT urun_id FROM urunler WHERE urun_serial=@serial";
            DataTable dt_urun_id = budak.Sorgu_DataTable(sorgu_secilen_urun_id, secilen_urun_serial);
            secilen_urun = Convert.ToInt32(dt_urun_id.Rows[0][0]);

            //Seçilen ürün ve ürün giriş kaydına ait bilgiler, ilgili textBox'lara yükleniyor.
            txtUrunAdi.Text = dgwStokGirisi.CurrentRow.Cells[1].Value.ToString();
            txtUrunMarkasi.Text = dgwStokGirisi.CurrentRow.Cells[2].Value.ToString();
            txtUrunModeli.Text = dgwStokGirisi.CurrentRow.Cells[3].Value.ToString();
            txtUrunSerial.Text = dgwStokGirisi.CurrentRow.Cells[4].Value.ToString();
            txtUrunGirisAdet.Text = dgwStokGirisi.CurrentRow.Cells[5].Value.ToString();
            //dtUrunGirisTarihi.Value = Convert.ToDateTime(dgwStokGirisi.CurrentRow.Cells[6].Value.ToString());
            txturun_cinsi.Text = dgwStokGirisi.CurrentRow.Cells[7].Value.ToString();
            txtGiris_Garanti.Text = dgwStokGirisi.CurrentRow.Cells[8].Value.ToString();

            //Ürün barkod numarası textBox'ı pasifleştiriliyor, diğer alanlar aktifleştiriliyor.
            txtUrunSerial.ReadOnly = true;
            txtUrunAdi.Items.IsReadOnly.ToString();            
            //txtUrunAdi.ReadOnly = false;
            txtUrunMarkasi.ReadOnly = false;
            txtUrunModeli.ReadOnly = false;
            txturun_cinsi.ReadOnly = false;
            #endregion
        }

        private void btnFiltrele_MouseEnter(object sender, EventArgs e)
        {
            #region MOUSE FİLTRELE ÜSTÜNE GELİNCE
            if (!filtre_acik)
            {
                filtre_acik = true;
                dtUrunGirisTarihi_son.Visible = true;
                cmbFiltreTarih.Visible = true;
                cmbFiltreTarih.SelectedIndex = 0;
            }
            #endregion
        }

        private void cmbFiltreTarih_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region COMBOBOX SEÇİLİ OLDUĞUNDAKİ TARİHLER
            if (cmbFiltreTarih.SelectedIndex == 3)
            {
                dtUrunGirisTarihi_son.Enabled = true;
            }
            else
            {
                dtUrunGirisTarihi_son.Enabled = false;
            }
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

        private void btnCikis_StokGiris_Click(object sender, EventArgs e)
        {
            #region ÇIKIŞ BUTONU
            if (MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Program kapatılıyor", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
            #endregion
        }

        private void tum_depo_giris_kayitlarini_listele()
        {
            #region DEPO TÜM GİRİŞ KAYIT LİSTELEME
            //DISTINCT komutu, aynı değere sahip hücreleri gruplar. Yani "DISTINCT urun_id", sadece farklı id'lere sahip ürünleri listeler.
            //SUM komutu, seçilen kolondaki değerlerin toplamını verir.
            //SELECT içinde SELECT kullanarak, sütun olarak belirli bir sorgunun sonucunu döndürebiliyoruz.
            //INNER JOIN kullanarak, sadece hem urunler, hem de depo tablosunda kayıtlı olan ürünleri listeliyoruz.
            string sorgu_tum_depo_girisleri = "SELECT DISTINCT u.urun_id,u.urun_ad,u.urun_marka,u.urun_model," +
                "u.urun_serial," +
                "(SELECT SUM(urun_sayisi) FROM depo WHERE urun_id=u.urun_id) AS urun_sayisi, " +
                "(SELECT SUM(nd.urun_sayisi) FROM depo AS nd INNER JOIN urunler AS nu ON nd.urun_id=nu.urun_id WHERE nu.urun_model=u.urun_model) AS model_toplam " +
                    ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                    "WHERE l.islem_ad='urun' AND l.islem_tip='insert' AND l.islem_kayit_id=u.urun_id) AS KullaniciAdi " +
                "FROM urunler AS u INNER JOIN depo AS d ON u.urun_id=d.urun_id";
            dgvDepo.DataSource = budak.Sorgu_DataTable(sorgu_tum_depo_girisleri);

            //(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi  WHERE l.islem_ad='giris' AND l.islem_tip='insert' AND l.islem_kayit_id=d.id)

            //Depodaki toplam ürün sayısı görüntüleniyor.
            string sorgu_depo_toplam_urun_sayisi = "SELECT SUM(urun_sayisi) FROM depo";
            DataTable dt_toplam_urun_sayisi = budak.Sorgu_DataTable(sorgu_depo_toplam_urun_sayisi);
            lblDepoToplamUrunSayisi.Text = dt_toplam_urun_sayisi.Rows[0][0].ToString();
            #endregion
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            #region ÇIKIŞ KAYDET BUTONU
            
            epHataDedektoru.Clear();
            Regex sayi_kontrolu = new Regex("0*[1-9][0-9]*");
            buyukharf_cikis_ekle();
            if (txtCikisYapilanUrunSerial.Text =="YOK")//16.11.2012 tarihinde çıkış adeti 1 den fazla olursa hata alıyoruz bu nedenle eklendi.
            {
                txtCikisYapilanAdet.ReadOnly = false;
            }
            
            if (txtTeslimatYapilanBirim.Text.Trim().Length == 0)
            {
                epHataDedektoru.SetError(txtTeslimatYapilanBirim, "Teslimat yapılan birimi girmediniz.");
            }
            else if (txtTeslimAlanAdi.Text.Trim().Length == 0)
            {
                epHataDedektoru.SetError(txtTeslimAlanAdi, "Teslim alan adını girmediniz.");
            }
            else if (txtCikisYapilanAdet.Text.Trim().Length == 0)
            {
                epHataDedektoru.SetError(txtCikisYapilanAdet, "Çıkış yapılan adet sayısını girmediniz.");
            }
            else if (txtCikisYapilanUrunSerial.Text.Trim().Length == 0)
            {
                epHataDedektoru.SetError(txtCikisYapilanUrunSerial, "Çıkış yapılan ürün serialini girmediniz.");
            }
            //else if (txtArizaTalepFormID.Text.Trim().Length==0)
            //{
            //    epHataDedektoru.SetError(txtArizaTalepFormID,"Arıza telep form id' yi girmediniz.");
            //}
            else if (txtcikis_aciklama.Text.Trim().Length == 0)
            {
                epHataDedektoru.SetError(txtcikis_aciklama, "Açıklama girmediniz.");
            }
            else if (!sayi_kontrolu.IsMatch(txtArizaTalepFormID.Text.Trim()))
            {
                epHataDedektoru.SetError(txtArizaTalepFormID, "Arıza talep form numarasını yanlış formatta girdiniz. Sadece sayı girebilirsiniz.");
            }
            else if (!sayi_kontrolu.IsMatch(txtCikisYapilanAdet.Text.Trim()))
            {
                epHataDedektoru.SetError(txtCikisYapilanAdet, "Çıkış yapılan adet sayısını yanlış formatta girdiniz. Sadece sayı girebilirsiniz.");
            }
            else
            {
                //Çıkış yapılan ürün sayısını farklı yerlerde kullanacağımızdan, bir değişkene atıyoruz.
                int cikis_yapan_urun_sayisi = Convert.ToInt32(txtCikisYapilanAdet.Text.Trim());
                int giris_tablosu_guncellendi = 0; //Giriş tablosunda gerekli düzenlemeler yapıldığını belirtecek olan bayrak değişkeni.
                //int cikan_urun_id = 0;
                #region Çıkışı yapılan ürünün ID numarası tespit ediliyor.

                string sorgu_urun_id = "SELECT urun_id FROM urunler WHERE urun_serial=@serial";
                DataTable cikan_urun_dt = budak.Sorgu_DataTable(sorgu_urun_id, txtCikisYapilanUrunSerial.Text.Trim());
                if (cikan_urun_dt.Rows.Count > 0)
                {
                    cikan_urun_id = Convert.ToInt32(cikan_urun_dt.Rows[0][0].ToString());
                }
                #endregion

                #region Çıkış kaydı ekleniyor
                string sorgu_insert_urun_cikis = "INSERT INTO cikislar(teslim_alan_birim, teslim_alan_kisi,urun_sayisi,cikis_urunserial,cikis_tarihi,ariza_takip_form_no,urun_id,cikis_aciklama)  " +
                    "VALUES(@birim,@kisi,@sayisi,@serial,@tarih,@form_no,@urun_id,@aciklama)";

                //int kaydedilen_cikis_urun = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_insert_urun_cikis, txtTeslimatYapilanBirim.Text.Trim(), txtTeslimAlanAdi.Text.Trim(), txtCikisYapilanAdet.Text.Trim(), txtCikisYapilanUrunSerial.Text.Trim(), txtArizaTalepFormID.Text.Trim());
                string formatli_cikis_tarihi = tarih_formatla(dtUrunCikisTarihi.Value.ToString());
                //MessageBox.Show(formatli_cikis_tarihi);
                int kaydedilen_cikis_urun = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_insert_urun_cikis, txtTeslimatYapilanBirim.Text.Trim(), txtTeslimAlanAdi.Text.Trim(), cikis_yapan_urun_sayisi.ToString(), txtCikisYapilanUrunSerial.Text.Trim(), formatli_cikis_tarihi, txtArizaTalepFormID.Text.Trim(), cikan_urun_id.ToString(), txtcikis_aciklama.Text.Trim());

                //İşlem günlüğe kaydediliyor.
                string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                string log_tarih = tarih_formatla(DateTime.Now.ToString());
                int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "cikis", "insert", kaydedilen_cikis_urun.ToString(), uye_id.ToString(), log_tarih);

                #region Envanter Kaydı oluşturuluyor.

                int cikan_urun_envanter_kayit_id = 0;
                string sorgu_cikan_urun_envanter_kayit_id = "SELECT id FROM cikislar WHERE cikis_urunserial=@cikisserial";
                DataTable cikan_urunenvanter_dt = budak.Sorgu_DataTable(sorgu_cikan_urun_envanter_kayit_id,txtCikisYapilanUrunSerial.Text.Trim());
                if (cikan_urunenvanter_dt.Rows.Count>0)
	            {
		            cikan_urun_envanter_kayit_id= Convert.ToInt32(cikan_urunenvanter_dt.Rows[0][0].ToString());
	            }

                string sorgu_insert_envanter = @"INSERT INTO envanter(urun_id,cikis_id) VALUES
(@urun,@cikis)";
                int eklenen_envanter_kaydi = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_insert_envanter,cikan_urun_id.ToString(),cikan_urun_envanter_kayit_id.ToString());


                #endregion

                #endregion

                #region Giriş tablosu güncelleniyor.
                //Çıkış yapılan ürün sayısı kaç adet ise, o sayıda döngü çevirerek giriş tablosunda
                //ilgili değişiklikleri yapacağız.
                for (int i = 1; i <= cikis_yapan_urun_sayisi; i++)
                {
                    //Çıkış tablosuna ilgili çıkış kaydı eklendi. Şimdi giriş tablosunda ilgili değişikliği
                    //yapmamız gerekiyor. İlk giriş kaydına ait satırı (en eski tarihte yapılan) tespit
                    //edeceğiz, ardından eğer bu kayıtta girişi yapılan ürün sayısı 1 ise satırı sileceğiz.
                    //Eğer birden fazla giriş yapılmışsa, sadece girişi yapılan ürün sayısı 1 düşürülecek.
                    string sorgu_ilk_giris_kaydi = "SELECT id,urun_sayisi FROM depo WHERE urun_id=@urun_id LIMIT 1";
                    DataTable ilk_giris_dt = budak.Sorgu_DataTable(sorgu_ilk_giris_kaydi, cikan_urun_id.ToString());
                    int giris_kayit_id = Convert.ToInt32(ilk_giris_dt.Rows[0][0].ToString());
                    int giris_yapan_urun_sayisi = Convert.ToInt32(ilk_giris_dt.Rows[0][1].ToString());
                    //Giriş yapılan ürün sayısına göre işlem yapıyoruz.
                    if (giris_yapan_urun_sayisi > 1) //Birden fazla ürün girişi yapılmışsa
                    {
                        //İlgili kayıt satırındaki ürün sayısı değerini 1 düşürüyoruz.
                        string sorgu_giris_guncelleme = "UPDATE depo SET urun_sayisi=urun_sayisi-1 WHERE id=@depo_id";
                        giris_tablosu_guncellendi = budak.Sorgu_Calistir(sorgu_giris_guncelleme, giris_kayit_id.ToString());
                    }
                    else //Sadece tek bir ürün girişi yapılmışsa
                    {
                        //İlgili kayıt satırını siliyoruz.
                        string sorgu_giris_silme = "DELETE FROM depo WHERE id=@depo_id";
                        giris_tablosu_guncellendi = budak.Sorgu_Calistir(sorgu_giris_silme, giris_kayit_id.ToString());
                    }
                }
                #endregion

                string bildirim = "";
                if (kaydedilen_cikis_urun > 0 && giris_tablosu_guncellendi > 0)
                {
                    bildirim = "Ürünün çıkışı başarıyla yapılmıştır.";
                    //Giriş başarılı olduğuna göre textBox'lar temizleniyor.
                    txtTeslimatYapilanBirim.Text = "";
                    txtTeslimAlanAdi.Text = "";
                    txtCikisYapilanUrunSerial.Text = "";
                    txtCikisYapilanAdet.Text = "";
                    txtArizaTalepFormID.Text = "";
                    txtcikis_aciklama.Text = "";
                    //DataGridView içeriği güncelleniyor.
                    gunu_cikis_kayitlarini_listele();
                }
                else
                {
                    bildirim = "Ürün çıkışı yapılamamıştır. Tekrar deneyiniz.";
                }
                MessageBox.Show(bildirim);

            }

            #endregion
        }

        private void gunu_cikis_kayitlarini_listele()
        {
            #region GÜNÜN ÇIKIŞ KAYITLARI

            string sorgu_gunun_cikislari = "SELECT c.id, c.teslim_alan_birim, c.teslim_alan_kisi, c.urun_sayisi,c.cikis_urunserial," +
                " c.cikis_tarihi, c.ariza_takip_form_no, c.cikis_aciklama" +
                    ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                    "WHERE l.islem_ad='cikis' AND l.islem_tip='insert' AND l.islem_kayit_id=c.id) AS KullaniciAdi " +
                "FROM cikislar AS c WHERE cikis_tarihi>=@gunun_baslangici";
            string gunun_baslangici = DateTime.Now.ToString("yyyyMMdd");
            //gunun_baslangici += " 00:00";
            dgwCikisIslemleri.DataSource = budak.Sorgu_DataTable(sorgu_gunun_cikislari, gunun_baslangici);                

            #endregion
        }

        private void txtCikisYapilanUrunSerial_TextChanged(object sender, EventArgs e)
        {
            #region ÇIKIŞ YAPILAN SERİAL CHANGED

            txtTeslimatYapilanBirim.ReadOnly = false;
            txtTeslimAlanAdi.ReadOnly = false;
            txtCikisYapilanAdet.ReadOnly = false;

            string sorgu_urun = "SELECT id, teslim_alan_birim, teslim_alan_kisi, urun_sayisi, FROM cikislar WHERE cikis_urunserial=@serial";
            bool urun_kayitli = budak.Kayit_var_mi(sorgu_urun, txtCikisYapilanUrunSerial.Text);

            if (urun_kayitli)
            {
                DataTable dt_urun_bilgisi = budak.Sorgu_DataTable(sorgu_urun, txtCikisYapilanUrunSerial.Text);
                int secilen_urun_bilgi = Convert.ToInt32(dt_urun_bilgisi.Rows[0][0].ToString());
                string secilen_cikis_birim = dt_urun_bilgisi.Rows[0][1].ToString();
                string secilen_cikis_kisi = dt_urun_bilgisi.Rows[0][2].ToString();
                string secilen_cikis_adet = dt_urun_bilgisi.Rows[0][3].ToString();

                txtTeslimatYapilanBirim.ReadOnly = true;
                txtTeslimAlanAdi.ReadOnly = true;
                txtCikisYapilanAdet.ReadOnly = true;
                txtTeslimatYapilanBirim.Text = secilen_cikis_birim;
                txtTeslimAlanAdi.Text = secilen_cikis_kisi;
                txtCikisYapilanAdet.Text = secilen_cikis_adet;

                secilen_urun = secilen_urun_bilgi;
            }

            #endregion
        }

        private void cmbCikis_Filtresi_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region ÇIKIŞ COMBOBOX TARİH FİLTRE

            if (cmbCikis_Filtresi.SelectedIndex == 3)
            {
                dturun_cikistarihi_son.Enabled = true;
            }
            else
            {
                dturun_cikistarihi_son.Enabled = false;
            }
            #endregion
        }

        private void btnFiltreleCikis_MouseEnter(object sender, EventArgs e)
        {
            #region ÇIKIŞ MOUSE ÜSTÜNE GELİNCE FİLTRE

            dturun_cikistarihi_son.Visible = true;
            cmbCikis_Filtresi.Visible = true;
            cmbCikis_Filtresi.SelectedItem = 0;

            #endregion
        }

        private void dgwCikisIslemleri_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            #region DGW ÇIKIŞ KAYIT ÇİFT TIKLAMA

            //Kaydet butonu pasifleştiriliyor.
            btnKaydet.Enabled = false;
            string secilen_cikis_serial = dgwCikisIslemleri.CurrentRow.Cells[4].Value.ToString();
            secilen_cikis_kaydi = Convert.ToInt32(dgwCikisIslemleri.CurrentRow.Cells[0].Value.ToString());
            
            //string sorgu_secilen_cikis_serial = "SELECT urun_id FROM urunler WHERE urun_serial=@serial";
            //DataTable dt_cikis_id = budak.Sorgu_DataTable(sorgu_secilen_cikis_serial, secilen_cikis_serial);
            //kaydedilen_cikis_urun = Convert.ToInt32(dt_cikis_id.Rows[0][0]);

            txtTeslimatYapilanBirim.Text = dgwCikisIslemleri.CurrentRow.Cells[1].Value.ToString();
            txtTeslimAlanAdi.Text = dgwCikisIslemleri.CurrentRow.Cells[2].Value.ToString();
            txtCikisYapilanAdet.Text = dgwCikisIslemleri.CurrentRow.Cells[3].Value.ToString();
            txtCikisYapilanUrunSerial.Text = dgwCikisIslemleri.CurrentRow.Cells[4].Value.ToString();
            dtUrunCikisTarihi.Value = Convert.ToDateTime(TariheCevir(dgwCikisIslemleri.CurrentRow.Cells[5].Value.ToString()));
            txtArizaTalepFormID.Text = dgwCikisIslemleri.CurrentRow.Cells[6].Value.ToString();
            txtcikis_aciklama.Text = dgwCikisIslemleri.CurrentRow.Cells[7].Value.ToString();
            txtCikisYapilanUrunSerial.ReadOnly = true;
            //txtTeslimatYapilanBirim.ReadOnly = false;
            //txtTeslimAlanAdi.ReadOnly = false;
            txtCikisYapilanAdet.ReadOnly = true;
            //txtArizaTalepFormID.ReadOnly = false;

            #endregion
        }

        private void btnFiltreleCikis_Click(object sender, EventArgs e)
        {
            #region ÇIKIŞ FİLTRE BUTONU
            try
            {
                string filtre_teslim_birim = txtTeslimatYapilanBirim.Text.Trim();
                string filtre_teslim_alankisi = txtTeslimAlanAdi.Text.Trim();
                string filtre_teslim_adet = txtCikisYapilanAdet.Text.Trim();
                string filtre_cikis_serial = txtCikisYapilanUrunSerial.Text.Trim();
                string filtre_ariza_form = txtArizaTalepFormID.Text.Trim();
                string filtre_tarih = tarih_formatla(dtUrunCikisTarihi.Value.ToString());
                string filtre_son_tarih = tarih_formatla(dturun_cikistarihi_son.Value.ToString());

                string filtre_bilgisi = "";
                string tarih_filtresi = "";

                List<string> sorgu_parametreleri_list = new List<string>();

                switch (cmbCikis_Filtresi.SelectedIndex)
                {
                    case 1:
                        tarih_filtresi = "c.cikis_tarihi>@tarih AND ";
                        sorgu_parametreleri_list.Add(filtre_tarih);
                        break;
                    case 2:
                        tarih_filtresi = "c.cikis_tarihi<@tarih AND ";
                        sorgu_parametreleri_list.Add(filtre_son_tarih);
                        break;
                    case 3:
                        tarih_filtresi = "c.cikis_tarihi BETWEEN @tarih_ilk AND @tarih_son AND ";
                        sorgu_parametreleri_list.Add(filtre_tarih);
                        //sorgu_parametreleri_list.Add(filtre_son_tarih);
                        break;
                    default:
                        tarih_filtresi = "";
                        break;
                }
                if (filtre_cikis_serial.Length > 0)
                {
                    filtre_bilgisi += " c.cikis_urunserial=@cikis_urunserial AND ";
                    sorgu_parametreleri_list.Add(filtre_cikis_serial);
                }
                if (filtre_teslim_birim.Length > 0)
                {
                    filtre_bilgisi += " c.teslim_alan_birim=@teslim_alan_birim AND ";
                    sorgu_parametreleri_list.Add(filtre_teslim_birim);
                }
                if (filtre_teslim_alankisi.Length > 0)
                {
                    filtre_bilgisi += " c.teslim_alan_kisi=@teslim_alan_kisi AND ";
                    sorgu_parametreleri_list.Add(filtre_teslim_alankisi);
                }
                if (filtre_teslim_adet.Length > 0)
                {
                    filtre_bilgisi += " c.urun_sayisi=@urun_sayisi AND ";
                    sorgu_parametreleri_list.Add(filtre_teslim_adet);
                }

                filtre_bilgisi = (filtre_bilgisi.Length > 0) ? filtre_bilgisi.Remove(filtre_bilgisi.Length - 5) : "";
                string[] sorgu_parametresi = sorgu_parametreleri_list.ToArray();

                if (filtre_bilgisi.Length > 0)
                {
                    //string sorgu_cikis_listesi = "SELECT c.teslim_alan_birim, c.teslim_alan_kisi, c.urun_sayisi, c.cikis_urunserial, c.cikis_tarihi, c.ariza_takip_form_no, " +
                    //    "c.cikis_aciklama FROM cikislar AS c WHERE " + tarih_filtresi + filtre_bilgisi;
                    string sorgu_cikis_listesi = "SELECT c.id, c.teslim_alan_birim, c.teslim_alan_kisi, c.urun_sayisi,c.cikis_urunserial," +
                    " c.cikis_tarihi, c.ariza_takip_form_no,c.cikis_aciklama" +
                        ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                        "WHERE l.islem_ad='cikis' AND l.islem_tip='insert' AND l.islem_kayit_id=c.id) AS KullaniciAdi " +
                    "FROM cikislar AS c WHERE " + tarih_filtresi + filtre_bilgisi;
                    //MessageBox.Show(sorgu_cikis_listesi);
                    DataTable filtre_tablo = budak.Sorgu_DataTable(sorgu_cikis_listesi, sorgu_parametresi);

                    if (filtre_tablo.Rows.Count == 0)
                    {
                        MessageBox.Show("Aradığınız kriterlere uygun sonuç bulunamadı.");
                    }
                    else
                    {
                        dgwCikisIslemleri.DataSource = filtre_tablo;
                    }
                }
                else
                {
                    //Herhangi bir arama kriteri belirtilmemişse tüm kayıtlar listeleniyor.
                    string sorgu_cikis_listesi = "SELECT c.id, c.teslim_alan_birim, c.teslim_alan_kisi, c.urun_sayisi,c.cikis_urunserial," +
                    " c.cikis_tarihi, c.ariza_takip_form_no,c.cikis_aciklama" +
                        ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                        "WHERE l.islem_ad='cikis' AND l.islem_tip='insert' AND l.islem_kayit_id=c.id) AS KullaniciAdi " +
                    "FROM cikislar AS c";
                    //MessageBox.Show(sorgu_cikis_listesi);
                    DataTable filtre_tablo = budak.Sorgu_DataTable(sorgu_cikis_listesi);
                    dgwCikisIslemleri.DataSource = filtre_tablo;
                }

                txtTeslimatYapilanBirim.Text = "";
                txtTeslimAlanAdi.Text = "";
                txtCikisYapilanAdet.Text = "";
                txtCikisYapilanUrunSerial.Text = "";
                txtArizaTalepFormID.Text = "";
                txtcikis_aciklama.Text = "";
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }


            #endregion

        private void btnDuzeltCikis_Click(object sender, EventArgs e)
        {
            #region ÇIKIŞ DÜZELT BUTON

            buyukharf_cikis_ekle();
            //Girilen ürün serial numarasının bağlı olduğu ürünün id numarası tespit ediliyor.
            string sorgu_urun_id_bul = "SELECT urun_id FROM urunler WHERE urun_serial=@serial";
            DataTable urun_id_dt = budak.Sorgu_DataTable(sorgu_urun_id_bul, txtCikisYapilanUrunSerial.Text.Trim());
            int duzeltme_urun_id = Convert.ToInt32(urun_id_dt.Rows[0][0]);
            string sorgu_update_urun_cikis = "UPDATE cikislar SET cikis_tarihi=@cikis_tarihi, " +
                "urun_sayisi=@uruncikis_sayisi, ariza_takip_form_no=@arizatakip," +
                "teslim_alan_kisi=@teslimalankisi, teslim_alan_birim=@teslimalanbirim, " +
                "cikis_urunserial=@cikis_serial,cikis_aciklama=@aciklama, urun_id=@urun_id WHERE id=@id";
            string formatli_tarih = tarih_formatla(dtUrunCikisTarihi.Value.ToString());
            int duzeltilen_urun_cikis = budak.Sorgu_Calistir(sorgu_update_urun_cikis,
                formatli_tarih, txtCikisYapilanAdet.Text, txtArizaTalepFormID.Text,
                txtTeslimAlanAdi.Text, txtTeslimatYapilanBirim.Text, txtCikisYapilanUrunSerial.Text, txtcikis_aciklama.Text,
                duzeltme_urun_id.ToString(), secilen_cikis_kaydi.ToString());

            //İşlem günlüğe kaydediliyor.
            string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
            string log_tarih = tarih_formatla(DateTime.Now.ToString());
            int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "cikis", "update", duzeltme_urun_id.ToString(), uye_id.ToString(), log_tarih);

            string duzelt_bildirim = "";

            if (duzeltilen_urun_cikis > 0)
            {
                duzelt_bildirim = "Düzeltme işleminiz başarıyla gerçekleşti.";
                gunu_cikis_kayitlarini_listele();
            }
            else
            {
                duzelt_bildirim = "Düzeltme işleminiz gerçekleştirilemiyor. Tekrar deneyiniz.";
            }
            MessageBox.Show(duzelt_bildirim);

            txtTeslimatYapilanBirim.Text = "";
            txtTeslimAlanAdi.Text = "";
            txtCikisYapilanAdet.Text = "";
            txtCikisYapilanUrunSerial.Text = "";
            txtArizaTalepFormID.Text = "";
            txtcikis_aciklama.Text = "";

            #endregion
        }

        private void btnSil_Cikis_Click(object sender, EventArgs e)
        {
            #region ÇIKIŞ SİL BUTON

            //string sorgu_delete_urun_cikis = " DELETE FROM cikislar WHERE cikis_urunserial = @silinecek_urun";
            //int silinen_urun_cikis = budak.Sorgu_Calistir(sorgu_delete_urun_cikis, txtCikisYapilanUrunSerial.Text);

            int silinen_urun_cikis = 0;
            if (secilen_cikis_kaydi > 0)
            {
                string sorgu_delete_urun_cikis = "DELETE FROM cikislar WHERE id=@id";
                silinen_urun_cikis = budak.Sorgu_Calistir(sorgu_delete_urun_cikis, secilen_cikis_kaydi.ToString());

                //İşlem günlüğe kaydediliyor.
                string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                string log_tarih = tarih_formatla(DateTime.Now.ToString());
                int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "cikis", "delete", secilen_cikis_kaydi.ToString(), uye_id.ToString(), log_tarih);

            }

            string sil_bildirim = "";

            if (silinen_urun_cikis > 0)
            {
                sil_bildirim = "Çıkış işlem kaydı başarıyla silinmiştir.";
                gunu_cikis_kayitlarini_listele();
                secilen_cikis_kaydi = 0; //Seçilen kayıt silindiğine göre, seçilen kayıt id'sinin tutan değişkenin değeri sıfırlanıyor.
                txtTeslimatYapilanBirim.Text = "";
                txtTeslimAlanAdi.Text = "";
                txtCikisYapilanAdet.Text = "";
                txtCikisYapilanUrunSerial.Text = "";
                txtArizaTalepFormID.Text = "";
                txtcikis_aciklama.Text = "";
                dtUrunCikisTarihi.Value = DateTime.Now;
            }
            else
            {
                sil_bildirim = "Çıkış işlem kaydı silinememiştir. Tekrar deneyiniz.";
            }
            MessageBox.Show(sil_bildirim);

            #endregion
        }

        public void    envanter_tum_giris_kayitlarini_listele()
        {
            #region ENVANTER TÜM GİRİŞ KAYITLARINI LİSTELE

            //string sorgu_tum_envanter_girisleri = "SELECT c.id,u.urun_id,c.teslim_alan_birim,u.urun_cinsi,u.urun_marka," +
            //    "u.urun_model,u.urun_serial,c.urun_sayisi,c.cikis_aciklama,c.cikis_tarihi" +
            //        ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
            //        "WHERE l.islem_ad='cikis' AND l.islem_tip='insert' AND l.islem_kayit_id=c.id) AS KullaniciAdi " +

            //    "FROM cikislar AS c JOIN urunler AS u ON c.urun_id = u.urun_id " +
            //    "ORDER BY c.teslim_alan_birim,u.urun_serial,c.cikis_tarihi DESC";
            //dgvEnvanter.DataSource = budak.Sorgu_DataTable(sorgu_tum_envanter_girisleri);

            //envanter_secili_urun = Convert.ToInt32(dgvEnvanter.Rows[0].Cells[1].Value);
            //envanter_secili_kayit_id = Convert.ToInt32(dgvEnvanter.Rows[0].Cells[0].Value);
            //////////-------------------------------------------//----------------------------------------------\\\\\\\\\
//            string sorgu_tum_envanter_girisleri = @"SELECT e.envanter_id,u.urun_id,c.teslim_alan_birim,u.urun_cinsi,u.urun_marka,
//            	u.urun_model,u.urun_serial,c.urun_sayisi,c.cikis_aciklama,c.cikis_tarihi
//            	,(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi
//            	WHERE l.islem_ad='cikis' AND l.islem_tip='insert' AND l.islem_kayit_id=e.envanter_id) AS KullaniciAdi
//            	
//                FROM envanter AS e INNER JOIN urunler AS u ON e.urun_id=u.urun_id INNER JOIN cikislar AS c ON e.cikis_id=c.id 
//            ORDER BY c.teslim_alan_birim,u.urun_serial,c.cikis_tarihi DESC";
//            dgvEnvanter.DataSource = budak.Sorgu_DataTable(sorgu_tum_envanter_girisleri);
//            envanter_secili_urun = Convert.ToInt32(dgvEnvanter.Rows[0].Cells[1].Value);
//            envanter_secili_kayit_id = Convert.ToInt32(dgvEnvanter.Rows[0].Cells[0].Value);


            //-------------------------- BU SEFER OLDUUUU =)))

            string sorgu_tum_envanter_girisleri = @"SELECT e.envanter_id,u.urun_id,c.teslim_alan_birim,u.urun_cinsi,u.urun_marka,
                u.urun_model,u.urun_serial,c.urun_sayisi,c.cikis_aciklama,c.cikis_tarihi
                    ,(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi 
                    WHERE l.islem_ad='cikis' AND l.islem_tip='insert' AND l.islem_kayit_id=e.envanter_id) AS KullaniciAdi 

                FROM envanter AS e JOIN urunler AS u ON e.urun_id = u.urun_id JOIN cikislar AS c ON e.cikis_id=c.id 
                ORDER BY c.teslim_alan_birim,u.urun_serial,c.cikis_tarihi DESC";
            dgvEnvanter.DataSource = budak.Sorgu_DataTable(sorgu_tum_envanter_girisleri);

            envanter_secili_urun = Convert.ToInt32(dgvEnvanter.Rows[0].Cells[1].Value);
            envanter_secili_kayit_id = Convert.ToInt32(dgvEnvanter.Rows[0].Cells[0].Value);

            #endregion
        }

        private void tbyeni_stok_girisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void btnCikis_StokCikis_Click(object sender, EventArgs e)
        {
            #region Çıkış, ÇIKIŞ BUTONU
            if (MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Program kapatılıyor", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
            #endregion
        }

        private void btnIstekYap_Click(object sender, EventArgs e)
        {
            #region İSTEK YAP BUTONU

            epHataDedektoru.Clear();
            Regex sayi_kontrolu = new Regex("0*[1-9][0-9]*");
            txtMalzemeTuru.Text = txtMalzemeTuru.Text.ToUpper();
            txtUrunIstekAciklama.Text = txtUrunIstekAciklama.Text.ToUpper();

            //İstenen ürün seriali ve açıklama kısımları zorunlu değil.
            if (txtMalzemeTuru.Text.Trim().Length == 0)
            {
                epHataDedektoru.SetError(txtMalzemeTuru, "İstenen ürünün cinsini girmediniz.");
            }
            else if (txtIstenenMiktarAdet.Text.Trim().Length == 0)
            {
                epHataDedektoru.SetError(txtIstenenMiktarAdet, "İstenen ürün miktarını girmediniz.");
            }
            else if (!sayi_kontrolu.IsMatch(txtIstenenMiktarAdet.Text.Trim()))
            {
                epHataDedektoru.SetError(txtIstenenMiktarAdet, "İstenen ürün miktarını doğru formatta girmediniz.");
            }
            else
            {
                //Ürün seriali ve istek açıklaması kısımları, girilmesi zorunlu alanlar değil.
                //Bu nedenle sorgumuzu, bu bilgilerin girilip girilmemesine göre oluşturuyoruz.
                string istek_eklenecek_sutunlar = "";
                string istek_eklenecek_veriler = "";
                List<string> istek_veriler_dizi = new List<string>();
                istek_veriler_dizi.Add(txtMalzemeTuru.Text.Trim());
                istek_veriler_dizi.Add(txtIstenenMiktarAdet.Text.Trim());
                istek_veriler_dizi.Add(tarih_formatla(dtIstekTarihi.Value.ToString()));
                if (txt_istek_urun_seri_no.Text.Trim().Length > 0)
                {
                    istek_eklenecek_sutunlar += ",istek_urun_serial";
                    istek_eklenecek_veriler += ",@istek_urun_serial";
                    istek_veriler_dizi.Add(txt_istek_urun_seri_no.Text.Trim());
                }
                if (txtUrunIstekAciklama.Text.Trim().Length > 0)
                {
                    istek_eklenecek_sutunlar += ",istek_aciklama";
                    istek_eklenecek_veriler += ",@istek_aciklama";
                    istek_veriler_dizi.Add(txtUrunIstekAciklama.Text.Trim());
                }

                string sorgu_istek_kayit = "INSERT INTO istek(malzeme_turu,istek_urun_sayisi,istek_tarih" +
                    istek_eklenecek_sutunlar + ") VALUES(@malzeme_turu,@istek_urun_sayisi,@istek_tarih" +
                    istek_eklenecek_veriler + ")";

                string[] istek_parametreleri = istek_veriler_dizi.ToArray();
                int eklenen_istek_kayit = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_istek_kayit, istek_parametreleri);
                string istek_bildirim = "";
                if (eklenen_istek_kayit > 0)
                {
                    istek_bildirim = "İstek kaydı başarıyla işlenmiştir.";
                    istek_dgv_doldur();

                    //İşlem günlüğe kaydediliyor.
                    string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                    string log_tarih = tarih_formatla(DateTime.Now.ToString());
                    int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "istek", "insert", eklenen_istek_kayit.ToString(), uye_id.ToString(), log_tarih);

                    txt_istek_urun_seri_no.Text = "";
                    txtMalzemeTuru.Text = "";
                    txtIstenenMiktarAdet.Text = "";
                    dtIstekTarihi.Value = DateTime.Now;
                    txtUrunIstekAciklama.Text = "";
                }
                else
                {
                    istek_bildirim = "İstek kaydı eklenirken bir sorun meydana geldi.";
                }
                MessageBox.Show(istek_bildirim);
            }

            #endregion
        }

        private void btnUrunIstekFiltrele_Click(object sender, EventArgs e)
        {
            #region İSTEK FİLTRE BUTONU

            string istek_urun_serial = txt_istek_urun_seri_no.Text.Trim();
            string istek_urun_cinsi = txtMalzemeTuru.Text.Trim();
            string istek_urun_sayisi = txtIstenenMiktarAdet.Text.Trim();
            string istek_aciklama = txtUrunIstekAciklama.Text.Trim();
            string istek_tarih_ilk = tarih_formatla(dtIstek_IlkTarih.Value.ToString());
            string istek_tarih_son = tarih_formatla(dtIstek_SonTarih.Value.ToString());

            string istek_filtre_kosullar = "";
            List<string> istek_filtre_dizi = new List<string>();
            if (istek_urun_serial.Length > 0)
            {
                istek_filtre_kosullar += "istek_urun_serial=@istek_urun_serial AND ";
                istek_filtre_dizi.Add(istek_urun_serial);
            }
            if (istek_urun_cinsi.Length > 0)
            {
                istek_filtre_kosullar += "malzeme_turu=@malzeme_turu AND ";
                istek_filtre_dizi.Add(istek_urun_cinsi);
            }
            if (istek_urun_sayisi.Length > 0)
            {
                istek_filtre_kosullar += "istek_urun_sayisi=@istek_urun_sayisi AND ";
                istek_filtre_dizi.Add(istek_urun_sayisi);
            }
            if (istek_aciklama.Length > 0)
            {
                istek_filtre_kosullar += "istek_aciklama=@istek_aciklama AND ";
                istek_filtre_dizi.Add(istek_aciklama);
            }
            if (!chkIstekTarihFiltre.Checked)
            {
                istek_filtre_kosullar += "istek_tarih BETWEEN @tarih_ilk AND @tarih_son AND ";
                istek_filtre_dizi.Add(istek_tarih_ilk);
                istek_filtre_dizi.Add(istek_tarih_son);
            }
            string[] istek_filtre_parametreleri = istek_filtre_dizi.ToArray();

            string string_istek_filtre = "";
            if (istek_filtre_kosullar.Length > 0)
            {
                istek_filtre_kosullar = istek_filtre_kosullar.Remove(istek_filtre_kosullar.Length - 5);
                string_istek_filtre = "WHERE " + istek_filtre_kosullar;
            }

            string sorgu_istek_filtreli = "SELECT istek_id,malzeme_turu,istek_urun_sayisi,istek_tarih,istek_aciklama," +
                "istek_urun_serial, (SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                " WHERE l.islem_ad='istek' AND l.islem_tip='insert' AND l.islem_kayit_id=istek_id) AS KullaniciAdi," +
                "istek_urun_geldi FROM istek " + string_istek_filtre;

            DataTable dt_istekler = budak.Sorgu_DataTable(sorgu_istek_filtreli, istek_filtre_parametreleri);
            dgwurun_istek.DataSource = dt_istekler;
            //İstek listesi DataGridView'ında bir bölümü gelmiş olan istek satırları kırmızı olarak görüntüleniyor.
            gelen_istekleri_kizart();
            #endregion
        }

        private void btnCikis_UrunIstek_Click(object sender, EventArgs e)
        {
            #region İSTEK ÇIKIŞ BUTONU

            if (MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Program kapatılıyor", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }

            #endregion
        }

        private void btnServiseGonder_Click(object sender, EventArgs e)
        {
            #region SERVİSE GÖNDER BUTONU

            epHataDedektoru.Clear();
            string firma_adi = txtFirmaAdi.Text.Trim().ToUpper();
            string urun_cinsi = txtUrunCinsi.Text.Trim().ToUpper();
            string urun_serial = txturun_seri_no.Text.Trim().ToUpper();
            string gonderilen_tarih = tarih_formatla(dtServisGonderilenTarih.Value.ToString());
            string urun_sorunu = txtUrunSorunu.Text.Trim().ToUpper();

            //Gerekli bilgilerin girilip girilmediği kontrol ediliyor.
            if (firma_adi.Length == 0)
            {
                epHataDedektoru.SetError(txtFirmaAdi, "Firma adını girmediniz.");
            }
            else if (urun_cinsi.Length == 0)
            {
                epHataDedektoru.SetError(txtUrunCinsi, "Ürün cinsini girmediniz.");
            }
            else if (urun_serial.Length == 0)
            {
                epHataDedektoru.SetError(txturun_seri_no, "Ürünün barkod numarasını girmediniz.");
            }
            else if (!budak.Kayit_var_mi("SELECT urun_id FROM urunler WHERE urun_serial=@serial", urun_serial))
            {
                epHataDedektoru.SetError(txturun_seri_no, "Girdiğiniz barkod numarası ile eşleşen bir ürün kayıtlı değil. Lütfen barkod numarasını kontrol ediniz.");
            }
            else if(urun_sorunu.Length == 0)
            {
                epHataDedektoru.SetError(txtUrunSorunu, "Ürünün servise gönderilmesinin sebebi olan arızayı belirtmediniz.");
            }
            else //Tüm kontrollerden başarıyla geçildiğine göre kayıt işlemi gerçekleştiriliyor.
            {
                string sorgu_servise_gonder = "INSERT INTO arizalar(firma_adi,urun_cinsi,ariza_urun_serial,"+
                    "gonderilen_tarih,urun_sorunu) VALUES(@firma_adi,@urun_cinsi,@ariza_urun_serial,"+
                    "@gonderilen_tarih,@urun_sorunu)";
                int eklenen_ariza_id = budak.Sorgu_Calistir_Eklenen_Id_Dondur(sorgu_servise_gonder, firma_adi, 
                    urun_cinsi,urun_serial, gonderilen_tarih, urun_sorunu);
                //Kullanıcı, işlem hakkında bilgilendiriliyor.
                if (eklenen_ariza_id > 0)
                {
                    //Ürün kaydı işlemi günlüğe kaydediliyor.
                    string sorgu_log_urun_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                    string log_urun_tarih = tarih_formatla(DateTime.Now.ToString());
                    int sorgu_log_urun_insert_id = budak.Sorgu_Calistir(sorgu_log_urun_insert, "ariza_servise_giden", "insert", eklenen_ariza_id.ToString(), uye_id.ToString(), log_urun_tarih);
                    
                    MessageBox.Show("Servis kaydı başarıyla işlenmiştir.");
                    //Kayıt yapıldığına göre TextBox'lar temizleniyor.
                    txtFirmaAdi.Text = "";
                    txtUrunCinsi.Text = "";
                    txturun_seri_no.Text = "";
                    dtServisGonderilenTarih.Value = DateTime.Now;
                    txtUrunSorunu.Text = "";

                    //DataGrdView içeriği güncelleniyor.
                    dgv_ariza_servise_giden_doldur();

                }
                else
                {
                    MessageBox.Show("Servis kaydı yapılırken bir sorun oluştu.\r\nLütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
                }
            }

            #endregion
        }

        private void btnServisGonder_Duzelt_Click(object sender, EventArgs e)
        {
            #region SERVİSE GÖNDERİM DÜZELTME BUTONU

            if (secilen_arizaya_giden_kaydi == 0)
            {
                MessageBox.Show("Lütfen düzeltmek istediğiniz servis kaydı satırını, çift tıklayarak seçiniz.");
            }
            else
            {
                epHataDedektoru.Clear();
                string firma_adi = txtFirmaAdi.Text.Trim().ToUpper();
                string urun_cinsi = txtUrunCinsi.Text.Trim().ToUpper();
                string urun_serial = txturun_seri_no.Text.Trim().ToUpper();
                string gonderilen_tarih = tarih_formatla(dtServisGonderilenTarih.Value.ToString());
                string urun_sorunu = txtUrunSorunu.Text.Trim().ToUpper();

                //Gerekli bilgilerin girilip girilmediği kontrol ediliyor.
                if (firma_adi.Length == 0)
                {
                    epHataDedektoru.SetError(txtFirmaAdi, "Firma adını girmediniz.");
                }
                else if (urun_cinsi.Length == 0)
                {
                    epHataDedektoru.SetError(txtUrunCinsi, "Ürün cinsini girmediniz.");
                }
                else if (urun_serial.Length == 0)
                {
                    epHataDedektoru.SetError(txturun_seri_no, "Ürünün barkod numarasını girmediniz.");
                }
                else if (!budak.Kayit_var_mi("SELECT urun_id FROM urunler WHERE urun_serial=@serial", urun_serial))
                {
                    epHataDedektoru.SetError(txturun_seri_no, "Girdiğiniz barkod numarası ile eşleşen bir ürün kayıtlı değil. Lütfen barkod numarasını kontrol ediniz.");
                }
                else if (urun_sorunu.Length == 0)
                {
                    epHataDedektoru.SetError(txtUrunSorunu, "Ürünün servise gönderilmesinin sebebi olan arızayı belirtmediniz.");
                }
                else //Tüm kontrollerden başarıyla geçildiğine göre güncelleme işlemi gerçekleştiriliyor.
                {
                    string sorgu_servise_giden_guncelle = "UPDATE arizalar SET firma_adi=@firma_adi," +
                        "urun_cinsi=@urun_cinsi,ariza_urun_serial=@ariza_urun_serial,"+
                        "gonderilen_tarih=@gonderilen_tarih,urun_sorunu=@urun_sorunu WHERE arizalar_id=@ariza_id";
                    int guncellenen_kayit_sayisi = budak.Sorgu_Calistir(sorgu_servise_giden_guncelle, firma_adi,
                        urun_cinsi, urun_serial, gonderilen_tarih, urun_sorunu,secilen_arizaya_giden_kaydi.ToString());
                    //Kullanıcı, işlem hakkında bilgilendiriliyor.
                    if (guncellenen_kayit_sayisi > 0)
                    {
                        //Güncelleme işlemi günlüğe kaydediliyor.
                        string sorgu_log_urun_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                        string log_urun_tarih = tarih_formatla(DateTime.Now.ToString());
                        int sorgu_log_urun_insert_id = budak.Sorgu_Calistir(sorgu_log_urun_insert, "ariza_servise_giden", "update", secilen_arizaya_giden_kaydi.ToString(), uye_id.ToString(), log_urun_tarih);

                        MessageBox.Show("Servis kaydı başarıyla güncellenmiştir.");
                        //Kayıt güncellendiğine göre TextBox'lar temizleniyor.
                        secilen_arizaya_giden_kaydi = 0;
                        txtFirmaAdi.Text = "";
                        txtUrunCinsi.Text = "";
                        txturun_seri_no.Text = "";
                        dtServisGonderilenTarih.Value = DateTime.Now;
                        txtUrunSorunu.Text = "";

                        //DataGridView içeriği güncelleniyor.
                        dgv_ariza_servise_giden_doldur();

                    }
                    else
                    {
                        MessageBox.Show("Servis kaydı yapılırken bir sorun oluştu.\r\nLütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
                    }
                }
            }

            #endregion
        }

        private void btnServistenGeriAl_Click(object sender, EventArgs e)
        {
            #region SERVİSTEN DÖNENLER BUTONU
            try
            {
                if (secilen_arizaya_giden_kaydi == 0)
                {
                    MessageBox.Show("Lütfen servisten dönen ürünü seçmek için tablodan ilgili kayıt satırına sağ tıklayınız.");
                }
                else
                {
                    epHataDedektoru.Clear();
                    string urun_cinsi = txtDonenUrunCinsi.Text.Trim().ToUpper();
                    string urun_serial = txtDonenUrunSeriNo.Text.Trim().ToUpper();
                    string alinan_tarih = tarih_formatla(dtServistenDonenlerTarih.Value.ToString());
                    string yapilan_islem = txtYapilanIslem.Text.Trim().ToUpper();
                    
                    //Gerekli bilgilerin girilip girilmediği kontrol ediliyor.
                    if (urun_cinsi.Length == 0)
                    {
                        epHataDedektoru.SetError(txtDonenUrunCinsi, "Ürün cinsini girmediniz.");
                    }
                    else if (urun_serial.Length == 0)
                    {
                        epHataDedektoru.SetError(txtDonenUrunSeriNo, "Ürünün barkod numarasını girmediniz.");
                    }
                    else if (!budak.Kayit_var_mi("SELECT urun_id FROM urunler WHERE urun_serial=@serial", urun_serial))
                    {
                        epHataDedektoru.SetError(txtDonenUrunSeriNo, "Girdiğiniz barkod numarası ile eşleşen bir ürün kayıtlı değil. Lütfen barkod numarasını kontrol ediniz.");
                    }
                    else if (yapilan_islem.Length == 0)
                    {
                        epHataDedektoru.SetError(txtYapilanIslem, "Ürünün servise gönderilmesinin sebebi olan arızayı belirtmediniz.");
                    }
                    else //Tüm kontrollerden başarıyla geçildiğine göre kayıt işlemi gerçekleştiriliyor.
                    {
                        string sorgu_servisten_donen = "UPDATE arizalar SET alinan_tarih=@alinan_tarih,yapilan_islem=@yapilan_islem " +
                            "WHERE arizalar_id=@ariza_id";
                        int eklenen_ariza_id = budak.Sorgu_Calistir(sorgu_servisten_donen, alinan_tarih, yapilan_islem, secilen_arizaya_giden_kaydi.ToString());
                    
                        //Kullanıcı, işlem hakkında bilgilendiriliyor.
                        if (eklenen_ariza_id > 0)
                        {
                            //Ürün kaydı işlemi günlüğe kaydediliyor.
                            string sorgu_log_urun_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                            string log_urun_tarih = tarih_formatla(DateTime.Now.ToString());
                            int sorgu_log_urun_insert_id = budak.Sorgu_Calistir(sorgu_log_urun_insert, "ariza_servisten_donen", "insert", eklenen_ariza_id.ToString(), uye_id.ToString(), log_urun_tarih);

                            #region SERVİSTEN DÖNÜNCE İLGİL FORMA GİTMESİ
                            string akilda_tut = "";
                            akilda_tut = txtDonenUrunSeriNo.Text;
                            MessageBox.Show("Servis kaydı başarıyla işlenmiştir.");
                            gerialimislemi gerial = new gerialimislemi();
                            gerial.ShowDialog();
                            if (gerial.secim  == "Giriş")
                            {
                                tbyeni_stok_girisi.SelectedIndex = 0;
                                txtUrunSerial.Text = akilda_tut;
                                txtUrunSerial_TextChanged(sender, e);
                            }
                            else
                            {
                                tbyeni_stok_girisi.SelectedIndex = 1;
                                txtCikisYapilanUrunSerial.Text = akilda_tut;
                                txtCikisYapilanUrunSerial_TextChanged(sender, e);
                            }
                            #endregion
                            //Kayıt yapıldığına göre TextBox'lar temizleniyor.
                            secilen_arizaya_giden_kaydi = 0;
                            txtDonenUrunCinsi.Text = "";
                            txtDonenUrunSeriNo.Text = "";
                            dtServistenDonenlerTarih.Value = DateTime.Now;
                            txtYapilanIslem.Text = "";

                            //DataGrdView içerikleri güncelleniyor.
                            dgv_ariza_servise_giden_doldur();
                            dgv_ariza_servisten_donen_doldur();

                        }
                        else
                        {
                            MessageBox.Show("Servis kaydı yapılırken bir sorun oluştu.\r\nLütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
                        }
                    }
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }

            #endregion

        }

        private void btnServisDönen_Duzelt_Click(object sender, EventArgs e)
        {
            #region SERVİSTEN DÖNENLER DÜZELTME BUTONU

            if (secilen_arizadan_donen_kaydi == 0)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz kaydı seçmek için tablodan ilgili kayıt satırına sağ tıklayınız.");
            }
            else
            {
                epHataDedektoru.Clear();
                string urun_cinsi = txtDonenUrunCinsi.Text.Trim().ToUpper();
                string urun_serial = txtDonenUrunSeriNo.Text.Trim().ToUpper();
                string alinan_tarih = tarih_formatla(dtServistenDonenlerTarih.Value.ToString());
                string yapilan_islem = txtYapilanIslem.Text.Trim().ToUpper();

                //Gerekli bilgilerin girilip girilmediği kontrol ediliyor.
                if (urun_cinsi.Length == 0)
                {
                    epHataDedektoru.SetError(txtDonenUrunCinsi, "Ürün cinsini girmediniz.");
                }
                else if (urun_serial.Length == 0)
                {
                    epHataDedektoru.SetError(txtDonenUrunSeriNo, "Ürünün barkod numarasını girmediniz.");
                }
                else if (!budak.Kayit_var_mi("SELECT urun_id FROM urunler WHERE urun_serial=@serial", urun_serial))
                {
                    epHataDedektoru.SetError(txtDonenUrunSeriNo, "Girdiğiniz barkod numarası ile eşleşen bir ürün kayıtlı değil. Lütfen barkod numarasını kontrol ediniz.");
                }
                else if (yapilan_islem.Length == 0)
                {
                    epHataDedektoru.SetError(txtYapilanIslem, "Ürünün servise gönderilmesinin sebebi olan arızayı belirtmediniz.");
                }
                else //Tüm kontrollerden başarıyla geçildiğine göre kayıt işlemi gerçekleştiriliyor.
                {
                    string sorgu_servisten_donen = "UPDATE arizalar SET alinan_tarih=@alinan_tarih,yapilan_islem=@yapilan_islem " +
                        "WHERE arizalar_id=@ariza_id";
                    int guncellenen_kayit_sayisi = budak.Sorgu_Calistir(sorgu_servisten_donen, alinan_tarih, yapilan_islem, secilen_arizadan_donen_kaydi.ToString());
                    //Kullanıcı, işlem hakkında bilgilendiriliyor.
                    if (guncellenen_kayit_sayisi > 0)
                    {
                        //Ürün kaydı işlemi günlüğe kaydediliyor.
                        string sorgu_log_urun_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                        string log_urun_tarih = tarih_formatla(DateTime.Now.ToString());
                        int sorgu_log_urun_insert_id = budak.Sorgu_Calistir(sorgu_log_urun_insert, "ariza_servisten_donen", "update", secilen_arizadan_donen_kaydi.ToString(), uye_id.ToString(), log_urun_tarih);

                        MessageBox.Show("Servis kaydı başarıyla güncellenmiştir.");
                        //Kayıt yapıldığına göre TextBox'lar temizleniyor.
                        secilen_arizaya_giden_kaydi = 0;
                        txtDonenUrunCinsi.Text = "";
                        txtDonenUrunSeriNo.Text = "";
                        dtServistenDonenlerTarih.Value = DateTime.Now;
                        txtYapilanIslem.Text = "";

                        //DataGrdView içerikleri güncelleniyor.
                        dgv_ariza_servise_giden_doldur();
                        dgv_ariza_servisten_donen_doldur();

                    }
                    else
                    {
                        MessageBox.Show("Servis kaydı güncellenirken bir sorun oluştu.\r\nLütfen bilgilerinizi kontrol edip tekrar deneyiniz.");
                    }
                }
            }

            #endregion
        }

        private void btnCikis_ServisAriza_Click(object sender, EventArgs e)
        {
            #region SERVİS ÇIKIŞ BUTONU

            if (MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Program kapatılıyor", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }

            #endregion
        }

        private void btnAra_Ara_Click(object sender, EventArgs e)
        {
             #region ÜRÜN DETAYI SEKMESİ ARA BUTONU
            txtAra_SerialNo.Text = txtAra_SerialNo.Text.ToUpper();
            txtAra_UrunCinsineGore.Text = txtAra_UrunCinsineGore.Text.ToUpper();
            string filtre_urun_serial = txtAra_SerialNo.Text.Trim().ToString();
            string filtre_urun_cinsi = txtAra_UrunCinsineGore.Text.Trim().ToString();
            string filtre_ilk_tarih = tarih_formatla(dt_ara_ilk_tarih.Value.ToString());
            string filtre_son_tarih = tarih_formatla(dt_ara_son_tarih.Value.ToString());

            //string deneme = txtAra_UrunCinsineGore.Text;
            //string deneme2 = txtAra_SerialNo.Text;
            //string bildirim = "İki arama seçeneğini bir arada girdiniz.\n\rSeçimlerinizi yeniden yapınız.";
            
            //if (deneme!="")
            //{
            //    MessageBox.Show(bildirim);
            //    txtAra_UrunCinsineGore.Text = "";
            //    txtAra_SerialNo.Text = "";
            //}
            //else if (deneme2!="")
            //{
            //    MessageBox.Show(bildirim);
            //    txtAra_SerialNo.Text = "";
            //    txtAra_UrunCinsineGore.Text = "";
            //}                   
            
            string ara_filtre_bilgisi = "";
            string ara_tarih_filtresi = "";
            List<string> sorgu_parametreleri_list = new List<string>();

            if (filtre_urun_serial.Length > 0)
            {
                ara_filtre_bilgisi += "u.urun_serial=@urun_serial AND ";
                sorgu_parametreleri_list.Add(filtre_urun_serial);
            }
            else
            {
                if (filtre_urun_cinsi.Length > 0)
                {
                    ara_filtre_bilgisi += "u.urun_cinsi=@urun_ad AND ";
                    sorgu_parametreleri_list.Add(filtre_urun_cinsi);
                }
            }

            ara_filtre_bilgisi = (ara_filtre_bilgisi.Length > 0) ? ara_filtre_bilgisi.Remove(ara_filtre_bilgisi.Length - 5) : "";
            string[] sorgu_parametreleri = sorgu_parametreleri_list.ToArray();
            string filtre_bilgileri = "";

            if (ara_filtre_bilgisi.Length > 0)
            {
                filtre_bilgileri = "WHERE " + ara_tarih_filtresi + ara_filtre_bilgisi;
            }
            string sorgu_aranan_liste = @"SELECT u.urun_id,u.urun_ad,u.urun_marka,u.urun_model,u.urun_serial,u.urun_cinsi,d.urun_sayisi,
            d.giris_tarihi,c.urun_sayisi AS cikis_sayisi,c.ariza_takip_form_no,c.teslim_alan_birim,c.teslim_alan_kisi,
            c.cikis_aciklama,a.firma_adi,a.gonderilen_tarih,a.urun_sorunu,a.alinan_tarih,a.yapilan_islem,
            h.hurdaya_ayrilma_tarihi,h.hurda_aciklama,
            (SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID=l.islem_yapan_kisi)
            AS KullaniciAdi
            FROM urunler AS u LEFT JOIN depo AS d ON u.urun_id=d.urun_id LEFT JOIN cikislar AS c ON u.urun_id=c.urun_id
            LEFT JOIN arizalar AS a ON u.urun_serial=a.ariza_urun_serial  LEFT JOIN hurdalar AS h ON
            u.urun_id=h.urun_id " + filtre_bilgileri + " GROUP BY u.urun_id ";
            DataTable ara_filtre_tablo = budak.Sorgu_DataTable(sorgu_aranan_liste, sorgu_parametreleri);

            dgwAra.DataSource = ara_filtre_tablo;
            //----------------------------------------------------------------------//////////////////////---------------------------------
            //string sorgu_urun_serial = "SELECT urun_serial FROM urunler";
            //dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_urun_serial);
            //string girilen_serial_ara = txtAra_SerialNo.Text.Trim();

            //if (girilen_serial_ara == budak.Kayit_var_mi("SELECT urun_id FROM urunler WHERE urun_serial=@serial"))
            //{               
            //    string sorgu_filtre_listele = "Select * FROM ara_sorgu, ara_sorgu2";
            //    dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_filtre_listele);
            //}----------------------------------------
            //string sorgu_urun_id = "SELECT urun_id FROM urunler WHERE urun_serial=@serial";
            //DataTable dt = budak.Sorgu_DataTable(sorgu_urun_id, txtAra_SerialNo.Text.Trim());
            //string urun_id = dt.Rows[0][0].ToString();
            //if (txtAra_SerialNo.Text==urun_id)
            //{
            //    string sorgu_filtre_listele = "Select * FROM ara_sorgu, ara_sorgu2";
            //    dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_filtre_listele);
            //}
            //-----------------------
            //string sorgu_serial_bul = "SELECT * FROM urunler WHERE urun_serial=@serial";
            //bool kayitli_serial = budak.Kayit_var_mi(sorgu_serial_bul, txtAra_SerialNo.Text);

            //if (kayitli_serial)
            //{
            //    string sorgu_filtre_listele = "Select * FROM ara_sorgu, ara_sorgu2";
            //    dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_filtre_listele);
            //}-------------------------
            //string sorgu_kayitli_urun_serial = "SELECT urun_serial FROM urunler WHERE urun_serial=@serial";
            //dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_kayitli_urun_serial,txtAra_SerialNo.Text);

            //if (budak.Kayit_var_mi(sorgu_kayitli_urun_serial))
            //{
            //    string sorgu_filtre_listele = "Select * FROM ara_sorgu, ara_sorgu2";
            //    dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_filtre_listele);
            //}-------------------------------

          //  string filtre_ara_urun_serial = txtAra_SerialNo.Text.Trim().ToString();
          //////  string filtre_bilgisi = "";
          //// // List<string> sorgu_parametre_list = new List<string>();

          //  string sorgu_kayitli_urun_serial = "SELECT urun_serial FROM urunler WHERE urun_serial=@serial";
          //  dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_kayitli_urun_serial, filtre_ara_urun_serial);

          //  if (filtre_ara_urun_serial == budak.Kayit_var_mi(sorgu_kayitli_urun_serial).ToString())
          //  {
          //      ////dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_kayitli_urun_serial, filtre_ara_urun_serial);
          //      //if (true)
          //      //{
          //      // string sorgu_filtre_listele = "Select * FROM ara_sorgu, ara_sorgu2";
          //      //dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_filtre_listele);
          //      //}
          //      MessageBox.Show("ürün seriali alındı" + filtre_ara_urun_serial);
          //  }
          //  else { MessageBox.Show("kayıt eşleşmedi"); }
            //------------------------------------
           
            //string filtre_ara_urun_serial = txtAra_SerialNo.Text.Trim().ToString();
            //string sorgu_kayitli_urun_serial = "select distinct urun_serial from ara_sorgu where urun_serial=@serial";
            ////////dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_kayitli_urun_serial, filtre_ara_urun_serial);
            //int serial_bul=budak.Sorgu_Calistir(sorgu_kayitli_urun_serial,filtre_ara_urun_serial);
            //if (serial_bul>0)
            //{
            //    string sorgu_serial_getir = "SELECT DISTINCT * from ara_sorgu, ara_sorgu2";
            //    dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_serial_getir);
            //}
            //-------------------------------------------------------
//            string sorgu_ara_listele = @"select u.urun_id,u.urun_ad,u.urun_marka,u.urun_model,u.urun_serial,u.urun_cinsi,d.urun_sayisi,
//                    d.giris_tarihi,c.urun_sayisi,c.ariza_takip_form_no,c.teslim_alan_birim,c.teslim_alan_kisi,
//                    c.cikis_aciklama,a.firma_adi,a.gonderilen_tarih,a.urun_sorunu,a.alinan_tarih,a.yapilan_islem,
//                    h.hurdaya_ayrilma_tarihi,h.hurda_aciklama,
//                    (SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID=l.islem_yapan_kisi)
//                    AS KullaniciAdi
//                    FROM urunler AS u JOIN depo AS d ON u.urun_id=d.urun_id JOIN cikislar AS c ON u.urun_id=c.id
//                    JOIN arizalar AS a ON u.urun_serial=a.ariza_urun_serial  JOIN hurdalar AS h ON
//                    u.urun_id=h.urun_id";
//            dgwAra.DataSource = budak.Sorgu_DataTable(sorgu_ara_listele);

            #endregion
        }      

        private void btnAra_Cikis_Click(object sender, EventArgs e)
        {
            #region ARA ÇIKIŞ BUTONU

            if (MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Program kapatılıyor", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }

            #endregion
        }

        private void lnklblAra_TarihlereGore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            #region ARA TARİHLER AKTİF

            grbAra_Tarihler.Visible = true;

            #endregion
        }

        private void lnklblAra_TarihlereGore_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            #region ARA TARİHLER PASİF

            grbAra_Tarihler.Visible = false;

            #endregion
        }

        private void menuSecilenUrunuSifirlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Giriş formu temizleniyor
            secilen_urun = 0;
            txtUrunAdi.Text = "";
            txtUrunMarkasi.Text = "";
            txtUrunModeli.Text = "";
            txtUrunSerial.Text = "";
            txturun_cinsi.Text = "";
            //txtUrunGirisAdet.Text = ""; SIFIRLADIĞIMIZ DA READONLY TRUE OLDUĞU İÇİN İŞLEM YAPILAMIYOR.
            txtGiris_Garanti.Text = "";
            dtUrunGirisTarihi.Value = DateTime.Now;
            txtUrunAdi.Items.IsReadOnly.ToString();
            //txtUrunAdi.ReadOnly = false;
            txtUrunMarkasi.ReadOnly = false;
            txtUrunModeli.ReadOnly = false;
            txtUrunSerial.ReadOnly = false;

            btnEkle.Enabled = true;
            btnDuzelt.Enabled = true;
            btnFiltrele.Enabled = true;
            btnSil_Giris.Enabled = true;
            //chkSil_GirisAktif.Checked = false;
            #endregion
        }

        #region Sağ tık menüsü düzenlemeleri
        private void dgwStokGirisi_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuSecilenUrunuSifirlaToolStripMenuItem.Visible = true;
                menuSecilenCikisiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenArizaServistenDonduToolStripMenuItem.Visible = false;
                menuSecilenUrunuHurdayaAyirToolStripMenuItem.Visible = false;
                menuSecilenIstekKaydiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenUrununCikisiniYapToolStripMenuItem.Visible = false;
                menuSag.Show(dgwStokGirisi.PointToScreen(e.Location));
            }
        }

        private void dgwCikisIslemleri_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuSecilenUrunuSifirlaToolStripMenuItem.Visible = false;
                menuSecilenCikisiSifirlaToolStripMenuItem.Visible = true;
                menuSecilenArizaServistenDonduToolStripMenuItem.Visible = false;
                menuSecilenUrunuHurdayaAyirToolStripMenuItem.Visible = false;
                menuSecilenIstekKaydiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenUrununCikisiniYapToolStripMenuItem.Visible = false;
                menuSag.Show(dgwCikisIslemleri.PointToScreen(e.Location));
            }
        }

        private void dgwServiseGonderilen_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuSecilenUrunuSifirlaToolStripMenuItem.Visible = false;
                menuSecilenCikisiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenArizaServistenDonduToolStripMenuItem.Visible = true;
                menuSecilenUrunuHurdayaAyirToolStripMenuItem.Visible = false;
                menuSecilenIstekKaydiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenUrununCikisiniYapToolStripMenuItem.Visible = false;
                menuSag.Show(dgwServiseGonderilen.PointToScreen(e.Location));
            }
        }

        private void dgvEnvanter_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuSecilenUrunuSifirlaToolStripMenuItem.Visible = false;
                menuSecilenCikisiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenArizaServistenDonduToolStripMenuItem.Visible = false;
                menuSecilenUrunuHurdayaAyirToolStripMenuItem.Visible = true;
                menuSecilenIstekKaydiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenUrununCikisiniYapToolStripMenuItem.Visible = false;
                menuSag.Show(dgvEnvanter.PointToScreen(e.Location));
            }
        }

        private void dgwurun_istek_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuSecilenArizaServistenDonduToolStripMenuItem.Visible = false;
                menuSecilenCikisiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenUrunuHurdayaAyirToolStripMenuItem.Visible = false;
                menuSecilenUrunuSifirlaToolStripMenuItem.Visible = false;
                menuSecilenIstekKaydiSifirlaToolStripMenuItem.Visible = true;
                menuSecilenIstekKaydiSifirlaToolStripMenuItem.Visible = true;
                menuSecilenUrununCikisiniYapToolStripMenuItem.Visible = false;
                menuSag.Show(dgwurun_istek.PointToScreen(e.Location));
            }
        }

        #endregion

        private void menuSecilenCikisiSifirlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Çıkış Sağ Click işlemleri
            txtTeslimatYapilanBirim.Text = "";
            txtTeslimAlanAdi.Text = "";
            txtCikisYapilanUrunSerial.Text = "";
            txtCikisYapilanAdet.Text = "1";
            txtCikisYapilanAdet.ReadOnly = true; //16.11.2012 tarihinde eklendi.
            txtArizaTalepFormID.Text = "";
            txtcikis_aciklama.Text = "";
            dtUrunCikisTarihi.Value = DateTime.Now;
            btnKaydet.Enabled = true;
            #endregion
        }

        private void buyukharf_giris_ekle()
        {
            #region Giriş Ekle BÜYÜK HARF

            txtUrunAdi.Text = txtUrunAdi.Text.ToUpper();
            txtUrunMarkasi.Text = txtUrunMarkasi.Text.ToUpper();
            txtUrunModeli.Text = txtUrunModeli.Text.ToUpper();
            txturun_cinsi.Text = txturun_cinsi.Text.ToUpper();
            txtGiris_Garanti.Text = txtGiris_Garanti.Text.ToUpper();
            txtUrunSerial.Text = txtUrunSerial.Text.ToUpper();

            #endregion
        }

        private void buyukharf_cikis_ekle()
        {
            #region Çıkış Ekle BÜYÜK HARF

            txtTeslimatYapilanBirim.Text = txtTeslimatYapilanBirim.Text.ToUpper();
            txtTeslimAlanAdi.Text = txtTeslimAlanAdi.Text.ToUpper();
            txtcikis_aciklama.Text = txtcikis_aciklama.Text.ToUpper();
            txtCikisYapilanUrunSerial.Text = txtCikisYapilanUrunSerial.Text.ToUpper();
            #endregion
        }

        private void uyeIslemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region ÜYE YÖNETİMİ AÇILIŞ PENCERESİ

            uye_yonetimi uyeyonetimi = new uye_yonetimi();
            uyeyonetimi.Show();

            #endregion
        }

        private void istek_dgv_doldur()
        {
            #region İSTEK LİSTESİ
            //İstek listesi DataGridVew'ına ilgili veriler yükleniyor.
            string sorgu_istek_listesi = "SELECT istek_id,malzeme_turu,istek_urun_sayisi,istek_tarih,istek_aciklama," +
                    "istek_urun_serial,(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                    " WHERE l.islem_ad='istek' AND l.islem_tip='insert' AND l.islem_kayit_id=istek_id) AS KullaniciAdi," +
                    "istek_urun_geldi FROM istek";

            DataTable dt_istekler = budak.Sorgu_DataTable(sorgu_istek_listesi);
            dgwurun_istek.DataSource = dt_istekler;
            //İstek listesi DataGridView'ında bir bölümü gelmiş olan istek satırları kırmızı olarak görüntüleniyor.
            gelen_istekleri_kizart();
            #endregion
        }

        private void gelen_istekleri_kizart()
        {
            #region İstekleri Kızart
            //İstek listesi DataGridView'ında bir bölümü gelmiş olan istek satırları kırmızı olarak görüntüleniyor.
            foreach (DataGridViewRow satir in dgwurun_istek.Rows) //DataGridView'ın her satırı için döngü çeviriyoruz.
            {
                if (satir.Cells[7].Value.ToString() == "True") //Eğer "istek_urun_geldi" kolonunun değeri "True" ise istenen ürünlerin bir bölümü gelmiş demektir.
                {
                    for (int i = 0; i < satir.Cells.Count; i++) //Satırdaki her hücre için döngü çeviriyoruz.
                    {
                        satir.Cells[i].Style.ForeColor = Color.Red; //Satırdaki tüm hücreleri teker teker kırmızıya boyuyoruz.
                    }
                }
            }
            #endregion
        }

        private void btnIstek_duzelt_Click(object sender, EventArgs e)
        {
            #region İSTEK DÜZELT BUTONU
            try
            {
                txtMalzemeTuru.Text = txtMalzemeTuru.Text.ToUpper();
                txtUrunIstekAciklama.Text = txtUrunIstekAciklama.Text.ToUpper();
               
                string sorgu_update_istek = "UPDATE istek SET malzeme_turu=@malzemeturu,istek_urun_sayisi=@isteksayi,istek_aciklama=@aciklama WHERE istek_id=@id";
                int duzeltilen_urun_sayisi = budak.Sorgu_Calistir(sorgu_update_istek, txtMalzemeTuru.Text, txtIstenenMiktarAdet.Text, txtUrunIstekAciklama.Text, secilen_urun.ToString());

                ////Depo girişi bilgileri güncelleniyor.
                //string sorgu_update_depo = "UPDATE depo SET urun_sayisi=@urun_sayisi,giris_tarihi=@giris_tarihi,garanti_bilgisi=@garanti_bilgisi WHERE id=@depo_id";
                ////Seçilen tarih, veritabanı için uygun formata sokuluyor.
                //string formatli_tarih = tarih_formatla(dtUrunGirisTarihi.Value.ToString());
                //int duzeltilen_depo_kaydi_sayisi = budak.Sorgu_Calistir(sorgu_update_depo, secilen_depo_id.ToString());

                //İşlem günlüğe kaydediliyor.
                string sorgu_log_giris_insert = "INSERT INTO loglar(islem_ad,islem_tip,islem_kayit_id,islem_yapan_kisi,islem_tarih) VALUES(@islem_ad,@islem_tip,@kayit_satir_id,@kisi,@tarih)";
                int sorgu_log_giris_insert_id = budak.Sorgu_Calistir(sorgu_log_giris_insert, "giris", "update", secilen_depo_id.ToString(), uye_id.ToString(), DateTime.Now.ToString());

                string duzelt_bildirim = "";
                if (duzeltilen_urun_sayisi > 0)
                {
                    duzelt_bildirim = "Düzeltme işleminiz başarıyla gerçekleşti.";
                    //Gün içinde yapılan giriş işlemlerinin listelendiği DataGridView güncelleniyor.
                    gunun_istek_kayitlarini_listele();
                    // ürünler düzeltildikten sonra sayfa yenilenmiş gibi textboxları temizliyoruz.
                    txtMalzemeTuru.Text = "";
                    txt_istek_urun_seri_no.Text = "";
                    txtIstenenMiktarAdet.Text = "";
                    txtUrunIstekAciklama.Text = "";
                    txtUrunSerial.ReadOnly = false;
                    secilen_urun = 0;
                }
                else
                {
                    duzelt_bildirim = "Düzeltme işleminiz gerçekleştirilemiyor. Tekrar deneyiniz.";
                }
                MessageBox.Show(duzelt_bildirim);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }
            
            #endregion
        }

        private void gunun_istek_kayitlarini_listele()
        {
            #region İSTEK Günün kayıtlarını listele

            string sorgu_gunun_istekleri = "SELECT istek_id, malzeme_turu,istek_urun_sayisi,istek_tarih,istek_aciklama" +
                    ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                    "WHERE l.islem_ad='istek' AND l.islem_tip='insert' AND l.islem_kayit_id=istek_id) AS KullaniciAdi " +
                " FROM istekler WHERE istek_tarih>=@gunun_baslangici";
            string gunun_baslangici = DateTime.Now.ToString("yyyyMMdd");
            //gunun_baslangici += " 00:00";
            dgwurun_istek.DataSource = budak.Sorgu_DataTable(sorgu_gunun_istekleri, gunun_baslangici);

            #endregion
        }

        private void dgwurun_istek_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            #region DGW İSTEK KAYIT ÇİFT TIKLAMA

            btnIstekYap.Enabled = false;
            //int secilen_istek_kaydi = 0;
            //secilen_istek_kaydi = Convert.ToInt32(dgwurun_istek.CurrentRow.Cells[0].Value.ToString());

            txtMalzemeTuru.Text = dgwurun_istek.CurrentRow.Cells[1].Value.ToString();
            txtIstenenMiktarAdet.Text = dgwurun_istek.CurrentRow.Cells[2].Value.ToString();
            //////////////////   HATALI SATIR YİNE BİR TARİH İŞLEMİ \\\\\\\\\\\\\\\\\\\\\
            dtIstekTarihi.Value = Convert.ToDateTime(TariheCevir(dgwurun_istek.CurrentRow.Cells[3].Value.ToString()));
            txtUrunIstekAciklama.Text = dgwurun_istek.CurrentRow.Cells[4].Value.ToString();

            #endregion
        }

        private void chkIstekTarihFiltre_CheckedChanged(object sender, EventArgs e)
        {
            #region İSTEK Tüm tarihlere göre istek filtreleme
            if (chkIstekTarihFiltre.Checked)
            {
                dtIstek_IlkTarih.Enabled = false;
                dtIstek_SonTarih.Enabled = false;
            }
            else
            {
                dtIstek_IlkTarih.Enabled = true;
                dtIstek_SonTarih.Enabled = true;
            }
            #endregion
        }

        private void menustripParolaDegistir_Click(object sender, EventArgs e)
        {
            #region Menü Strip Parola Değiştir Penceresi
            frmParolaDegistir paroladegis = new frmParolaDegistir(uye_id,yetki_duzeyi);
            paroladegis.Show();
            #endregion
        }

        #region  EXCEL AKTARIMLARI

        private void btnGiris_ExceleAktar_Click(object sender, EventArgs e)
        {
            #region STOK GİRİŞ EXCELE AKTAR

            Excel_Aktar(dgwStokGirisi);

            #endregion
        }

        private void btnCikis_Excele_aktar_Click(object sender, EventArgs e)
        {
            #region ÇIKIŞ EXCELE AKTAR

            Excel_Aktar(dgwCikisIslemleri);

            #endregion
        }

        private void btnDepo_Excele_aktar_Click(object sender, EventArgs e)
        {
            #region DEPO EXCELE AKTAR

            Excel_Aktar(dgvDepo);

            #endregion
        }

        private void btnistek_excel_aktar_Click(object sender, EventArgs e)
        {
            #region  İSTEK EXCEL AKTARIM

            Excel_Aktar(dgwurun_istek);

            #endregion
        }
        
        private void btnservisgonderilen_excele_aktar_Click(object sender, EventArgs e)
        {
            #region SERVİSE GÖNDERİLEN EXCEL AKTARIM

            Excel_Aktar(dgwServiseGonderilen);

            #endregion
        }

        private void btnservisDonen_excele_aktar_Click(object sender, EventArgs e)
        {
            #region SERVİSTEN DÖNEN EXCEL AKTARIM

            Excel_Aktar(dgwServistenDonenler);

            #endregion
        }

        private void btnAra_excelaktar_Click(object sender, EventArgs e)
        {
            #region ARA EXCEL AKTARIM

            Excel_Aktar(dgwAra);

            #endregion
        }

        private void btnEnvanter_excelaktar_Click(object sender, EventArgs e)
        {
            #region ENVANTER EXCEL AKTARIM

            Excel_Aktar(dgvEnvanter);

            #endregion
        }

        private void btnHurda_excelaktarim_Click(object sender, EventArgs e)
        {
            #region HURDA EXCEL AKTARIM

            Excel_Aktar(dgvHurda);

            #endregion
        }

        private void Excel_Aktar(DataGridView VeriTablosu)
        {
           // #region EXCEL AKTARIM
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;

            Microsoft.Office.Interop.Excel.Workbook kitap = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];

            for (int i = 0; i < VeriTablosu.Columns.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Range myrange = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, i + 1];
                myrange.Value2 = VeriTablosu.Columns[i].HeaderText;
            }

            for (int i = 0; i < VeriTablosu.Columns.Count; i++)
            {
                for (int j = 0; j < VeriTablosu.Rows.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myrange = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[j + 2, i + 1];
                    myrange.Value2 = VeriTablosu[i, j].Value;
                }
            }
        }

        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txturun_seri_no_TextChanged(object sender, EventArgs e)
        {
            #region Servise Gönderim sekmesinde seriali girilen ürünün cinsinin otomatik olarak yazılması

            string sorgu_urun_kontrol = "SELECT urun_cinsi FROM urunler WHERE urun_serial=@serial";
            DataTable dt_urun_bilgisi = budak.Sorgu_DataTable(sorgu_urun_kontrol, txturun_seri_no.Text.Trim());
            if (dt_urun_bilgisi.Rows.Count > 0)
            {
                txtUrunCinsi.Text = dt_urun_bilgisi.Rows[0][0].ToString();
            }

            #endregion
        }

        private void dgv_ariza_servise_giden_doldur()
        {
            #region Servise giden ürünler DataGridView'ının doldurulması

            string sorgu_ariza_servise_gidenler = "SELECT arizalar_id,firma_adi,urun_cinsi,ariza_urun_serial,"+
                "gonderilen_tarih,urun_sorunu" +
                    ",(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi " +
                    "WHERE l.islem_ad='ariza_servise_giden' AND l.islem_tip='insert' AND l.islem_kayit_id=arizalar_id) AS KullaniciAdi " +
                "FROM arizalar WHERE alinan_tarih=0 ORDER BY gonderilen_tarih DESC";
            dgwServiseGonderilen.DataSource = budak.Sorgu_DataTable(sorgu_ariza_servise_gidenler);

            #endregion
        }

        private void dgv_ariza_servisten_donen_doldur()
        {
            #region Servisten dönen ürünler DataGridView'ının doldurulması

            string sorgu_ariza_servisten_donenler = @"SELECT arizalar_id,urun_cinsi,ariza_urun_serial,
                alinan_tarih,yapilan_islem
                    ,(SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi 
                   WHERE l.islem_ad='ariza_servisten_donen' AND l.islem_tip='insert' AND l.islem_kayit_id=arizalar_id) AS KullaniciAdi 
                FROM arizalar WHERE alinan_tarih!=0 ORDER BY alinan_tarih DESC";
            dgwServistenDonenler.DataSource = budak.Sorgu_DataTable(sorgu_ariza_servisten_donenler);

            #endregion
        }

        private void dgwServiseGonderilen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            #region Servise gidenler DataGridView'ı çift tıklama

            secilen_arizaya_giden_kaydi = Convert.ToInt32(dgwServiseGonderilen.CurrentRow.Cells[0].Value);
            txtFirmaAdi.Text = dgwServiseGonderilen.CurrentRow.Cells[1].Value.ToString();
            txtUrunCinsi.Text = dgwServiseGonderilen.CurrentRow.Cells[2].Value.ToString();
            txturun_seri_no.Text = dgwServiseGonderilen.CurrentRow.Cells[3].Value.ToString();
            dtServisGonderilenTarih.Value = Convert.ToDateTime(TariheCevir(dgwServiseGonderilen.CurrentRow.Cells[4].Value.ToString()));
            txtUrunSorunu.Text = dgwServiseGonderilen.CurrentRow.Cells[5].Value.ToString();

            #endregion
        }

        private void menuSecilenArizaServistenDonduToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region Servisten dönen kaydın seçilmesi
            //Servise gidenler tablosundan bir kayıt seçilmemişse, kullanıcı uyarılıyor.
            if (secilen_arizaya_giden_kaydi == 0)
            {
                MessageBox.Show("Lütfen servisten dönen ürünü seçmek için, tabloda ilgili kayıt satırına çift tıklayınız.");
            }
            else //Servise giden kaydı seçilmişse, ilgili alanlar dolduruluyor. 
            {
                txtDonenUrunCinsi.Text = dgwServiseGonderilen.CurrentRow.Cells[2].Value.ToString();
                txtDonenUrunSeriNo.Text= dgwServiseGonderilen.CurrentRow.Cells[3].Value.ToString();
            }
            #endregion
        }

        private void dgwServistenDonenler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            #region Servisten dönenler DataGridView'ı çift tıklama
            
            secilen_arizadan_donen_kaydi = Convert.ToInt32(dgwServistenDonenler.CurrentRow.Cells[0].Value);
            txtDonenUrunCinsi.Text = dgwServistenDonenler.CurrentRow.Cells[1].Value.ToString();
            txtDonenUrunSeriNo.Text = dgwServistenDonenler.CurrentRow.Cells[2].Value.ToString();
            dtServistenDonenlerTarih.Value = Convert.ToDateTime(TariheCevir(dgwServistenDonenler.CurrentRow.Cells[3].Value.ToString()));
            txtYapilanIslem.Text = dgwServistenDonenler.CurrentRow.Cells[4].Value.ToString();

            #endregion
        }

        private void kullanıcıAdıDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region KULLANICI ADI DEĞİŞTİRME FORM AÇILMASI
            KullaniciAdi_Degistirme kullaniciaddegistir = new KullaniciAdi_Degistirme(uye_id);
            kullaniciaddegistir.Show();
            #endregion
        }

        private void menuSecilenUrunuHurdayaAyirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region  HURDAYA AYIR AÇIKLAMASI (sağ tık menüsü)

            Hurdaya_Ayir hurda_ayir = new Hurdaya_Ayir(this);
            hurda_ayir.ShowDialog();

            #endregion
        }

        private void hurdalar_tum_giris_kaydini_listele()
        {
            #region HURDA GİRİŞ KAYDI LİSTELENİYOR
            //string sorgu_hurdalar_giris_kayitlari = "SELECT c.id, u.urun_ad, u.urun_marka, u.urun_model, u.urun_serial, d.urun_sayisi," +
                    //"h.hurdaya_ayrilma_tarihi, h.hurda_aciklama, (SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN " +
                        //"loglar AS l ON y.YetkiID = l.islem_yapan_kisi WHERE l.islem_ad='hurda' AND l.islem_tip='insert' " +
                        //"AND l.islem_kayit_id=c.id) AS KullaniciAdi FROM urunler AS u JOIN depo AS d ON " +
                        //"u.urun_id = d.urun_id JOIN hurdalar AS h ON u.urun_id = h.urun_id";
                string sorgu_hurdalar_giris_kayitlari = @"SELECT h.hurda_id,u.urun_ad,u.urun_marka,u.urun_model,u.urun_serial,
                    (SELECT SUM(urun_sayisi) FROM depo WHERE u.urun_id) AS urun_sayisi,
                    h.hurdaya_ayrilma_tarihi,h.hurda_aciklama,
                    (SELECT y.KullaniciAdi FROM yetkiler AS y INNER JOIN loglar AS l ON y.YetkiID = l.islem_yapan_kisi
                    WHERE l.islem_ad='hurda' AND l.islem_tip='insert' AND l.islem_kayit_id=h.hurda_id) AS KullaniciAdi
                    FROM hurdalar AS h INNER JOIN urunler AS u ON h.urun_id=u.urun_id";
            dgvHurda.DataSource = budak.Sorgu_DataTable(sorgu_hurdalar_giris_kayitlari);
            #endregion

        }

        private void yenileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region YENİLE BUTONU

            Form1_Load(sender, e);
            //Global değişkenlerin değerleri sıfırlanıyor.
            secilen_urun = 0; //Önceden kaydedilen bir ürün seçildiğinde kullanılacak olan değişken. 
            secilen_depo_id = 0; //Düzeltilmek istenen depo giriş kaydının id numarası.
            filtre_acik = false;
            secilen_cikis_kaydi = 0;
            secilen_arizaya_giden_kaydi = 0;
            secilen_arizadan_donen_kaydi = 0;

            #endregion
        }

        private void yeniStokTakipAçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login_SilAktif giris = new Login_SilAktif();
            giris.Show();
        }

        private void menuSecilenIstekKaydiSifirlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region İstek Formu Temizleniyor

            secilen_urun = 0;
            //txt_istek_urun_seri_no.Text = "";
            txtMalzemeTuru.Text = "";
            txtIstenenMiktarAdet.Text = "";
            dtIstekTarihi.Value = DateTime.Now;
            txtUrunIstekAciklama.Text = "";

            btnIstekYap.Enabled = true;

            #endregion
        }

        private void dgvEnvanter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            envanter_secili_urun = Convert.ToInt32(dgvEnvanter.SelectedRows[0].Cells[1].Value);
            envanter_secili_kayit_id = Convert.ToInt32(dgvEnvanter.SelectedRows[0].Cells[0].Value);
        }

        private string TariheCevir(string sayi)
        {
            string Yil = sayi.Substring(0, 4);
            string Ay = sayi.Substring(4, 2);
            string Gun = sayi.Substring(6, 2);
            return Gun + "." + Ay + "." + Yil;
        }

        private void menuSecilenUrununCikisiniYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string akinda_tut = "";
            akinda_tut = dgvDepo.CurrentRow.Cells[4].Value.ToString();
            tbyeni_stok_girisi.SelectedIndex = 1;
            txtCikisYapilanUrunSerial.Text = akinda_tut;
            txtCikisYapilanUrunSerial_TextChanged(sender, e);
        }

        private void dgvDepo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuSecilenArizaServistenDonduToolStripMenuItem.Visible = false;
                menuSecilenCikisiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenIstekKaydiSifirlaToolStripMenuItem.Visible = false;
                menuSecilenUrunuHurdayaAyirToolStripMenuItem.Visible = false;
                menuSecilenUrununCikisiniYapToolStripMenuItem.Visible = true;
                menuSecilenUrunuSifirlaToolStripMenuItem.Visible = false;
                menuSag.Show(dgvDepo.PointToScreen(e.Location));
            }            
        }

    }
}