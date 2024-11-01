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
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {

        }

        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MUSTERIHAREKETLER", bgl.baglanti());
            da.Fill(dt);
            gridcontrol3.DataSource = dt;
        }


        void firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            gridControl1.DataSource = dt2;
        }

        public string ad;


        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;

            musterihareket();
            firmahareket();


            //TOPLAM TUTARI HESAPLAMA   

            SqlCommand komut1 = new SqlCommand("Select Sum(Tutar) From TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                LblKasaToplam.Text = dr1[0].ToString() + " TL";

            }
            bgl.baglanti().Close();

            //SON AYIN FATURALARI
            SqlCommand Komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ) from TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr2 = Komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblYapilanOdemeler.Text = dr2[0].ToString() + " TL";

            }
            bgl.baglanti().Close();

            //SON AYIN PERSONEL MAAŞLARI

            SqlCommand komut3 = new SqlCommand("Select Maaslar From TBL_GIDERLER order by ID asc", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblPersonelMaas.Text = dr3[0].ToString() + " TL";

            }
            bgl.baglanti().Close();

            //TOPLAM MÜŞTERİ SAYISI

            SqlCommand komut4 = new SqlCommand("Select Count (*) From TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblMusteriSayi.Text = dr4[0].ToString();

            }
            bgl.baglanti().Close();

            //TOPLAM FİRMA SAYISI

            SqlCommand komut5 = new SqlCommand("Select Count (*) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblFirma.Text = dr5[0].ToString();

            }
            bgl.baglanti().Close();

            //TOPLAM ŞEHİR SAYISI

            SqlCommand komut6 = new SqlCommand("Select Count (Distinct(IL)) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblSehir.Text = dr6[0].ToString();

            }
            bgl.baglanti().Close();

            SqlCommand komut7 = new SqlCommand("Select Count (Distinct(IL)) From TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                LblMusteriSehir.Text = dr7[0].ToString();

            }
            bgl.baglanti().Close();

            SqlCommand komut8 = new SqlCommand("Select Count (*) From TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                LblPersonelSayi.Text = dr8[0].ToString();

            }
            bgl.baglanti().Close();

            SqlCommand komut9 = new SqlCommand("Select Sum(Adet) From TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                LblStokSayisi.Text = dr9[0].ToString();

            }
            bgl.baglanti().Close();





        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //ELEKTRİK
            sayac++;
            if (sayac > 0 && sayac <= 5)
            {

                groupControl10.Text = "ELEKTRİK";
                SqlCommand komut10 = new SqlCommand("Select top 4 Ay,ELEKTRIK from TBL_GIDERLER order by ID Desc", bgl.baglanti());

                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            if (sayac > 5 && sayac <= 10)
            {
                //DOĞALGAZ
                groupControl10.Text = "DOĞALGAZ";
                chartControl3.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("Select  Top 4 AY , DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();

            }

            if (sayac > 11 && sayac <= 15)
            {
                //SU
                groupControl10.Text = "SU";
                chartControl3.Series["Aylar"].Points.Clear();


                SqlCommand komut11 = new SqlCommand("Select  Top 4 AY , SU FROM TBL_GIDERLER ORDER BY ID DESC", bgl.baglanti());

                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));

                }
                bgl.baglanti().Close();


            }
        }

    }

}


