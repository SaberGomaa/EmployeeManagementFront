using AribMVC.DTOs;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Task = Models.Entities.Task;

namespace AribMVC.Controllers
{
    public class TasksController : Controller
    {

        private readonly HttpClient _httpClient;
        public TasksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BackendApi");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["token"];

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.GetAsync("/api/Task/GetAll");
            response.EnsureSuccessStatusCode();
            var departments = await response.Content.ReadFromJsonAsync<GResponse<List<Task>>>();

            return View(departments?.Data);
        }

        // GET: Controller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Controller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Controller/Create
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

        // GET: Controller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Controller/Edit/5
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

        // GET: Controller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Controller/Delete/5
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
