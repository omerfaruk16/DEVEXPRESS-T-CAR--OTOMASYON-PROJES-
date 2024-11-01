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

namespace Ticari_Otomasyon
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmStoklar_Load(object sender, EventArgs e)
        {


            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD,Sum(Adet) As 'Miktar' from TBL_URUNLER Group by URUNAD", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;




            //charta stok miktari çekme

            SqlCommand komut = new SqlCommand("Select URUNAD,Sum(Adet) As 'Miktar' from TBL_URUNLER Group by URUNAD", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }

            bgl.baglanti();

            //CHARTA FİRMA ŞEHİR SAYISI ÇEKME

            SqlCommand komut2 = new SqlCommand("Select IL,Count(*) From TBL_FIRMALAR Group By IL", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl3.Series["Series 2"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }

            //CHARTA MÜŞTERİ ŞEHİR SAYISI ÇEKME

            SqlCommand komut3 = new SqlCommand("Select IL,Count(*) From TBL_MUSTERILER Group By IL", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                chartControl4.Series["Series 1"].Points.AddPoint(Convert.ToString(dr3[0]), int.Parse(dr3[1].ToString()));
            }

            //CHARTA PERSONEL GÖREV GRAFİĞİ ÇEKME


            SqlCommand komut4 = new SqlCommand("Select GOREV,Count(*) From TBL_PERSONELLER Group By GOREV", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                chartControl5.Series["Series 1"].Points.AddPoint(Convert.ToString(dr4[0]), int.Parse(dr4[1].ToString()));
            }

            bgl.baglanti().Close();


        }












        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabPage3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.ad = dr["URUNAD"].ToString();
            }

            fr.Show();
        }
    }
}
