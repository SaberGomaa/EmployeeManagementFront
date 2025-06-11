using AribMVC.DTOs;
using DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AribMVC.Controllers
{
    public class UserController : Controller
    {

        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient, IHttpClientFactory httpClientFactory )
        {
            _httpClient = httpClient;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequset model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var apiUrl = "http://www.backend.somee.com/api/User/login";

            var response = await _httpClient.PostAsJsonAsync(apiUrl, model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GResponse<LoginResponse>>();

                if (result != null && result.IsSucceeded)
                {
                    string token = result.Data.token;
                    Response.Cookies.Append("token", token, new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    });
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Invalid credentials.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error connecting to login service.");
            }

            return View(model);
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
