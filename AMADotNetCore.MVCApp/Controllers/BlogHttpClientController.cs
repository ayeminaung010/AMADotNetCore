using AMADotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AMADotNetCore.MVCApp.Controllers
{
	public class BlogHttpClientController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly IConfiguration _configuration;
		public BlogHttpClientController (HttpClient httpClient, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_configuration = configuration;
		}
		
		public async Task<IActionResult> Index()
		{
			var lst = new List<BlogDataModel>();
			//var url = $"{_configuration.GetSection("ApiUrl")}/api/blog";
			var response = await _httpClient.GetAsync("/api/Blog");
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr);
			}
			return View(lst);
		}
	}
}
