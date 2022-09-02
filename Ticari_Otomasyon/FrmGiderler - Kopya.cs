using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.sqlDataReaderawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }
        SqlConnect sql = new SqlConnect();

        void GiderListesi()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From TBL_GIDERLER Order By ID Asc", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        void temizle()
        {
            TxtDogalgaz.Text = "";
            TxtEkstra.Text = "";
            TxtElektrik.Text = "";
            Txtid.Text = "";
            Txtinternet.Text = "";
            TxtMaaslar.Text = "";
            TxtSu.Text = "";
            CmbAy.Text = "";
            CmbYil.Text = "";
            RchNotlar.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            GiderListesi();

            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_GIDERLER  (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", sql.Connection());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(Txtinternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RchNotlar.Text);
            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Gider tabloya eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GiderListesi();
            //temizle();
        }

        private void gridView1_FocusesqlDataReaderowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusesqlDataReaderowChangedEventArgs e)
        {
            DataRow sqlDataReader = gridView1.GetDataRow(gridView1.FocusesqlDataReaderowHandle);
            if (sqlDataReader != null)
            {
                Txtid.Text = sqlDataReader["ID"].ToString();
                CmbAy.Text = sqlDataReader["AY"].ToString();
                CmbYil.Text = sqlDataReader["YIL"].ToString();
                TxtElektrik.Text = sqlDataReader["ELEKTRIK"].ToString();
                TxtSu.Text = sqlDataReader["SU"].ToString();
                TxtDogalgaz.Text = sqlDataReader["DOGALGAZ"].ToString();
                Txtinternet.Text = sqlDataReader["INTERNET"].ToString();
                TxtMaaslar.Text = sqlDataReader["MAASLAR"].ToString();
                TxtEkstra.Text = sqlDataReader["EKSTRA"].ToString();
                RchNotlar.Text = sqlDataReader["NOTLAR"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("Delete From TBL_GIDERLER where ID=@p1", sql.Connection());
            komutsil.Parameters.AddWithValue("@p1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            sql.Connection().Close();
            GiderListesi();
            MessageBox.Show("Gider Listeden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_GIDERLER set AY=@P1,YIL=@P2,ELEKTRIK=@P3,SU=@P4,DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 where ID=@p10", sql.Connection());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbYil.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(Txtinternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RchNotlar.Text);
            komut.Parameters.AddWithValue("@p10", Txtid.Text);
            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Gider Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            GiderListesi();
            temizle();
        }
    }
}
