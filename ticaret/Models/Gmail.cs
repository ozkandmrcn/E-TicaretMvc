using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ticaret.Models
{
    public class Gmail
    {
        public static void SendMail(string body)
        {
            var fromAddress = new MailAddress("ozkandemiircan@gmail.com", "Güvenlik Kontrolü");
            var toAddress = new MailAddress("ozkandemiircan@gmail.com");
            const string subject = "Güvenlik Kontrolü";


            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "Ozkan5757*")

            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    smtp.Send(message);
                }
            }
        }

    }
}