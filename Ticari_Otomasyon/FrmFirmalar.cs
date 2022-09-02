using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        SqlConnect sql = new SqlConnect();

        void FirmaListesi()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From TBL_FIRMALAR1", sql.Connection());
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;

        }

        void SEHIRListesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", sql.Connection());
            SqlDataReader dataRow = komut.ExecuteReader();
            while (dataRow.Read())
            {
                Cmbil.Properties.Items.Add(dataRow[0]);
            }
            sql.Connection().Close();
        }

        void CariKodAciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", sql.Connection());
            SqlDataReader dataRow = komut.ExecuteReader();
            while (dataRow.Read())
            {
                RchKod1.Text = dataRow[0].ToString();
            }
            sql.Connection().Close();
        }


        void Clean()
        {
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
            TxtMail.Text = "";
            TxtSektor.Text = "";
            TxtVergi.Text = "";
            TxtYetkili.Text = "";
            TxtYetkiliGorev.Text = "";
            MskFax.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            MskYetkiliTC.Text = "";
            TxtKod1.Text = "";
            RchAdres.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
            TxtAd.Focus();
        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            FirmaListesi();

            SEHIRListesi();

            CariKodAciklamalar();

            Clean();
        }



        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                Txtid.Text = dataRow["ID"].ToString();
                TxtAd.Text = dataRow["AD"].ToString();
                TxtYetkiliGorev.Text = dataRow["YETKILISTATU"].ToString();
                TxtYetkili.Text = dataRow["YETKILIADSOYAD"].ToString();
                MskYetkiliTC.Text = dataRow["YETKILITC"].ToString();
                TxtSektor.Text = dataRow["SEKTOR"].ToString();
                MskTelefon1.Text = dataRow["TELEFON1"].ToString();
                MskTelefon2.Text = dataRow["TELEFON2"].ToString();
                MskTelefon3.Text = dataRow["TELEFON3"].ToString();
                TxtMail.Text = dataRow["MAIL"].ToString();
                MskFax.Text = dataRow["FAX"].ToString();
                Cmbil.Text = dataRow["IL"].ToString();
                Cmbilce.Text = dataRow["ILCE"].ToString();
                TxtVergi.Text = dataRow["VERGIDAIRE"].ToString();
                RchAdres.Text = dataRow["ADRES"].ToString();
                TxtKod1.Text = dataRow["OZELKOD1"].ToString();
                TxtKod2.Text = dataRow["OZELKOD2"].ToString();
                TxtKod3.Text = dataRow["OZELKOD3"].ToString();
            }

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand add = new SqlCommand("insert into TBL_FIRMALAR1 (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES" +
                                              " (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)", sql.Connection());
            add.Parameters.AddWithValue("@P1", TxtAd.Text);
            add.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            add.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            add.Parameters.AddWithValue("@P4", MskYetkiliTC.Text);
            add.Parameters.AddWithValue("@P5", TxtSektor.Text);
            add.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            add.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            add.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            add.Parameters.AddWithValue("@P9", TxtMail.Text);
            add.Parameters.AddWithValue("@P10", MskFax.Text);
            add.Parameters.AddWithValue("@P11", Cmbil.Text);
            add.Parameters.AddWithValue("@P12", Cmbilce.Text);
            add.Parameters.AddWithValue("@P13", TxtVergi.Text);
            add.Parameters.AddWithValue("@P14", RchAdres.Text);
            add.Parameters.AddWithValue("@P15", TxtKod1.Text);
            add.Parameters.AddWithValue("@P16", TxtKod2.Text);
            add.Parameters.AddWithValue("@P17", TxtKod3.Text);
            add.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FirmaListesi();
            Clean();

        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where SEHIR=@p1", sql.Connection());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dataRow = komut.ExecuteReader();
            while (dataRow.Read())
            {
                Cmbilce.Properties.Items.Add(dataRow[0]);
            }
            sql.Connection().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand delete = new SqlCommand("Delete From TBL_FIRMALAR1 where ID=@id", sql.Connection());
            delete.Parameters.AddWithValue("@id", Txtid.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            FirmaListesi();
            MessageBox.Show("Firma listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            Clean();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand update = new SqlCommand("Update TBL_FIRMALAR1 set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9,IL=@P11,ILCE=@P12,FAX=@P10,VERGIDAIRE=@P13,ADRES=@P14,OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@P17 WHERE ID=@id", 
                sql.Connection());
            update.Parameters.AddWithValue("@P1", TxtAd.Text);
            update.Parameters.AddWithValue("@P2", TxtYetkiliGorev.Text);
            update.Parameters.AddWithValue("@P3", TxtYetkili.Text);
            update.Parameters.AddWithValue("@P4", MskYetkiliTC.Text);
            update.Parameters.AddWithValue("@P5", TxtSektor.Text);
            update.Parameters.AddWithValue("@P6", MskTelefon1.Text);
            update.Parameters.AddWithValue("@P7", MskTelefon2.Text);
            update.Parameters.AddWithValue("@P8", MskTelefon3.Text);
            update.Parameters.AddWithValue("@P9", TxtMail.Text);
            update.Parameters.AddWithValue("@P10", MskFax.Text);
            update.Parameters.AddWithValue("@P11", Cmbil.Text);
            update.Parameters.AddWithValue("@P12", Cmbilce.Text);
            update.Parameters.AddWithValue("@P13", TxtVergi.Text);
            update.Parameters.AddWithValue("@P14", RchAdres.Text);
            update.Parameters.AddWithValue("@P15", TxtKod1.Text);
            update.Parameters.AddWithValue("@P16", TxtKod2.Text);
            update.Parameters.AddWithValue("@P17", TxtKod3.Text);
            update.Parameters.AddWithValue("@id", Txtid.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            FirmaListesi();
            Clean();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Clean();
        }
    }
}
