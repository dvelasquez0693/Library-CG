using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookRequest = Library.Models.BookRequest;

namespace Library.Controllers
{
    public class BookRequestsController : Controller
    {
        // GET: BookRequests
        public async Task<IActionResult> Index()
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/bookrequests"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var bookRequests = JsonConvert.DeserializeObject<List<BookRequest>>(apiResponse);


                        await LoadBooks();
                        await LoadStudents();
                        await LoadAuthors();


                        return View(new BookRequestIndexViewModel { BookRequests = bookRequests });
                    }

                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Filter(IFormCollection formFields)
        {

            var bookRequests = new List<BookRequest>();
            using (var httpClient = new HttpClient())
            {

                var queryString = GetQueryStringParamsForSearch(formFields);

                using (var response = await httpClient.GetAsync("http://localhost:5001/api/bookrequests/search?" + queryString))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        bookRequests = JsonConvert.DeserializeObject<List<BookRequest>>(apiResponse);
                    }

                }
            }


            await LoadBooks();
            await LoadStudents();
            await LoadAuthors();


            return View("Index", new BookRequestIndexViewModel { BookRequests = bookRequests });
        }


        private string GetQueryStringParamsForSearch(IFormCollection formFields)
        {
            string studentId = formFields["StudentId"];
            string authorId = formFields["AuthorId"];
            string bookId = formFields["BookId"];
            string returned = formFields["IsBookReturned"];
            string from = formFields["From"];
            string to = formFields["To"];
            string returnDate = formFields["ReturnDate"];
            var queryString = "";

            if (!string.IsNullOrEmpty(studentId))
                queryString += $"studentId={studentId}";

            if (!string.IsNullOrEmpty(authorId))
                queryString += $"&authorId={authorId}";

            if (!string.IsNullOrEmpty(bookId))
                queryString += $"&bookId={bookId}";

            if (!string.IsNullOrEmpty(returned))
                queryString += $"&returned=true";

            if (!string.IsNullOrEmpty(from))
                queryString += $"&from={from}";

            if (!string.IsNullOrEmpty(to))
                queryString += $"&to={to}";

            if (!string.IsNullOrEmpty(returnDate))
                queryString += $"&returnDate={returnDate}";

            return queryString;
        }

        // GET: BookRequests/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    return View();
        //}

        // GET: BookRequests/Create
        public async Task<IActionResult> Create()
        {
            await LoadBooks();
            await LoadStudents();
            return View();
        }

        // POST: BookRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookRequest bookRequest)
        {
            try
            {
                var jsonBooks = JsonConvert.SerializeObject(bookRequest);
                var content = new StringContent(jsonBooks, Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("http://localhost:5001/api/bookrequests",
                        content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }

                    }
                }
                return View(bookRequest);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: BookRequests/Edit/5
        public async Task<IActionResult> Edit(int studentId, int bookId)
        {
            BookRequest bookRequest;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5001/api/bookrequests/{studentId}/{bookId}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    bookRequest = JsonConvert.DeserializeObject<BookRequest>(apiResponse);
                }
            }


            await LoadBooks();
            await LoadStudents();
            await LoadAuthors();

            if (bookRequest == null) return NotFound();
            return View(bookRequest);
        }

        // POST: BookRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookRequest bookRequest)
        {
            try
            {
                var jsonBook = JsonConvert.SerializeObject(bookRequest);
                var content = new StringContent(jsonBook, Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsync($"http://localhost:5001/api/bookrequests/" + id, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(bookRequest);


            }
            catch
            {
                return View();
            }
        }

        // GET: BookRequests/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            BookRequest bookRequest = new BookRequest();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/bookrequests/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        bookRequest = JsonConvert.DeserializeObject<BookRequest>(apiResponse);
                    }


                }
            }
            if (bookRequest == null) return NotFound();
            return View(bookRequest);
        }

        // POST: BookRequests/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, BookRequest bookRequest)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync("http://localhost:5001/api/bookrequests/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                            return RedirectToAction(nameof(Index));
                    }
                }
                return View("Index");

            }
            catch
            {
                return View();
            }
        }


        private async Task LoadStudents()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync("http://localhost:5001/api/student");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                ViewBag.Students = students?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            }
        }


        private async Task LoadBooks()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync("http://localhost:5001/api/books");

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var books = JsonConvert.DeserializeObject<List<Books>>(apiResponse);
                ViewBag.Books = books?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            }
        }

        private async Task LoadAuthors()
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync("http://localhost:5001/api/authors");

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                var authors = JsonConvert.DeserializeObject<List<Author>>(apiResponse);
                ViewBag.Authors = authors?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            }
        }


    }
}
