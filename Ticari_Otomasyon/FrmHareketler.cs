using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }

        SqlConnect sql = new SqlConnect();

        void FirmaHareketleri()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Exec FirmaHareketler", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl2.DataSource = dataTable;
        }

        void MusteriHareketleri()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Exec MusteriHareketler", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }


        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            FirmaHareketleri();

            MusteriHareketleri();
        }
    }
}
