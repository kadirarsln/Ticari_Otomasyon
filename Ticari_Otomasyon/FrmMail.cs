using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            TxtMailAdres.Text = mail;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential("Mail", "Şifre");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.live.com";
            smtpClient.EnableSsl = true;
            message.To.Add(TxtMesaj.Text);
            message.From = new MailAddress("Mail");
            message.Subject = TxtKonu.Text;
            message.Body = TxtMesaj.Text;
            smtpClient.Send(message);
        }
    }
}
