using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        SqlConnect sql = new SqlConnect();
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            Stoklar();

            Ajanda();

            FirmaHareketleri();

            Fihrist();

            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");

            Haberler();
        }
        void Stoklar()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select Urunad,Sum(Adet) as 'Adet' From TBL_URUNLER group by Urunad having Sum(adet)<=20 order by Sum(adet)", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            GridControlStoklar.DataSource = dataTable;
        }

        void Ajanda()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select top 10 NOTTARIH,NOTSAAT,NOTBASLIK From TBL_NOTLAR order by NOTID desc", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControlAjanda.DataSource = dataTable;
        }

        void FirmaHareketleri()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Exec FirmaSon10", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControlFirmaHareket.DataSource = dataTable;
        }

        void Fihrist()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select AD,TELEFON1 From TBL_FIRMALAR1", sql.Connection());
            sqlDataAdapter.Fill(dataTable);
            gridControlFihrist.DataSource = dataTable;
        }

        void Haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("http://haberturk.com/rss");
            while (xmloku.Read())
            {
                if (xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }



    }
}
