
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
        
        public async Task<bool> GetDevRequired(DevloperMaster devloperMaster)
        {
            int count = 1;
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

            sb.Append("<table border = 1><tr><th>Id</th>");
            sb.Append("<th>Stack</th>");
            sb.Append("<th>Count</th></tr>");
            foreach (var item1 in datalist)
            {
                sb.Append($"<tr><th>{count}</th><th>{item1.Key}</th><th>{item1.Value}</th></tr>");
                count++;
            }
            sb.Append("</table>");
            

            var result = await _emailService.SendEmailAsync(devloperMaster.Email, sb);
            return true;
        }

       
    }
}
