using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();


        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select UrunAd, Sum(Adet) as 'Adet' From TBL_URUNLER " +
                "Group By URUNAD having Sum(Adet) <= 100 order by SUM(Adet)", bgl.baglanti());
            da.Fill(dt);
            GridControlStoklar.DataSource = dt;

        }

        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select TARIH,SAAT,BASLIK FROM TBL_NOTLAR ", bgl.baglanti());
            da.Fill(dt);
            GridControlAjanda.DataSource = dt;
        }

        void firmahareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareketler", bgl.baglanti());
            da.Fill(dt);
            GridControlFirmaHareket.DataSource = dt;
        }

        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Ad,Telefon1 From TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            GridControlFihrist.DataSource = dt;
        }

        void haberler(string a)
        {
            listBox1.Items.Clear();
            XmlTextReader xmloku = new XmlTextReader(a);
            while(xmloku.Read())
            {
                if(xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }
            






        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();

            ajanda();

            firmahareket();

            fihrist();

            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/kurlar_tr.html");

            string adres = "https://www.hurriyet.com.tr/rss/anasayfa";
            haberler(adres);





        }
    }
}
