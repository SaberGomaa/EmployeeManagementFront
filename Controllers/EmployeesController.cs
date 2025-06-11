using AribMVC.DTOs;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Net.Http;

namespace AribMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeesController(IHttpClientFactory httpClientFactory)
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

            var response = await _httpClient.GetAsync("/api/Employee/GetAll");
            response.EnsureSuccessStatusCode();
            var departments = await response.Content.ReadFromJsonAsync<GResponse<List<Employee>>>();

            return View(departments?.Data);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Employee Employee)
        {
            var token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            var response = await _httpClient.PutAsJsonAsync("/api/Employee/Update", Employee);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<GResponse<Employee>>();
            return RedirectToAction("Index");
        }

    }
}
