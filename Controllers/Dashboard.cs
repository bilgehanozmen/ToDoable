
using Microsoft.AspNetCore.Mvc;

namespace ToDoable.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
