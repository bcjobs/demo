using BookStore;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.MVC6.Components
{
    public class BookCatalogViewComponent : ViewComponent
    {
        public BookCatalogViewComponent(IBookCatalog catalog)
        {
            Catalog = catalog;
        }

        IBookCatalog Catalog { get; }

        public IViewComponentResult Invoke(int max)
        {
            var books = Catalog.Read(new BookQuery
            {
                PageSize = max
            });

            return View(books);
        }
    }
}
