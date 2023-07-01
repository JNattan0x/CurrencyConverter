using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public abstract class BaseController : Microsoft.AspNetCore.Mvc.Controller
    {
        protected readonly IServiceProvider _provider;

        public BaseController(IServiceProvider provider)
        {
            this._provider = provider;
        }
    }
}