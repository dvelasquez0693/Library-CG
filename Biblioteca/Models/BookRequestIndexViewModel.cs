using System.Collections.Generic;

namespace Library.Models
{
    public class BookRequestIndexViewModel
    {

        public IEnumerable<Library.Models.BookRequest> BookRequests { get; set; }

        public Filter Filter { get; set; }
    }
}
