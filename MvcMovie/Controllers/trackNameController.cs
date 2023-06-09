using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class trackNameController : Controller
    {
        public IActionResult Index(string name)
        {

            if (name != null)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                HttpContext.Response.Cookies.Append("username", name, options);
                return Redirect("/myname");
            }
            return View();


          
        }
    }
}
