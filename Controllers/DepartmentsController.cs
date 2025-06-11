using AribMVC.DTOs;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace AribMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly HttpClient _httpClient;
        public DepartmentsController(IHttpClientFactory httpClientFactory)
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

            var response = await _httpClient.GetAsync("/api/Department/GetAll");
            response.EnsureSuccessStatusCode();
            var departments = await response.Content.ReadFromJsonAsync<GResponse<List<Department>>>();

            return View(departments?.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Department department)
        {
            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.PutAsJsonAsync("/api/Department/Update", department);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GResponse<Department>>();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentAddDTO department)
        {
            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.PostAsJsonAsync("/api/Department/Add", department);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GResponse<Department>>();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.DeleteAsync($"/api/Department/Delete/{id}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GResponse<bool>>();
            return RedirectToAction("Index");
        }

    }
}
