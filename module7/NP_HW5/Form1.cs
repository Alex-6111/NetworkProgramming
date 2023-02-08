using System.Net.Mail;
using System.Net;

namespace NP_HW5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(textBoxYourEmail.Text);
                mail.To.Add(textBoxTo.Text);
                mail.Subject = textBoxSubject.Text;
                mail.Body = textBoxMessage.Text;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mail.From.Address, textBoxYourPassword.Text);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                MessageBox.Show("mail Send"); 
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}