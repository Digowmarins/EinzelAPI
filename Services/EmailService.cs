using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Einzel.Services
{
    public class EmailService
    {
        public void SendEmail(string toEmail, string subject, string body, string nomeUsuario)
        {
            var fromAddress = new MailAddress("Seu e-mail (usar um hotmail/outlook)", "Einzel");
            var toAddress = new MailAddress(toEmail, nomeUsuario);
            const string fromPassword = "senha do seu e-mail";

            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

    }
}
