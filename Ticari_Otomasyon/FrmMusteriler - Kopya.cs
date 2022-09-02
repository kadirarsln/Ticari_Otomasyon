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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        SqlConnect sql = new SqlConnect();

        void Listele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From TBL_MUSTERILER", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        void Clean()
        {
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtMail.Text = "";
            TxtSoyad.Text = "";
            TxtVergi.Text = "";
            MskTC.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            RchAsqlDataReaderes.Text = "";
        }

        void SehirListesi()
        {
            SqlCommand komut = new SqlCommand("Select Sehir From TBL_ILLER", sql.Connection());
            SqlDataReader sqlDataReader = komut.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Cmbil.Properties.Items.Add(sqlDataReader[0]);
            }
            sql.Connection().Close();
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            Listele();

            SehirListesi();

            Clean();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("Select IlCE from TBL_ILCELER where Sehir=@p1", sql.Connection());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader sqlDataReader = komut.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Cmbilce.Properties.Items.Add(sqlDataReader[0]);
            }
            sql.Connection().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,AsqlDataReaderES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", sql.Connection());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", Cmbil.Text);
            komut.Parameters.AddWithValue("@p8", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", RchAsqlDataReaderes.Text);
            komut.Parameters.AddWithValue("@p10", TxtVergi.Text);
            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Clean();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Delete from TBL_MUSTERILER where ID=@p1", sql.Connection());
            komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Listele();
        }

        private void gridView1_FocusesqlDataReaderowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusesqlDataReaderowChangedEventArgs e)
        {
            DataRow sqlDataReader = gridView1.GetDataRow(gridView1.FocusesqlDataReaderowHandle);
            if (sqlDataReader != null)
            {
                Txtid.Text = sqlDataReader["ID"].ToString();
                TxtAd.Text = sqlDataReader["AD"].ToString();
                TxtSoyad.Text = sqlDataReader["SOYAD"].ToString();
                MskTelefon1.Text = sqlDataReader["TELEFON"].ToString();
                MskTelefon2.Text = sqlDataReader["TELEFON2"].ToString();
                MskTC.Text = sqlDataReader["TC"].ToString();
                TxtMail.Text = sqlDataReader["MAIL"].ToString();
                Cmbil.Text = sqlDataReader["IL"].ToString();
                Cmbilce.Text = sqlDataReader["ILCE"].ToString();
                TxtVergi.Text = sqlDataReader["VERGIDAIRE"].ToString();
                RchAsqlDataReaderes.Text = sqlDataReader["AsqlDataReaderES"].ToString();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_MUSTERILER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4,TC=@P5,MAIL=@P6,IL=@P7,ILCE=@P8,VERGIDAIRE=@P9,AsqlDataReaderES=@P10 where ID=@P11", sql.Connection());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", Cmbil.Text);
            komut.Parameters.AddWithValue("@p8", Cmbilce.Text);
            komut.Parameters.AddWithValue("@p9", TxtVergi.Text);
            komut.Parameters.AddWithValue("@p10", RchAsqlDataReaderes.Text);
            komut.Parameters.AddWithValue("@p11", Txtid.Text);
            komut.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }

        private void MskTelefon2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
