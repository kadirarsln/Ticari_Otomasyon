using System;
using System.Data;
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

        void Clean()
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

            Clean();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            Add();
            GiderListesi();
            Clean();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            Delete();
            GiderListesi();
            Clean();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            Update();
            GiderListesi();
            Clean();
        }

        void Add()
        {
            SqlCommand add = new SqlCommand("insert into TBL_GIDERLER  (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) values " +
                                            "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", sql.Connection());
            add.Parameters.AddWithValue("@p1", CmbAy.Text);
            add.Parameters.AddWithValue("@p2", CmbYil.Text);
            add.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            add.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            add.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            add.Parameters.AddWithValue("@p6", decimal.Parse(Txtinternet.Text));
            add.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            add.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            add.Parameters.AddWithValue("@p9", RchNotlar.Text);
            add.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Gider tabloya eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void Delete()
        {
            SqlCommand delete = new SqlCommand("Delete From TBL_GIDERLER where ID=@id", sql.Connection());
            delete.Parameters.AddWithValue("@id", Txtid.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Gider Listeden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        void Update()
        {
            SqlCommand update = new SqlCommand("update TBL_GIDERLER set AY=@P1,YIL=@P2,ELEKTRIK=@P3,SU=@P4,DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 where ID=@id", sql.Connection());
            update.Parameters.AddWithValue("@p1", CmbAy.Text);
            update.Parameters.AddWithValue("@p2", CmbYil.Text);
            update.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            update.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            update.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            update.Parameters.AddWithValue("@p6", decimal.Parse(Txtinternet.Text));
            update.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            update.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            update.Parameters.AddWithValue("@p9", RchNotlar.Text);
            update.Parameters.AddWithValue("@id", Txtid.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Gider Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow sqlDataReader = gridView1.GetDataRow(gridView1.FocusedRowHandle);
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
    }
}
