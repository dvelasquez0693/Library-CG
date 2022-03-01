using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using DataAccess;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public LibraryContext Context { get; }

        public StudentController(LibraryContext _context)
        {
            Context = _context;
        }
        // GET: api/<StudentsController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return Context.Student.ToList();
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {            
            var student = Context.Student.SingleOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (student == null) return NotFound();
            if (ModelState.IsValid)
            {
                Context.Add(student);
                Context.SaveChanges();
            }
            return Ok(student);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            if (id != student.Id) return NotFound();
            if (ModelState.IsValid)
            {
                Context.Update(student);
                Context.SaveChanges();             
            }
            return Ok(student);          

        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = Context.Student.SingleOrDefault(s => s.Id == id);
            if (student == null) return NotFound();

            Context.Remove(student);
            Context.SaveChanges();

            return Ok();
        }
    }
}
