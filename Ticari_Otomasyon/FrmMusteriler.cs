using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            RchAdres.Text = "";
        }

        void SehirListesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", sql.Connection());
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

            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@sehir", sql.Connection());
            komut.Parameters.AddWithValue("@sehir", Cmbil.SelectedIndex + 1);
            SqlDataReader sqlDataReader = komut.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Cmbilce.Properties.Items.Add(sqlDataReader[0]);
            }
            sql.Connection().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            Add();
            Listele();

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
            SqlCommand add = new SqlCommand("insert into TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", sql.Connection());
            add.Parameters.AddWithValue("@p1", TxtAd.Text);
            add.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            add.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            add.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            add.Parameters.AddWithValue("@p5", MskTC.Text);
            add.Parameters.AddWithValue("@p6", TxtMail.Text);
            add.Parameters.AddWithValue("@p7", Cmbil.Text);
            add.Parameters.AddWithValue("@p8", Cmbilce.Text);
            add.Parameters.AddWithValue("@p9", RchAdres.Text);
            add.Parameters.AddWithValue("@p10", TxtVergi.Text);
            add.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void Delete()
        {
            SqlCommand delete = new SqlCommand("Delete from TBL_MUSTERILER where ID=@id", sql.Connection());
            delete.Parameters.AddWithValue("@id", Txtid.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        void Update()
        {
            SqlCommand update = new SqlCommand("update TBL_MUSTERILER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4,TC=@P5,MAIL=@P6,IL=@P7,ILCE=@P8,VERGIDAIRE=@P9,ADRES=@P10 where ID=@id", sql.Connection());
            update.Parameters.AddWithValue("@p1", TxtAd.Text);
            update.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            update.Parameters.AddWithValue("@p3", MskTelefon1.Text);
            update.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            update.Parameters.AddWithValue("@p5", MskTC.Text);
            update.Parameters.AddWithValue("@p6", TxtMail.Text);
            update.Parameters.AddWithValue("@p7", Cmbil.Text);
            update.Parameters.AddWithValue("@p8", Cmbilce.Text);
            update.Parameters.AddWithValue("@p9", TxtVergi.Text);
            update.Parameters.AddWithValue("@p10", RchAdres.Text);
            update.Parameters.AddWithValue("@id", Txtid.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow sqlDataReader = gridView1.GetDataRow(gridView1.FocusedRowHandle);
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
                RchAdres.Text = sqlDataReader["ADRES"].ToString();
            }
        }
    }
}
