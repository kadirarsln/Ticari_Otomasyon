using System;
using System.Data;
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

        SqlConnect sql = new SqlConnect();

        void BankaBilgisiListele()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Execute BankaBilgileri", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        void SEHIRListesi()
        {
            SqlCommand SEHIRListele = new SqlCommand("Select SEHIR From TBL_ILLER", sql.Connection());
            SqlDataReader sqlDataReader = SEHIRListele.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Cmbil.Properties.Items.Add(sqlDataReader[0]);
            }

            sql.Connection().Close();
        }

        void FirmaListesi()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select ID,AD From TBL_FIRMALAR1", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dataTable;
        }


        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            BankaBilgisiListele();

            SEHIRListesi();

            FirmaListesi();

            Clean();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            Add();
            BankaBilgisiListele();
            Clean();
        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            Clean();
        }
        private void BtnSil_Click(object sender, EventArgs e)
        {
            Delete();
            BankaBilgisiListele();
        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            Update();
            BankaBilgisiListele();
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("Select IlCE from TBL_ILCELER where SEHIR=@ilce", sql.Connection());
            komut.Parameters.AddWithValue("@ilce", Cmbil.SelectedIndex + 1);
            SqlDataReader dataRow = komut.ExecuteReader();
            while (dataRow.Read())
            {
                Cmbilce.Properties.Items.Add(dataRow[0]);
            }

            sql.Connection().Close();
        }


        void Add()
        {
            SqlCommand add = new SqlCommand(
                "insert into TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values " +
                "(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", sql.Connection());
            add.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            add.Parameters.AddWithValue("@p2", Cmbil.Text);
            add.Parameters.AddWithValue("@p3", Cmbilce.Text);
            add.Parameters.AddWithValue("@p4", TxtSube.Text);
            add.Parameters.AddWithValue("@p5", TxtIBAN.Text);
            add.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            add.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            add.Parameters.AddWithValue("@p8", MskTelefon.Text);
            add.Parameters.AddWithValue("@p9", MskTarih.Text);
            add.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            add.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            add.ExecuteNonQuery();
            BankaBilgisiListele();
            sql.Connection().Close();
            MessageBox.Show("Banka Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        void Delete()
        {
            SqlCommand delete = new SqlCommand("delete from TBL_BANKALAR where ID=@id", sql.Connection());
            delete.Parameters.AddWithValue("@id", Txtid.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            Clean();
            MessageBox.Show("Banka Bilgisi Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            BankaBilgisiListele();
        }

        void Update()
        {
            SqlCommand update =
                new SqlCommand(
                    "update TBL_BANKALAR set BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 WHERE ID=@id",
                    sql.Connection());
            update.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            update.Parameters.AddWithValue("@p2", Cmbil.Text);
            update.Parameters.AddWithValue("@p3", Cmbilce.Text);
            update.Parameters.AddWithValue("@p4", TxtSube.Text);
            update.Parameters.AddWithValue("@p5", TxtIBAN.Text);
            update.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            update.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            update.Parameters.AddWithValue("@p8", MskTelefon.Text);
            update.Parameters.AddWithValue("@p9", MskTarih.Text);
            update.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            update.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            update.Parameters.AddWithValue("@id", Txtid.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Banka Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void Clean()
        {
            TxtBankaAd.Text = "";
            TxtHesapNo.Text = "";
            TxtHesapTuru.Text = "";
            TxtIBAN.Text = "";
            Txtid.Text = "";
            TxtSube.Text = "";
            TxtYetkili.Text = "";
            MskTarih.Text = "";
            MskTelefon.Text = "";
            lookUpEdit1.Text = "";
        }

        private void gridView1_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dataRow != null)
            {
                Txtid.Text = dataRow["ID"].ToString();
                TxtBankaAd.Text = dataRow["BANKAADI"].ToString();
                Cmbil.Text = dataRow["IL"].ToString();
                Cmbilce.Text = dataRow["ILCE"].ToString();
                TxtSube.Text = dataRow["SUBE"].ToString();
                TxtIBAN.Text = dataRow["IBAN"].ToString();
                TxtHesapNo.Text = dataRow["HESAPNO"].ToString();
                TxtYetkili.Text = dataRow["YETKILI"].ToString();
                MskTelefon.Text = dataRow["TELEFON"].ToString();
                MskTarih.Text = dataRow["TARIH"].ToString();
                TxtHesapTuru.Text = dataRow["HESAPTURU"].ToString();
            }
        }
    }
}
