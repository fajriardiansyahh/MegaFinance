using Microsoft.AspNetCore.Mvc;
using WebAPI.DataContext;

namespace WebAPI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _DatabaseContext;
        public AccountController(DatabaseContext databaseContext)
        {
            _DatabaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
