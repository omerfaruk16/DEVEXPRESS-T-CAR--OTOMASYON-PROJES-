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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void Firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }




        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select * From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbıl.Properties.Items.Add(dr[1]);

            }

            bgl.baglanti().Close();

        }


       

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 From TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchOzlKd1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }


        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            Firmalistesi();

            sehirlistesi();

            carikodaciklamalar();

            temizle();

        }

        void temizle()
        {
            TxtAd.Text = "";
            TxtID.Text = "";
            TxtSektor.Text = "";
            MskYetkili.Text = "";
            MskTc.Text = "";
            TxtYetkiliGorev.Text = "";
            MskTel1.Text = "";
            MskTel2.Text = "";
            MskTel3.Text = "";
            MskFax.Text = "";
            TxtMail.Text = "";
            Cmbıl.Text = "";
            CmbIlce.Text = "";
            TxtVergı.Text = "";
            RchAdres.Text = "";
            Txtozelkod1.Text = "";
            Txtozelkod2.Text = "";
            Txtozelkod3.Text = "";


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtID.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtSektor.Text = dr["SEKTOR"].ToString();
                MskYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskTc.Text = dr["YETKILITC"].ToString();
                TxtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                MskTel1.Text = dr["TELEFON1"].ToString();
                MskTel2.Text = dr["TELEFON2"].ToString();
                MskTel3.Text = dr["TELEFON3"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                Cmbıl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtVergı.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                Txtozelkod1.Text = dr["OZELKOD1"].ToString();
                Txtozelkod2.Text = dr["OZELKOD2"].ToString();
                Txtozelkod3.Text = dr["OZELKOD3"].ToString();



            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR(AD,YETKILIADSOYAD,YEKILITC,YETKILISTATU,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", MskYetkili.Text);
            komut.Parameters.AddWithValue("@p3", MskTc.Text).ToString();
            komut.Parameters.AddWithValue("@p4", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@p6", MskTel1.Text);
            komut.Parameters.AddWithValue("@p7", MskTel2.Text);
            komut.Parameters.AddWithValue("@p8", MskTel3.Text);
            komut.Parameters.AddWithValue("@p9", TxtMail.Text);
            komut.Parameters.AddWithValue("@p10", MskFax.Text);
            komut.Parameters.AddWithValue("@p11", Cmbıl.Text);
            komut.Parameters.AddWithValue("@p12", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p13", TxtVergı.Text);
            komut.Parameters.AddWithValue("@p14", RchAdres.Text);
            komut.Parameters.AddWithValue("@p15", Txtozelkod1.Text);
            komut.Parameters.AddWithValue("@p16", Txtozelkod2.Text);
            komut.Parameters.AddWithValue("@p17", Txtozelkod3.Text);
            komut.Parameters.AddWithValue("@p18", TxtID.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgisi Başarıyla Eklendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Firmalistesi();

        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {

            CmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE From TBL_ILCELER WHERE SEHIR = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbıl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIlce.Properties.Items.Add(dr[0]);

            }

            bgl.baglanti().Close();


        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE  From TBL_FIRMALAR WHERE ID = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            Firmalistesi();
            MessageBox.Show("Firma Listeden Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FIRMALAR Set AD=@p1, YETKILIADSOYAD=@p2, YETKILITC=@p3, YETKILISTATU=@p4, SEKTOR= @p5, TELEFON1= @p6, TELEFON2=@p7, TELEFON3=@p8, MAIL=@p9, FAX=@p10, IL=@p11, ILCE=@p12, VERGIDAIRE=@p13, ADRES=@p14, OZELKOD1=@p15, OZELKOD2=@p16, OZELKOD3=@p17 WHERE ID=@p18 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", MskYetkili.Text);
            komut.Parameters.AddWithValue("@p3", MskTc.Text).ToString();
            komut.Parameters.AddWithValue("@p4", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@p6", MskTel1.Text);
            komut.Parameters.AddWithValue("@p7", MskTel2.Text);
            komut.Parameters.AddWithValue("@p8", MskTel3.Text);
            komut.Parameters.AddWithValue("@p9", TxtMail.Text);
            komut.Parameters.AddWithValue("@p10", MskFax.Text);
            komut.Parameters.AddWithValue("@p11", Cmbıl.Text);
            komut.Parameters.AddWithValue("@p12", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p13", TxtVergı.Text);
            komut.Parameters.AddWithValue("@p14", RchAdres.Text);
            komut.Parameters.AddWithValue("@p15", Txtozelkod1.Text);
            komut.Parameters.AddWithValue("@p16", Txtozelkod2.Text);
            komut.Parameters.AddWithValue("@p17", Txtozelkod3.Text);
            komut.Parameters.AddWithValue("@p18", TxtID.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgisi Başarıyla Güncellendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Firmalistesi();

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
