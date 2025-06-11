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

        [HttpPost]
        public async Task<IActionResult> Update(Task Task)
        {
            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.PutAsJsonAsync("/api/Task/Update", Task);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GResponse<Task>>();
            return RedirectToAction("Index");
        }
    }
}
