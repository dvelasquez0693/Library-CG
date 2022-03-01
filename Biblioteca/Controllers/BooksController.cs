using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        // GET: BooksController
        public async Task<IActionResult> Index()
        {

            List<Books> books = new List<Books>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/books"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        books = JsonConvert.DeserializeObject<List<Books>>(apiResponse);
                        return View(books);
                    }

                }
            }
            return NotFound();
        }

        // GET: BooksController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: BooksController/Create
        public async Task<IActionResult> Create()
        {
            var authors = new List<Author>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/authors"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        authors = JsonConvert.DeserializeObject<List<Author>>(apiResponse);
                        ViewBag.Authors = authors?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
                    }

                }
            }
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Books books)
        {
            try
            {
                var jsonBooks = JsonConvert.SerializeObject(books);
                var content = new StringContent(jsonBooks, Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync("http://localhost:5001/api/books",
                        content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }

                    }
                }
                return View(books);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: BooksController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Books books = new Books();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/books/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    books = JsonConvert.DeserializeObject<Books>(apiResponse);
                }
            }
            if (books == null) return NotFound();
            return View(books);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Books book)
        {
            try
            {
                var jsonBook = JsonConvert.SerializeObject(book);
                var content = new StringContent(jsonBook, Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsync("http://localhost:5001/api/books/" + id, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
                return View(book);


            }
            catch
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Books books = new Books();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5001/api/books/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        books = JsonConvert.DeserializeObject<Books>(apiResponse);
                    }


                }
            }
            if (books == null) return NotFound();
            return View(books);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Books books)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync("http://localhost:5001/api/books/" + id))
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
    }
}
