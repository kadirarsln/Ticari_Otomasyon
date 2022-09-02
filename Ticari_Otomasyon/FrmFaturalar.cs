using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        SqlConnect sql = new SqlConnect();

        void Listele()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from TBL_FATURABILGI", sql.Connection());
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        void Clean()
        {
            TxtAlici.Text = "";
            Txtid.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            TxtTeslimAlan.Text = "";
            TxtTeslimEden.Text = "";
            TxtVergiDairesi.Text = "";
            MskSaat.Text = "";
            MskTarih.Text = "";
        }

        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            Listele();

            Clean();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {

            if (TxtFaturaid.Text == "")
            {

                SqlCommand add = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES " +
                                                "(@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", sql.Connection());
                add.Parameters.AddWithValue("@P1", TxtSeri.Text);
                add.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
                add.Parameters.AddWithValue("@P3", MskTarih.Text);
                add.Parameters.AddWithValue("@P4", MskSaat.Text);
                add.Parameters.AddWithValue("@P5", TxtVergiDairesi.Text);
                add.Parameters.AddWithValue("@P6", TxtAlici.Text);
                add.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
                add.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
                add.ExecuteNonQuery();
                sql.Connection().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
            //FİRMA CARİSİ
            if (TxtFaturaid.Text != "" && comboBox1.Text == "Firma")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values " +
                                                   "(@p1,@p2,@p3,@p4,@p5)", sql.Connection());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaid.Text);
                komut2.ExecuteNonQuery();
                sql.Connection().Close();

                //Hareket Tablosuna Veri Girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_FIRMAHAREKETLER (Urunıd,adet,personel,fırma,fıyat,toplam,faturaıd,tarıh) values " +
                                                   "(@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", sql.Connection());
                komut3.Parameters.AddWithValue("@h1", TxtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFırma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                sql.Connection().Close();

                //Stok Sayısını Azaltma
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set adet=adet-@miktar where Id=@id", sql.Connection());
                komut4.Parameters.AddWithValue("@miktrar", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@id", TxtUrunid.Text);
                komut4.ExecuteNonQuery();
                sql.Connection().Close();
                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //MUŞTERİ CARİSİ
            if (TxtFaturaid.Text != "" && comboBox1.Text == "Müşteri")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values " +
                                                   "(@p1,@p2,@p3,@p4,@p5)", sql.Connection());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaid.Text);
                komut2.ExecuteNonQuery();
                sql.Connection().Close();

                //Hareket Tablosuna Veri Girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_MUSTERIHAREKETLER (URUNID,ADET,PERSONEŞ,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) values " +
                                                   "(@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", sql.Connection());
                komut3.Parameters.AddWithValue("@h1", TxtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFırma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                sql.Connection().Close();

                //Stok Sayısını Azaltma
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set adet=adet-@miktar where Id=@id", sql.Connection());
                komut4.Parameters.AddWithValue("@Mmiktar", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@id", TxtUrunid.Text);
                komut4.ExecuteNonQuery();
                sql.Connection().Close();
                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSil_Click_1(object sender, EventArgs e)
        {
            Delete();
            Clean();
            Listele();
        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            Update();
            Clean();
            Listele();
        }
        private void BtnTemizle_Click_1(object sender, EventArgs e)
        {
            Clean();
        }
        private void BtnBul_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay frmFaturaUrunDetay = new FrmFaturaUrunDetay();
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                frmFaturaUrunDetay.id = dataRow["FATURABILGIID"].ToString();
            }
            frmFaturaUrunDetay.Show();
        }

        void Delete()
        {
            SqlCommand delete = new SqlCommand("Delete From TBL_FATURABILGI where FATURABILGIID=@faturaBilgiID", sql.Connection());
            delete.Parameters.AddWithValue("@faturaBilgiID", Txtid.Text);
            delete.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Fatura Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        void Update()
        {
            SqlCommand update = new SqlCommand("update TBL_FATURABILGI set SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 WHERE FATURABILGIID=@faturaBilgiID", sql.Connection());
            update.Parameters.AddWithValue("@P1", TxtSeri.Text);
            update.Parameters.AddWithValue("@P2", TxtSiraNo.Text);
            update.Parameters.AddWithValue("@P3", MskTarih.Text);
            update.Parameters.AddWithValue("@P4", MskSaat.Text);
            update.Parameters.AddWithValue("@P5", TxtVergiDairesi.Text);
            update.Parameters.AddWithValue("@P6", TxtAlici.Text);
            update.Parameters.AddWithValue("@P7", TxtTeslimEden.Text);
            update.Parameters.AddWithValue("@P8", TxtTeslimAlan.Text);
            update.Parameters.AddWithValue("@faturaBilgiID", Txtid.Text);
            update.ExecuteNonQuery();
            sql.Connection().Close();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        void Search()
        {
            SqlCommand search = new SqlCommand("Select URUNAD,SATISFIYAT from TBL_URUNLER where Id=@id", sql.Connection());
            search.Parameters.AddWithValue("@id", TxtUrunid.Text);
            SqlDataReader dataRow = search.ExecuteReader();
            while (dataRow.Read())
            {
                TxtUrunAd.Text = dataRow[0].ToString();
                TxtFiyat.Text = dataRow[1].ToString();
            }
            sql.Connection().Close();
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                Txtid.Text = dataRow["FATURABILGIID"].ToString();
                TxtSiraNo.Text = dataRow["SIRANO"].ToString();
                TxtSeri.Text = dataRow["SERI"].ToString();
                MskTarih.Text = dataRow["TARIH"].ToString();
                MskSaat.Text = dataRow["SAAT"].ToString();
                TxtAlici.Text = dataRow["ALICI"].ToString();
                TxtTeslimEden.Text = dataRow["TESLIMEDEN"].ToString();
                TxtTeslimAlan.Text = dataRow["TESLIMALAN"].ToString();
                TxtVergiDairesi.Text = dataRow["VERGIDAIRE"].ToString();
            }
        }
    }
}
