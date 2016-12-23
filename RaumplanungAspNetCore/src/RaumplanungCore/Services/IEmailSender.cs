using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;

namespace RaumplanungCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string username, string email, string subject, string message);
    }
}
