using AMADotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMADotNetCore.MVCApp.Controllers
{
    public class ApexChartsController : Controller
    {
        public IActionResult PieChart()
        {
            var model = new PieChartModel()
            {
                Series = new List<int> { 44, 55, 13, 43, 22 },
                Labels = new List<string> { "Team A", "Team B", "Team C", "Team D", "Team E" }
            };
            return View(model);
        }
    }
}
