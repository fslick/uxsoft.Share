using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace uxsoft.Share.Services
{
    public class SendGrid
    {
        public SendGrid(string apiUrl, string apiKey)
        {
            Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            this.SendMailApiUrl = apiUrl;
        }
        private HttpClient Http { get; set; } = new HttpClient();
        private string SendMailApiUrl { get; set; }

        public async Task<bool> SendMail(string from, string to, string subject, string contentType, string content)
        {
            var json = new
            {
                personalizations = new[]
                {
                    new {
                        to = new[] { new { email = to } },
                        subject = subject
                    }
                },
                from = new { email = from },
                content = new[] { new { type = contentType, value = content } }
            };
            var body = JObject.FromObject(json).ToString();

            var response = await Http.PostAsync(SendMailApiUrl, new StringContent(body, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }
    }
}
