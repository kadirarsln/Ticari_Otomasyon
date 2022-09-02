using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

         SqlConnect sql = new SqlConnect();

        private void button1_MouseHover(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.Yellow;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.LightSeaGreen;
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("Select * From TBL_ADMIN where KullaniciAd=@kullaniciAd and sifre=@sifre", sql.Connection());
            sqlCommand.Parameters.AddWithValue("@kullaniciAd", TxtKullaniciAd.Text);
            sqlCommand.Parameters.AddWithValue("@sifre", TxtSifre.Text);
            SqlDataReader sqlDataReader= sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                FrmAnaModul frmAnaModul = new FrmAnaModul();
                frmAnaModul.kullanici = TxtKullaniciAd.Text;
                frmAnaModul.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            sql.Connection().Close();
        }

    }
}
