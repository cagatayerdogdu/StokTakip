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
    public partial class gerialimislemi : Form
    {
        public gerialimislemi()
        {
            InitializeComponent();
        }

        public string secim = "";
  
        private void btn_geri_alim_islem_yap_Click(object sender, EventArgs e)
        {
            if (rdyeni_urun.Checked)
            {
                secim = "Giriş";
            }
            else
            {
                secim = "Çıkış";
            }
            this.Hide();
        }

        private void rdyeni_urun_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(13))
            {
                btn_geri_alim_islem_yap_Click(sender, e);
            }
        }

        private void rdcikis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(13))
            {
                btn_geri_alim_islem_yap_Click(sender, e);
            }
        }
    }
}
