using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DevRequired.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string to,  StringBuilder builder)
        {
           await Execute(to,  builder);
            return true;
        }
        public async Task Execute(string to, StringBuilder builder)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_configuration["MailSettings:Mail"], _configuration["MailSettings:DisplayName"])
                };
                to.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(t => mail.To.Add(new MailAddress(t)));
            
                mail.Body = builder.ToString();
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_configuration["MailSettings:Host"], Convert.ToInt32(_configuration["MailSettings:Port"])))
                {
                    smtp.Credentials = new NetworkCredential(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
