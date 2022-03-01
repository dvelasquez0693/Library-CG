using System.Collections.Generic;

namespace Entity
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
