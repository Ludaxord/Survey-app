using System;
using System.Net;
using System.Net.Mail;

namespace SurveyAPI.Helpers
{
    public class EmailClient
    {
        public void SendSMTPEmails(string emailDeliver, string subject, string body, string first_last_name) {

            var fromAddress = new MailAddress("konrad.survey.test@gmail.com", "Survey App");
            var toAddress = new MailAddress(emailDeliver, first_last_name);
            const string fromPassword = "TESTTESTTEST";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
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
