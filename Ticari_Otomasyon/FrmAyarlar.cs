using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        SqlConnect sql = new SqlConnect();

        void Listele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * From TBL_ADMIN", sql.Connection());
            dataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            Listele();
            TxtKullaniciAd.Text = "";
            TxtSifre.Text = "";
        }

        private void Btnislem_Click(object sender, EventArgs e)
        {
            if (Btnislem.Text == "Kaydet")
            {
                Add();
                Listele();
            }
            if (Btnislem.Text == "Güncelle")
            {
                Update();
                Listele();
            }
        }

        void Add()
        {
            SqlCommand add = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", sql.Connection());
            add.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
            add.Parameters.AddWithValue("@p2", TxtSifre.Text);
            add.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void Update()
        {
            SqlCommand update = new SqlCommand("update TBL_ADMIN set sifre=@sifre where KullaniciAd=@kullaniciAd", sql.Connection());
            update.Parameters.AddWithValue("@kullaniciAd", TxtKullaniciAd.Text);
            update.Parameters.AddWithValue("@sifre", TxtSifre.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void Delete()
        {
            SqlCommand delete = new SqlCommand("Delete From TBL_ADMIN where KullaniciAd=@kullaniciAd", sql.Connection());
            delete.Parameters.AddWithValue("@kullaniciAd", TxtKullaniciAd.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Kullanıcı Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                TxtKullaniciAd.Text = dataRow["KullaniciAd"].ToString();
                TxtSifre.Text = dataRow["Sifre"].ToString();
            }
        }

        private void TxtKullaniciAd_TextChanged(object sender, EventArgs e)
        {
            if (TxtKullaniciAd.Text != "")
            {
                Btnislem.Text = "Güncelle";
                Btnislem.BackColor = Color.GreenYellow;
            }
            else
            {
                Btnislem.Text = "Kaydet";
                Btnislem.BackColor = Color.MediumTurquoise;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}
