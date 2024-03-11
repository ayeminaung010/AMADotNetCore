using Microsoft.AspNetCore.Mvc;

namespace AMADotNetCore.MVCApp.Controllers
{
    public class HighChartController : Controller
    {
        public IActionResult PieChart()
        {

            return View();
        }
    }
}
