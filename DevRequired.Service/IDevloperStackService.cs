using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using DevRequired.Model;

namespace DevRequired.Service
{
   public interface IDevloperStackService
    {
       Task<string> GetRequiredDevAsync(DevloperMaster devloperMaster);
    }
}
