using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        SqlConnect sql = new SqlConnect();

        void musterihareket()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Execute MusteriHareketler", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        void firmahareket()
        {
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("Execute FirmaHareketler", sql.Connection());
            sqlDataAdapter2.Fill(dataTable2);
            gridControl3.DataSource = dataTable2;
        }

        void Giderler()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From TBL_GIDERLER order by ID asc", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl2.DataSource = dataTable;
        }


        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {

            LblAktifKullanici.Text = ad;

            musterihareket();

            firmahareket();

            Giderler();

            //Toplam Tutarı Hesaplama
            SqlCommand komut1 = new SqlCommand("Select Sum(Tutar) From TBL_FATURADETAY", sql.Connection());
            SqlDataReader sqlDataReader1 = komut1.ExecuteReader();
            while (sqlDataReader1.Read())
            {
                LblKasaToplam.Text = sqlDataReader1[0].ToString() + " TL";
            }
            sql.Connection().Close();

            //Son ayın faturaları
            SqlCommand komut2 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", sql.Connection());
            SqlDataReader sqlDataReader2 = komut2.ExecuteReader();
            while (sqlDataReader2.Read())
            {
                LblOdemeler.Text = sqlDataReader2[0].ToString() + " TL";
            }
            sql.Connection().Close();

            //Son ayın personel maaşları
            SqlCommand komut3 = new SqlCommand("Select Maaslar From TBL_GIDERLER order by ID asc", sql.Connection());
            SqlDataReader sqlDataReader3 = komut3.ExecuteReader();
            while (sqlDataReader3.Read())
            {
                LblPersonelMaasları.Text = sqlDataReader3[0].ToString() + " TL";
            }
            sql.Connection().Close();

            //Toplam Müşteri Sayısı
            SqlCommand komut4 = new SqlCommand("Select Count(*) From Tbl_Musterıler", sql.Connection());
            SqlDataReader sqlDataReader4 = komut4.ExecuteReader();
            while (sqlDataReader4.Read())
            {
                LblMusteriSayisi.Text = sqlDataReader4[0].ToString();
            }
            sql.Connection().Close();

            //Toplam Firma Sayısı
            SqlCommand komut5 = new SqlCommand("Select Count(*) From TBL_FIRMALAR1", sql.Connection());
            SqlDataReader sqlDataReader5 = komut5.ExecuteReader();
            while (sqlDataReader5.Read())
            {
                LblFirmaSayisi.Text = sqlDataReader5[0].ToString();
            }
            sql.Connection().Close();

            //Toplam Firma Şehir Sayısı
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) From TBL_FIRMALAR1", sql.Connection());
            SqlDataReader sqlDataReader6 = komut6.ExecuteReader();
            while (sqlDataReader6.Read())
            {LblSehirSayısı.Text = sqlDataReader6[0].ToString();
            }
            sql.Connection().Close();

            //Toplam Müşteri Şehir Sayısı
            SqlCommand komut7 = new SqlCommand("Select Count(Distinct(IL)) From TBL_MUSTERILER", sql.Connection());
            SqlDataReader sqlDataReader7 = komut7.ExecuteReader();
            while (sqlDataReader7.Read())
            {
                LblSehirSayisi2.Text = sqlDataReader7[0].ToString();
            }
            sql.Connection().Close();

            //Toplam Personel Sayısı
            SqlCommand komut8 = new SqlCommand("Select Count(*) From TBL_PERSONELLER", sql.Connection());
            SqlDataReader sqlDataReader8 = komut8.ExecuteReader();
            while (sqlDataReader8.Read())
            {
                LblPersonelSayisi.Text = sqlDataReader8[0].ToString();
            }
            sql.Connection().Close();

            //Toplam Ürün Sayısı
            SqlCommand komut9 = new SqlCommand("Select Sum(Adet) From TBL_URUNLER", sql.Connection());
            SqlDataReader sqlDataReader9 = komut9.ExecuteReader();
            while (sqlDataReader9.Read())
            {
                LblStokSayisi.Text = sqlDataReader9[0].ToString();
            }
            sql.Connection().Close();
            
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;

            //Elektrik
            if (sayac > 0 && sayac <= 5)
            {
                groupControl10.Text = "Elektrik";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 Ay,Elektrık from TBL_GIDERLER order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader10 = komut10.ExecuteReader();
                while (sqlDataReader10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader10[0], sqlDataReader10[1]));
                }
                sql.Connection().Close();
            }

            //Su
            if (sayac > 5 && sayac <= 10)
            {
                groupControl10.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,Su from Tbl_Gıderler order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader11 = komut11.ExecuteReader();
                while (sqlDataReader11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader11[0], sqlDataReader11[1]));
                }
                sql.Connection().Close();
            }

            //Doğalgaz
            if (sayac > 10 && sayac <= 15)
            {
                groupControl10.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,Dogalgaz from Tbl_Gıderler order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader11 = komut11.ExecuteReader();
                while (sqlDataReader11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader11[0], sqlDataReader11[1]));
                }
                sql.Connection().Close();
            }

            //İnternet
            if (sayac > 15 && sayac <= 20)
            {
                groupControl10.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,INTERNET from Tbl_Gıderler order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader11 = komut11.ExecuteReader();
                while (sqlDataReader11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader11[0], sqlDataReader11[1]));
                }
                sql.Connection().Close();
            }

            //Ekstra
            if (sayac > 20 && sayac <= 25)
            {
                groupControl10.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,Ekstra from Tbl_Gıderler order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader11 = komut11.ExecuteReader();
                while (sqlDataReader11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader11[0], sqlDataReader11[1]));
                }
                sql.Connection().Close();
            }
            if (sayac == 26)
            {
                sayac = 0;
            }
        }
        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;

            //Elektrik
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl11.Text = "Elektrik";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("Select top 4 Ay,Elektrık from TBL_GIDERLER order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader10 = komut10.ExecuteReader();
                while (sqlDataReader10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader10[0], sqlDataReader10[1]));
                }
                sql.Connection().Close();
            }

            //Su
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl11.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,Su from Tbl_Gıderler order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader11 = komut11.ExecuteReader();
                while (sqlDataReader11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader11[0], sqlDataReader11[1]));
                }
                sql.Connection().Close();
            }

            //Doğalgaz
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl11.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,Dogalgaz from Tbl_Gıderler order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader11 = komut11.ExecuteReader();
                while (sqlDataReader11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader11[0], sqlDataReader11[1]));
                }
                sql.Connection().Close();
            }

            //İnternet
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl11.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,INTERNET from Tbl_Gıderler order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader11 = komut11.ExecuteReader();
                while (sqlDataReader11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader11[0], sqlDataReader11[1]));
                }
                sql.Connection().Close();
            }

            //Ekstra
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl11.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();
                //Chart Controle Su Faturası Son 4 Ay Listeleme
                SqlCommand komut11 = new SqlCommand("Select Top 4 Ay,Ekstra from TBL_GIDERLER order by ID Desc", sql.Connection());
                SqlDataReader sqlDataReader11 = komut11.ExecuteReader();
                while (sqlDataReader11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(sqlDataReader11[0], sqlDataReader11[1]));
                }
                sql.Connection().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
