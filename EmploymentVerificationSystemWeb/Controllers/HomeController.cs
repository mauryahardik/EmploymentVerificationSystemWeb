using EmploymentVerificationSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using EmploymentVerificationSystemWeb.Models.Request;
using EmploymentVerificationSystemWeb.Models.ResponseMessage;
using Microsoft.Extensions.Configuration;

namespace EmploymentVerificationSystemWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IConfiguration  configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration= configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyEmployment([FromBody] EmploymentVerificationRequest request)
        {
            if (ModelState.IsValid)
            {
                var api = _configuration.GetValue<string>("ApiUrl");
                var jsonRequest = JsonSerializer.Serialize(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(""+api+"api/verify-employment", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ResponseMessageDto>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Ok(result);
                }
                else
                {
                    // Handle the error response as needed
                    throw new Exception($"Error calling API: {response.ReasonPhrase}");
                }
            }
            else
            {
                return BadRequest("Invalid data.");
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
