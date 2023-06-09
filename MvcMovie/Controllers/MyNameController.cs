using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class MyNameController : Controller
    {
        public IActionResult Index()
        {

           
                
                string username = HttpContext.Request.Cookies["username"];

                ViewBag.name = username;




            return View();
        }
    }
}
