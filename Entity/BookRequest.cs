using System;

namespace Entity
{
    public class BookRequest
    {
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public Book Book { get; set; }
        public Student Student { get; set; }
        public bool IsBookReturned { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
