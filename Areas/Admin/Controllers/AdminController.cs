using Microsoft.AspNetCore.Mvc;

namespace MusicStore.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
