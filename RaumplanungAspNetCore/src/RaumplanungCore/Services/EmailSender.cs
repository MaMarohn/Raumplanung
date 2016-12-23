using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail.Abstractions;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Threading;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace RaumplanungCore.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string username, string email, string subject, string mailmessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Raumplanung", "raumplanunghda@gmail.com"));
            message.To.Add(new MailboxAddress(username, email));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = @mailmessage;
            message.Body = bodyBuilder.ToMessageBody();
           

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.googlemail.com", 587, SecureSocketOptions.Auto);
                
                client.Authenticate("raumplanunghda@gmail.com", "Raumplanung123", default(CancellationToken));
                
                // Note: since we don't have an OAuth2 token, disable 	// the XOAUTH2 authentication mechanism.     client.Authenticate("anuraj.p@example.com", "password");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
