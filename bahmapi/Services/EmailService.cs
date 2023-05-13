using System.Net;
using System.Net.Mail;

namespace bahmapi.Services
{
    public partial class EmailService
    {
        public void EnviaEmail(string Para, string Copia, string Assunto, string Mensagem)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = false;
            smtpClient.Host = "";
            smtpClient.Port = 0;
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("", "");
            MailMessage mailMessage = new MailMessage("", Para, Assunto, Mensagem);
            if (Copia != "")
            {
                mailMessage.Bcc.Add(Copia);
            }
            smtpClient.Send(mailMessage);
        }
    }
}