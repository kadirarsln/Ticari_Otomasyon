using System;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmNotDetay : Form
    {
        public FrmNotDetay()
        {
            InitializeComponent();
        }

        public string metin;

        private void FrmNotDetay_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = metin;
        }
    }
}
