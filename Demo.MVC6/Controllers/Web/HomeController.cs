using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Infra.Events;

namespace Demo.MVC6.Controllers.Web
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            await new Services.Events.Base().RaiseAsync();
            await new Services.Events.Derived().RaiseAsync();

            return Content(Startup.Configuration["AppSettings:Greeting"]);
        }

        public IActionResult Library()
        {
            return View();
        }

        public IActionResult BookCatalog()
        {
            return ViewComponent("BookCatalog", 10);
        }
    }
}
