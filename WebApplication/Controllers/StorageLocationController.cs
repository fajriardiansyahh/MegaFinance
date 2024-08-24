using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class StorageLocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
