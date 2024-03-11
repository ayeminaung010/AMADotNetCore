using AMADotNetCore.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AMADotNetCore.MVCApp.Controllers
{
    public class ChartJsController : Controller
    {
        public IActionResult ColumnChart()
        {
            var model = new CoulmnChartModel()
            {
                Labels = new List<string> { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" },
                Data = new List<int> { 12, 19, 3, 5, 2, 3 }
            };
            return View(model);
        }
    }
}
