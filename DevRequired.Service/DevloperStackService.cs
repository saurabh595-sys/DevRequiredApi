
using DevRequired.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevRequired.Service
{
    public class DevloperStackService : IDevloperStackService
    {
        private readonly IEmailService _emailService;
        public DevloperStackService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<string> GetRequiredDevAsync(DevloperMaster devloperMaster)
        {
            try {

                int count = 1;
                if (devloperMaster.Stack != "")
                {
                    string[] list = devloperMaster.Stack.Split('|');
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
                    sb.Append("<p>Thank you for visiting our website and making our team.<br>");
                    sb.Append("Below is the team which you want and we can get it started within 3 days.<br> We will send you profiles and rates of this team within the next 24 hours, once you approve it we can start within the next 2 days.</p><br>");
                    sb.Append($"<p>Name :  {devloperMaster.Name}</p>");
                    sb.Append($"<p>PhoneNo : {devloperMaster.PhoneNo}</p>");
                    sb.Append("<table style='width: 95 %; margin-bottom: 1rem; color: #212529; border-collapse: collapse; margin-bottom: 35px !important; margin-right: auto; margin-left: auto;'>");
                    sb.Append("<tr style='  color: #f8f9fa !important;'><th style='background-color: #5d78ff;text-align: left; padding: 0.75rem; vertical-align: top; border: 1px solid #dee2e6;'>No.</th>");
                    sb.Append("<th style='background-color: #5d78ff;text-align: left; padding: 0.75rem; vertical-align: top; border: 1px solid #dee2e6;'>Stack</th>");
                    sb.Append("<th style='background-color: #5d78ff;text-align: left; padding: 0.75rem; vertical-align: top; border: 1px solid #dee2e6;'>Count</th></tr> ");
                    foreach (var item1 in datalist)
                    {
                        sb.Append($"<tr  style=' color: #f8f9fa !important;'><td style='text-align: left; padding: 0.75rem; vertical-align: top; border-bottom: 1px solid #dee2e6;color:black' > {count} </td>" +
                            $"<td style='text-align: left; padding: 0.75rem; vertical-align: top; border-bottom: 1px solid #dee2e6;color:black'> { item1.Key} </td>" +
                            $"<td style='text-align: right; padding: 0.75rem 1.4rem 0.75rem 0.75rem; vertical-align: top; border-bottom: 1px solid #dee2e6;color:black'>{ item1.Value}</td> ");
                        count++;
                    }
                   
                    sb.Append("</table>");
                 
                    var result = await _emailService.SendEmailAsync(devloperMaster.Email, sb);
                     return "Email Sent Sucessfully";



                }
                else
                {
                    return "Failed to send Email";
                }
               
            }
            catch(Exception ex)
            {
                return ex.ToString(); ;
            }

           
        }
    }
}
