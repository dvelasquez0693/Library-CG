using System.Collections.Generic;

namespace Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Publisher { get; set; }

        public string Area { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookRequest> BookRequests { get; set; }
    }
}
