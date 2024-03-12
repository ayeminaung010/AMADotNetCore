using AMADotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace AMADotNetCore.MVCApp.Controllers
{
	public class BlogRefitController : Controller
	{
		private readonly IBlogApi _blogApi;
		private readonly IConfiguration _configuration;
		public BlogRefitController(IBlogApi blogApi, IConfiguration configuration)
		{
			_blogApi = blogApi;
			_configuration = configuration;
		}
		public async Task<IActionResult> Index()
		{
			var lst = new List<BlogDataModel>();
			lst = await _blogApi.GetBlogs();
			return View(lst);
		}
	}
}
