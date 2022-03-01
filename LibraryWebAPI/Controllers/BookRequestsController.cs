using DataAccess;
using Entity;
using LibraryWebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRequestsController : ControllerBase
    {
        private readonly LibraryContext context;

        public BookRequestsController(LibraryContext _context)
        {
            context = _context;
        }
        // GET: api/<BookRequestsController>
        [HttpGet]
        public IEnumerable Get()
        {
            // return context.BookRequest.ToList();

            var result = context.BookRequest.Include(bo => bo.Book).Include(s => s.Student).Select(b => new
            {
                b.BookId,
                BookName = b.Book.Name,
                b.StudentId,
                StudentName = b.Student.Name,
                b.RequestDate,
                b.ReturnDate,
                b.IsBookReturned
            })
                .ToList();

            return result;
        }


        [HttpGet("{studentId}/{bookId}")]
        public IActionResult Get(int studentId, int bookId)
        {
            var bookRequest = context.BookRequest
                .Include(b => b.Book)
                .Include(s => s.Student)
                .SingleOrDefault(s => s.StudentId == studentId && s.BookId == bookId);

            if (bookRequest == null) return NotFound();

            return Ok(bookRequest);
        }

        // GET api/<BookRequestsController>/5
        [HttpGet("search")]
        public IActionResult Search(int? studentId, int? authorId, int? bookId, bool? returned, DateTime? from, DateTime? to, DateTime? returnDate)
        {

            to = to?.Date.AddDays(1);

            var bookRequest = context.BookRequest
                .Include(b => b.Book)
                .Include(s => s.Student)
                .Where(br => studentId == null || br.StudentId == studentId)
                .Where(br => authorId == null || br.Book.BookAuthors.Any(ba => ba.AuthorId == authorId))
                .Where(br => bookId == null || br.BookId == bookId)
                .Where(br => returned == null || br.IsBookReturned == returned)
                .Where(br => from == null || br.ReturnDate >= from)
                .Where(br => to == null || br.ReturnDate <= to)
                .Where(br => returnDate == null || br.ReturnDate == returnDate)
                .Select(b => new
                {
                    b.BookId,
                    BookName = b.Book.Name,
                    b.StudentId,
                    StudentName = b.Student.Name,
                    b.RequestDate,
                    b.ReturnDate,
                    b.IsBookReturned
                }).ToList();

            if (!bookRequest.Any()) return NotFound();

            return Ok(bookRequest);
        }

        // POST api/<BookRequestsController>
        [HttpPost]
        public IActionResult Post([FromBody] BookRequestDto bookRequestDto)
        {
            var bookRequest = new BookRequest()
            {
                StudentId = bookRequestDto.StudentId,
                BookId = bookRequestDto.BookId,
                IsBookReturned = bookRequestDto.IsBookReturned,
                RequestDate = bookRequestDto.RequestDate.Date,
                ReturnDate = bookRequestDto.ReturnDate.Date
            };

            context.BookRequest.Add(bookRequest);
            context.SaveChanges();
            return Ok();
        }

        // PUT api/<BookRequestsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BookRequestDto bookRequestDto)
        {
            if (id != bookRequestDto.Id) return NotFound();
            var model = new BookRequest
            {
                BookId = bookRequestDto.BookId,
                IsBookReturned = bookRequestDto.IsBookReturned,
                StudentId = bookRequestDto.StudentId,
                ReturnDate = bookRequestDto.ReturnDate,
                RequestDate = bookRequestDto.RequestDate
            };

            if (ModelState.IsValid)
            {
                context.Update(model);
                context.SaveChanges();
            }
            return Ok(bookRequestDto);
        }

        // DELETE api/<BookRequestsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromBody] BookRequestDto bookRequestDto)
        {
            if (id != bookRequestDto.Id) return NotFound();
            var model = new BookRequest
            {
                BookId = bookRequestDto.BookId,
                IsBookReturned = bookRequestDto.IsBookReturned,
                StudentId = bookRequestDto.StudentId,
                ReturnDate = bookRequestDto.ReturnDate,
                RequestDate = bookRequestDto.RequestDate
            };

            context.Remove(model);
            context.SaveChanges();

            return Ok();
        }
    }
}
