using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uxsoft.Share.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(SendGrid sg)
        {
            this.SendGrid = sg;
        }

        private SendGrid SendGrid { get; set; }

        public async Task SendEmailAsync(string from, string to, string subject, string message)
        {
            await SendGrid.SendMail(from, to, subject, "text/html", message);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
