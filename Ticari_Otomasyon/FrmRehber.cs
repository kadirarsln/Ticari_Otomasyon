using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        SqlConnect sql = new SqlConnect();

        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //Müşteri Bilgileri
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select AD,SOYAD,TELEFON,TELEFON2,MAIL from TBL_MUSTERILER", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControl1.DataSource = dataTable;

            //Firma Bilgileri
            DataTable dataTable2 = new DataTable();
            SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter("Select AD,YETKILIADSOYAD,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX from TBL_FIRMALAR1", sql.Connection());
            sqlDataAdapter2.Fill(dataTable2);
            gridControl2.DataSource = dataTable2;
        }
        
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmMail = new FrmMail();
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dataRow != null)
            {
                frmMail.mail = dataRow["MAIL"].ToString();
            }
            frmMail.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail frmMail = new FrmMail();
            DataRow dataRow = gridView2.GetDataRow(gridView2.FocusedRowHandle);

            if (dataRow != null)
            {
                frmMail.mail = dataRow["MAIL"].ToString();
            }
            frmMail.Show();
        }
    }
}
