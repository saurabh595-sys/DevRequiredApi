
using Api;
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
    public class DevStackController :BaseController
    {


        private readonly IDevloperStackService _devloperStackService;
        public DevStackController(IDevloperStackService devloperStackService)
        {
            _devloperStackService = devloperStackService;
        }



        [HttpPost]
        [Route("Email")]
        public async  Task<string>GetDevRequiredEmail(DevloperMaster devloperMasterDto)
        {
           
            var data = await _devloperStackService.GetRequiredDevAsync(devloperMasterDto);
           
            return data;
        }

    }
}
