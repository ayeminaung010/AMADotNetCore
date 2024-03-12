using AMADotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace AMADotNetCore.MVCApp.Controllers
{
	public class BlogRestClientController : Controller
	{
		private readonly RestClient _restClient;
		private readonly IConfiguration _configuration;
		public BlogRestClientController(RestClient restClient, IConfiguration configuration)
		{
			_restClient = restClient;
			_configuration = configuration;
		}
		public async Task<IActionResult> Index()
		{
			var lst = new List<BlogDataModel>();

			RestRequest restRequest = new RestRequest("/api/blog",Method.Get);
			var response = await _restClient.ExecuteAsync(restRequest);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = response.Content!;
				lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr);
			}
			return View(lst);
		}
	}
}
