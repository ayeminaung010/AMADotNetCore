using Microsoft.AspNetCore.Mvc;

namespace AMADotNetCore.MVCApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult PieChart()
        {
            return View();
        }
    }
}
