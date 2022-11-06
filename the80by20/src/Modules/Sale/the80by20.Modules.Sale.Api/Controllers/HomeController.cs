﻿using Microsoft.AspNetCore.Mvc;

namespace the80by20.Modules.Masterdata.Api.Controllers
{
    [Route(SaleModule.BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Sale API";
    }
}
