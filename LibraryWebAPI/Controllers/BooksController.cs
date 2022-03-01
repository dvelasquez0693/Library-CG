using DataAccess;
using Entity;
using LibraryWebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext libraryContext;

        public BooksController(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }
        // GET: api/<BooksController>
        [HttpGet]
        public IEnumerable Get()
        {
            var result = libraryContext.Book.Select(b => new
            {
                b.Id,
                b.Name,
                b.Publisher,
                b.Area,
                AuthorId = b.BookAuthors.Select(ba => ba.AuthorId).FirstOrDefault(),
                AuthorName = b.BookAuthors.Select(ba => ba.Author.Name).FirstOrDefault(),

            }).ToList();

            return result;
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = libraryContext.Book.SingleOrDefault(s => s.Id == id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        public IActionResult Post([FromBody] BookDto bookDto)
        {
            var book = new Book { Name = bookDto.Name, Publisher = bookDto.Description };
            var bookAuthor = new BookAuthor { AuthorId = bookDto.AuthorId, Book = book };

            libraryContext.BookAuthor.Add(bookAuthor);
            libraryContext.SaveChanges();
            return Ok();

        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            if (id != book.Id) return NotFound();
            if (ModelState.IsValid)
            {
                libraryContext.Update(book);
                libraryContext.SaveChanges();
            }
            return Ok(book);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
