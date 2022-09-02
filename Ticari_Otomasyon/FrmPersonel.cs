using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }
        SqlConnect sql = new SqlConnect();

        void PersonelListe()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from TBL_PERSONELLER", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        void SEHIRListesi()
        {
            SqlCommand il = new SqlCommand("Select SEHIR From TBL_ILLER", sql.Connection());
            SqlDataReader dataRow = il.ExecuteReader();
            while (dataRow.Read())
            {
                Cmbil.Properties.Items.Add(dataRow[0]);
            }
            sql.Connection().Close();
        }
        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand ilce = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@SEHIR", sql.Connection());
            ilce.Parameters.AddWithValue("@SEHIR", Cmbil.SelectedIndex + 1);
            SqlDataReader dataRow = ilce.ExecuteReader();
            while (dataRow.Read())
            {
                Cmbilce.Properties.Items.Add(dataRow[0]);
            }
            sql.Connection().Close();
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            PersonelListe();

            SEHIRListesi();

            Clean();
        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            Add();
            PersonelListe();
            Clean();
        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            Delete();
            PersonelListe();
            Clean();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            Update();
            PersonelListe();
            Clean();
        }

        void Clean()
        {
            Txtid.Text = "";
            TxtAd.Text = "";
            TxtGorev.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            MskTC.Text = "";
            MskTelefon1.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            RchAdres.Text = "";
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                Txtid.Text = dataRow["ID"].ToString();
                TxtAd.Text = dataRow["AD"].ToString();
                TxtSoyad.Text = dataRow["SOYAD"].ToString();
                MskTelefon1.Text = dataRow["TELEFON"].ToString();
                MskTC.Text = dataRow["TC"].ToString();
                TxtMail.Text = dataRow["MAIL"].ToString();
                Cmbil.Text = dataRow["IL"].ToString();
                Cmbilce.Text = dataRow["ILCE"].ToString();
                RchAdres.Text = dataRow["ADRES"].ToString();
                TxtGorev.Text = dataRow["GOREV"].ToString();
            }
        }


        void Add()
        {
            SqlCommand add = new SqlCommand("insert into TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) values " +
                                            "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)",
                sql.Connection());
            add.Parameters.AddWithValue("@P1", TxtAd.Text);
            add.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            add.Parameters.AddWithValue("@P3", MskTelefon1.Text);
            add.Parameters.AddWithValue("@P4", MskTC.Text);
            add.Parameters.AddWithValue("@P5", TxtMail.Text);
            add.Parameters.AddWithValue("@P6", Cmbil.Text);
            add.Parameters.AddWithValue("@P7", Cmbilce.Text);
            add.Parameters.AddWithValue("@P8", RchAdres.Text);
            add.Parameters.AddWithValue("@P9", TxtGorev.Text);
            add.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Personel Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void Delete()
        {
            SqlCommand delete = new SqlCommand("delete from TBL_PERSONELLER where ID=@p1", sql.Connection());
            delete.Parameters.AddWithValue("@p1", Txtid.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Personel Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        void Update()
        {
            SqlCommand update = new SqlCommand("Update TBL_PERSONELLER set AD=@P1,SOYAD=@P2,TELEFON=@P3,TC=@P4,MAIL=@P5,IL=@P6,ILCE=@P7,ADRES=@P8,GOREV=@P9 WHERE ID=@P10", sql.Connection());
            update.Parameters.AddWithValue("@P1", TxtAd.Text);
            update.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            update.Parameters.AddWithValue("@P3", MskTelefon1.Text);
            update.Parameters.AddWithValue("@P4", MskTC.Text);
            update.Parameters.AddWithValue("@P5", TxtMail.Text);
            update.Parameters.AddWithValue("@P6", Cmbil.Text);
            update.Parameters.AddWithValue("@P7", Cmbilce.Text);
            update.Parameters.AddWithValue("@P8", RchAdres.Text);
            update.Parameters.AddWithValue("@P9", TxtGorev.Text);
            update.Parameters.AddWithValue("@P10", Txtid.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
