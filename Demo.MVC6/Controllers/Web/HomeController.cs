using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Demo.MVC6.Controllers.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello!!!");
        }
    }
}
