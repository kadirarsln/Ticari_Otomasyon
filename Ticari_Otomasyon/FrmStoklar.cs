using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }
        SqlConnect sql = new SqlConnect();

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 4);
            //chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 8);
            //chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 6);
            //chartControl1.Series["Series 1"].Points.AddPoint("Adana", 5);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select UrunAd,Sum(Adet) As 'Miktar' from Tbl_Urunler group by UrunAd", sql.Connection());
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable; 

            //Charta Stok Miktarı Listeleme
            SqlCommand komut = new SqlCommand("Select UrunAd,Sum(Adet) As 'Miktar' from TBL_URUNLER group by UrunAd", sql.Connection());
            SqlDataReader dataRow = komut.ExecuteReader();
            while (dataRow.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dataRow[0]), int.Parse(dataRow[1].ToString()));
            }
            sql.Connection().Close();

            //Charta Firma Şehir Sayısı Çekme
            SqlCommand komut2 = new SqlCommand("Select IL,Count(*) From TBL_FIRMALAR1 Group By IL", sql.Connection());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            sql.Connection().Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay frmStokDetay = new FrmStokDetay();
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                frmStokDetay.ad = dataRow["URUNAD"].ToString();
            }
            frmStokDetay.Show();
        }
    }
}
