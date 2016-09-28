using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uxsoft.Share.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, string to, string subject, string message);
    }
}
