using Microsoft.AspNetCore.Mvc;

namespace Music3_Api.Controllers.BaseController
{
    public class BaseAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
