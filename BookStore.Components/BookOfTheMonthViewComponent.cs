using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class BookOfTheMonthViewComponent : ViewComponent
    {
        public BookOfTheMonthViewComponent(IBookCatalog catalog)
        {
            Catalog = catalog;
        }

        IBookCatalog Catalog { get; }

        public IViewComponentResult Invoke()
        {
            var book = Catalog.Read(new BookQuery()).First();
            return View(book);
        }
    }
}
