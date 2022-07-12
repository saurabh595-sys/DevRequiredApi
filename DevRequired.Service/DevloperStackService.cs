
using DevRequired.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevRequired.Service
{
    public class DevloperStackService : IDevloperStackService
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public DevloperStackService(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

      

        public async Task<string> GetRequiredDevAsync(DevloperMaster devloperMaster)
        {
            try
            {
                var sender = _configuration["MailSettings:sender"];
                await MailTemplate(devloperMaster, devloperMaster.Email, false);
                await MailTemplate(devloperMaster, sender, true);
                return "Email Send Sucessfully";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }


        public async Task<bool> MailTemplate(DevloperMaster devloper, string sender, bool flag)
        {

            int count = 1;
            if (devloper.Stack != "")
            {
                string[] list = devloper.Stack.Split('|');
                Dictionary<string, int> datalist = new Dictionary<string, int>();
                StringBuilder sb = new StringBuilder();
                foreach (var item in list)
                {
                    var value = item.Split(':');
                    for (int i = 0; i < value.Length; i++)
                    {
                        datalist.Add(value[i], Convert.ToInt32(value[i + 1]));
                        i += 1;
                    }
                }
                sb.Append("<p>Thank you for visiting our website and making your team.<br>");
                sb.Append("Below is the team which you want and we can get it started within 3 days.<br> We will send you profiles and rates of this team within the next 24 hours, once you approve it we can start within the next 2 days.</p><br>");
                sb.Append($"<p>Name :  {devloper.Name}</p>");
                sb.Append($"<p>PhoneNo : {devloper.PhoneNo}</p>");
                if (flag == true)
                {
                    sb.Append($"<p>Organization : {devloper.Organization}</p>");

                }
                sb.Append("<table style='border-collapse:collapse'><tr><th style='padding:12px 15px;border:1px solid black;text-align: center;font-size: 16px; background-color: blue;color:white;'> Sr.No </th>");
                sb.Append("<th style='padding:12px 15px;border:1px solid black;text-align: center;font-size: 16px; background-color: blue;color:white;'>Stack</th>");
                sb.Append("<th  style='padding:12px 15px;border:1px solid black;text-align: center;font-size: 16px; background-color: blue;color:white;'>Count</th></tr> ");
                foreach (var item1 in datalist)
                {
                    sb.Append($"<tr><td style='text-align: left; padding: 0.75rem;padding-left: 1.3rem; vertical-align: top;border: 1px solid black;color:black' > {count} </td>" +
                        $"<td style='text-align: left; padding: 0.75rem;padding-left: 1.3rem; vertical-align: top;border: 1px solid black;color:black'> { item1.Key} </td>" +
                        $"<td style='text-align: right; padding: 0.75rem;padding-left: 1.3rem; vertical-align: top;border: 1px solid black;color:black'>{ item1.Value}</td> </tr>");
                    count++;
                }
                sb.Append("</table>");

                return await _emailService.SendEmailAsync(sender, sb);
            }

            return false;
        }
    }
}
