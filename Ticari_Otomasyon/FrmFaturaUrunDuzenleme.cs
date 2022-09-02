using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        public string urunid;
        SqlConnect sql = new SqlConnect();

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            TxtUrunid.Text = urunid;

            SqlCommand komut = new SqlCommand("Select * From TBL_FATURADETAY where FATURAURUNID=@faturaUrunID", sql.Connection());
            komut.Parameters.AddWithValue("@faturaUrunID", urunid);
            SqlDataReader sqlDataReader = komut.ExecuteReader();
            while (sqlDataReader.Read())
            {
                TxtFiyat.Text = sqlDataReader[3].ToString();
                TxtMiktar.Text = sqlDataReader[2].ToString();
                TxtTutar.Text = sqlDataReader[4].ToString();
                TxtUrunAd.Text = sqlDataReader[1].ToString();

                sql.Connection().Close();
            }
        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            Delete();
        }

        void Update()
        {
            SqlCommand update = new SqlCommand("update TBL_FATURADETAY set URUNAD=@P1,MIKTAR=@P2,FIYAT=@P3,TUTAR=@P4 WHERE FATURAURUNID=@faturaUrunID", sql.Connection());
            update.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
            update.Parameters.AddWithValue("@p2", TxtMiktar.Text);
            update.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
            update.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
            update.Parameters.AddWithValue("@faturaUrunID", TxtUrunid.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Değişiklikler Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void Delete()
        {
            SqlCommand delete = new SqlCommand("Delete From Tbl_FaturaDetay where FATURAURUNID=@p1", sql.Connection());
            delete.Parameters.AddWithValue("@p1", TxtUrunid.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
