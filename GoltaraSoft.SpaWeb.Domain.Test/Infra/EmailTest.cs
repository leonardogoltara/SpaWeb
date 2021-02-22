using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Mail;

namespace GoltaraSolutions.SpaWeb.Domain.Test.Infra
{
    [TestClass]
    public class EmailTest
    {
       // [TestMethod]
        public void SendEmail()
        {
            //< add key = "EmailHostSMTP" value = "smtpout.secureserver.net" />
            //< add key = "EmailPorta" value = "465" />
            //< add key = "EmailUserName" value = "noreplay@goltarasys.com" />
            //< add key = "EmailSenha" value = "4658@Goltarasys" />
            //< add key = "EmailSSL" value = "1" />

            var fromAddress = new MailAddress("noreplay@goltarasys.com", "From Name");
            var toAddress = new MailAddress("lsgolt94@gmail.com", "To Name");
            string fromPassword = "4658@Goltarasys";
            string subject = "Subject";
            string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtpout.secureserver.net",
                Port = 80,
                EnableSsl = false,
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
