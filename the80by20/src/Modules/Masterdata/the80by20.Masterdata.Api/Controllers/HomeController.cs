using Microsoft.AspNetCore.Mvc;

namespace the80by20.Modules.Masterdata.Api.Controllers
{
    [Route(MasterDataModule.BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Master Data API";
    }
}
