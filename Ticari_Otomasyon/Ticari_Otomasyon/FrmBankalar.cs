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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }


        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri ", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }


        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select * From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Properties.Items.Add(dr[1]);

            }

            bgl.baglanti().Close();

        }



        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {

            CmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("Select ILCE From TBL_ILCELER WHERE SEHIR = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbIlce.Properties.Items.Add(dr[0]);

            }

            bgl.baglanti().Close();


        }


        void temizle()
        {
            TxtBankaAd.Text = "";
            TxtHesapNo.Text = "";
            TxtHesapTur.Text = "";
            TxtIban.Text = "";
            TxtID.Text = "";
            TxtSube.Text = "";
            TxtTarih.Text = "";
            TxtTelefonNo.Text = "";
            TxtYetkılıAdSoyad.Text = "";
            LookUpFirma.Text = "";
        }


        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID, AD From TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            LookUpFirma.Properties.ValueMember = "ID";
            LookUpFirma.Properties.DisplayMember = "AD";
            LookUpFirma.Properties.DataSource = dt;

        }




        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();

            sehirlistesi();

            firmalistesi();

            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into BANKAADI,IL,ILCE,SUBE,TELEFON,IBAN,HESAPNO,YETKILI,TARIH,HESAPTURU,FIRMAID) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", CmbIl.Text);
            komut.Parameters.AddWithValue("@p3", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtTelefonNo.Text);
            komut.Parameters.AddWithValue("@p6", TxtIban.Text);
            komut.Parameters.AddWithValue("@p7", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p8", TxtYetkılıAdSoyad.Text);
            komut.Parameters.AddWithValue("@p9", TxtTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTur.Text);
            komut.Parameters.AddWithValue("@p11", LookUpFirma.EditValue);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Başarıyla Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtID.Text = dr["ID"].ToString();
                TxtBankaAd.Text = dr["BANKAADI"].ToString();
                CmbIl.Text = dr["IL"].ToString();
                CmbIlce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                TxtTelefonNo.Text = dr["TELEFON"].ToString();
                TxtIban.Text = dr["IBAN"].ToString();
                TxtHesapNo.Text = dr["HESAPNO"].ToString();
                TxtYetkılıAdSoyad.Text = dr["YETKILI"].ToString();
                TxtTarih.Text = dr["TARIH"].ToString();
                TxtHesapTur.Text = dr["HESAPTURU"].ToString();

            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete From TBL_BANKALAR Where ID = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            MessageBox.Show("Banka Bilgisi Başarıyla Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update TBL_BANKALAR set BANKAADI=@p1,IL=@p2, ILCE=@p3, SUBE=@p4,TELEFON=@p5,IBAN=@p6,HESAPNO=@p7,YETKILI=@p8,TARIH=@p9,HESAPTURU=@p10,FIRMAID=@p11 Where ID=@p12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", CmbIl.Text);
            komut.Parameters.AddWithValue("@p3", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtTelefonNo.Text);
            komut.Parameters.AddWithValue("@p6", TxtIban.Text);
            komut.Parameters.AddWithValue("@p7", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p8", TxtYetkılıAdSoyad.Text);
            komut.Parameters.AddWithValue("@p9", TxtTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTur.Text);
            komut.Parameters.AddWithValue("@p11", LookUpFirma.EditValue);
            komut.Parameters.AddWithValue("@p12", TxtID.Text);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Başarıyla Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
