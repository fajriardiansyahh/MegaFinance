using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApplication.Models;
using WebApplication.Models.Users;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this._httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            var userLogin = HttpContext.Session.GetObject<UserLoginStateModel>("user");

            if (userLogin == null)
            {
                return View();
            }

            return RedirectToAction("Index", "BPKB");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string json = JsonConvert.SerializeObject(loginViewModel);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(new Uri("https://localhost:44308/api/v1/") + "Account/Login", content);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                        if (jsonResponse == null)
                        {
                            ModelState.AddModelError("", "Username or Password wrong");
                            return View("Home");
                        }
                        else
                        {
                            UserLoginStateModel result = JsonConvert.DeserializeObject<UserLoginStateModel>(jsonResponse);
                            HttpContext.Session.SetObject("user", result);
                            return RedirectToAction("Index", "BPKB");
                        }
                    }

                    ModelState.AddModelError("", "Username or Password wrong");
                    return RedirectToAction("Index", "Home");
                }

                return View("PageError");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error in cloud - GetPLUInfo" + e.Message);
                throw;
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }
    }
}
