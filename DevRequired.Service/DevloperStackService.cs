
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
            int count = 1;
            if (devloperMaster.Stack == "")
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

                sb.Append("<table  style=width:95 %; margin-bottom: 1rem; gb(56, 52, 133); border-collapse: collapse; margin-bottom: 35px !important; margin-right: auto; margin-left: auto;'>");

                sb.Append($"<tr><th colspan= 3 style=text-align:left;padding-left:10px>Name :  {devloperMaster.Name}</th></tr>");
                sb.Append($"<tr><th colspan= 3 style=text-align:left;padding-left:10px>PhoneNo : {devloperMaster.PhoneNo}</th></tr>");

                sb.Append("<tr><th width=10% style='border-collapse:collapse;border:1px solid black';><h4> SrNo </h4></th>");
                sb.Append("<th width=50% style='border-collapse:collapse;border:1px solid black';><h4>Stack</h4></th>");
                sb.Append("<th width=20% style='border-collapse:collapse;border:1px solid black';><h4>Count</h4></th></tr> ");
                foreach (var item1 in datalist)
                {
                    sb.Append($"<tr><td style='border-collapse:collapse;border: 1px solid black;text-align:center; padding-top:5px; padding-bottom: 5px'; > {count} </td>" +
                        $"<td style = 'border-collapse:collapse;border:1px solid black;text-align:center;padding-top:5px; padding-bottom: 5px';> { item1.Key} </td>" +
                        $"<td style = 'border-collapse:collapse;border:1px solid black;text-align:right ;padding-top:5px; padding-bottom: 5px; padding-right: 30px;'>{ item1.Value}</td> ");
                    count++;
                }
                sb.Append("</table>");


                var result = await _emailService.SendEmailAsync(devloperMaster.Email, sb);

                if (result == true)
                    return "Email Sent Sucessfully";
                return "Failed to send Email";
            }
            return "Something went wrong..";

        }
    }
}
