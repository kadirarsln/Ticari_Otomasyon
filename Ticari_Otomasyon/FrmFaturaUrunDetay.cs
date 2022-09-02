using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmFaturaUrunDetay : Form
    {
        public FrmFaturaUrunDetay()
        {
            InitializeComponent();
        }
        public string id;
        SqlConnect sql = new SqlConnect();

        void Listele()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * From Tbl_FaturaDetay where FaturaID='" + id + "'", sql.Connection());
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        private void FrmFaturaUrunDetay_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme frmFaturaUrunDuzenleme = new FrmFaturaUrunDuzenleme();
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dataRow != null)
            {
                frmFaturaUrunDuzenleme.urunid = dataRow["FATURAURUNID"].ToString();
            }
            frmFaturaUrunDuzenleme.Show();
            //this.Hide();
        }
    }
}
