using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        SqlConnect sql = new SqlConnect();

        void Listele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From Tbl_Notlar", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            Listele();

            Clean();
        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            Add();
            Listele();
            Clean();
        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Clean();
        }
        private void BtnSil_Click(object sender, EventArgs e)
        {
            Delete();
            Listele();
            Clean();
        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            Update();
            Listele();
            Clean();
        }

        void Add()
        {
            SqlCommand add = new SqlCommand("insert into TBL_NOTLAR (NOTTARIH,NOTSAAT,NOTBASLIK,NOTDETAY,NOTOLUSTURAN,NOTHITAP) values (@P1,@P2,@P3,@P4,@P5,@P6)", sql.Connection());
            add.Parameters.AddWithValue("@P1", MskTarih.Text);
            add.Parameters.AddWithValue("@P2", MskSaat.Text);
            add.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            add.Parameters.AddWithValue("@P4", RchDetay.Text);
            add.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            add.Parameters.AddWithValue("@P6", TxtHitap.Text);
            add.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Not Bilgisi Siteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void Delete()
        {
            SqlCommand delete = new SqlCommand("Delete From TBL_NOTLAR Where NOTID=@notID", sql.Connection());
            delete.Parameters.AddWithValue("@notID", Txtid.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Not Sistemden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        void Clean()
        {
            TxtBaslik.Text = "";
            TxtHitap.Text = "";
            Txtid.Text = "";
            TxtOlusturan.Text = "";
            RchDetay.Text = "";
            MskSaat.Text = "";
            MskTarih.Text = "";
        }

        void Update()
        {
            SqlCommand update = new SqlCommand("UPDATE TBL_NOTLAR set TARIH=@P1,SAAT=@P2,BASLIK=@P3,DETAY=@P4,OLUSTURAN=@P5,HITAP=@P6 where NOTID=@notID", sql.Connection());
            update.Parameters.AddWithValue("@P1", MskTarih.Text);
            update.Parameters.AddWithValue("@P2", MskSaat.Text);
            update.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            update.Parameters.AddWithValue("@P4", RchDetay.Text);
            update.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            update.Parameters.AddWithValue("@P6", TxtHitap.Text);
            update.Parameters.AddWithValue("@notID", Txtid.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Not Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                Txtid.Text = dataRow["NOTID"].ToString();
                TxtBaslik.Text = dataRow["NOTBASLIK"].ToString();
                RchDetay.Text = dataRow["NOTDETAY"].ToString();
                TxtOlusturan.Text = dataRow["NOTOLUSTURAN"].ToString();
                TxtHitap.Text = dataRow["NOTHITAP"].ToString();
                MskTarih.Text = dataRow["NOTTARIH"].ToString();
                MskSaat.Text = dataRow["NOTSAAT"].ToString();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay frmNotDetay = new FrmNotDetay();

            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                frmNotDetay.metin = dataRow["NOTDETAY"].ToString();
            }
            frmNotDetay.Show();
        }
    }
}
