using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BookStore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.MVC6.Controllers.Api
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        public BooksController(IBookCatalog catalog)
        {
            Catalog = catalog;
        }

        IBookCatalog Catalog { get; }

        [HttpGet]
        public IEnumerable<Book> Get(BookQuery query)
        {
            query = query ?? new BookQuery();
            return Catalog.Read(query);
        }

        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return Catalog.Read(id);
        }

        [HttpPost]
        public Book Post([FromBody] Book book)
        {
            var id = Catalog.Write(book);
            return Catalog.Read(id);
        }

        [HttpPut("{id}")]
        public Book Put(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                throw new ArgumentException();

            Catalog.Write(book);
            return book;
        }

    }
}
