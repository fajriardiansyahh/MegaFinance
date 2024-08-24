using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using WebApplication.Models;
using WebApplication.Models.Users;

namespace WebApplication.Controllers
{
    [Route("BPKB/")]
    public class BPKBController : Controller
    {
        private Uri _url;
        private readonly HttpClient _httpClient;

        public BPKBController()
        {
            this._url = new Uri("https://localhost:44308/api/v1/");
            this._httpClient = new HttpClient();
            _httpClient.BaseAddress = _url;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public IActionResult Index()
        {
            UserLoginStateModel userLoginStateModel = HttpContext.Session.GetObject<UserLoginStateModel>("user");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userLoginStateModel.token);
            List<BPKBModel> BPKBModels = new List<BPKBModel>();
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + "BPKB/GetAll").Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                BPKBModels = JsonConvert.DeserializeObject<List<BPKBModel>>(data);
            }

            return View(BPKBModels);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(BPKBModel bPKBModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserLoginStateModel userLoginStateModel = HttpContext.Session.GetObject<UserLoginStateModel>("user");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userLoginStateModel.token);
                    bPKBModel.created_by = userLoginStateModel.user.user_id.ToString();
                    string json = JsonConvert.SerializeObject(bPKBModel);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(_httpClient.BaseAddress + "BPKB/AddBPKB", content);
                    string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", jsonResponse);
                        return View();
                    }

                    return RedirectToAction("Index", "BPKB");
                }

                ModelState.AddModelError("", "Error in Model creating BPKB  - Message: Model Error");
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error in creating BPKB  - Message: " + e.Message);
                throw;
            }
        }

        [Route("Edit")]
        public IActionResult Edit(string agreement_number)
        {
            BPKBModel BPKBModels = new BPKBModel();
            UserLoginStateModel userLoginStateModel = HttpContext.Session.GetObject<UserLoginStateModel>("user");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userLoginStateModel.token);
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(_httpClient.BaseAddress + "BPKB/GetByID?agreement_number=" + agreement_number).Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string data = httpResponseMessage.Content.ReadAsStringAsync().Result;
                BPKBModels = JsonConvert.DeserializeObject<BPKBModel>(data);
            }
            else
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized();
                }

                return View("PageNotFound");
            }

            return View(BPKBModels);
        }

        [HttpPost]
        public async Task<IActionResult> PostEdit(BPKBModel bPKBModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserLoginStateModel userLoginStateModel = HttpContext.Session.GetObject<UserLoginStateModel>("user");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userLoginStateModel.token);
                    bPKBModel.last_updated_by = userLoginStateModel.user.user_id.ToString();
                    string json = JsonConvert.SerializeObject(bPKBModel);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage httpResponseMessage = await _httpClient.PutAsync(_httpClient.BaseAddress + "BPKB/UpdateBPKB", content);
                    string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", jsonResponse);
                        return View();
                    }

                    return RedirectToAction("Index", "BPKB");
                }

                ModelState.AddModelError("", "Error in Model creating BPKB  - Message: Model Error");
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error in creating BPKB  - Message: " + e.Message);
                throw;
            }
        }
    }
}
