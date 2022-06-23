
using DevRequired.Model;
using DevRequired.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DevRequired.Api.Controllers
{ 
    public class EmailController :BaseController
    {


        private readonly IDevloperStackService _devloperStackService;
        public EmailController(IDevloperStackService devloperStackService)
        {
            _devloperStackService = devloperStackService;
        }



        [HttpPost]
        [Route("Email")]
        public Task< bool> GetEmail(DevloperMaster devloperMasterDto)
        {
           
            var data =  _devloperStackService.GetDevRequired(devloperMasterDto);
           
            return data;
        }

    }
}
