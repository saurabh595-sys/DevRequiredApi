using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using DevRequired.Model;

namespace DevRequired.Service
{
   public interface IDevloperStackService
    {
       Task< bool> GetDevRequired(DevloperMaster devloperMaster);
    }
}
