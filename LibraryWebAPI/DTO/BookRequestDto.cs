using System;

namespace LibraryWebAPI.DTO
{
    public class BookRequestDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public string BookName { get; set; }
        public string StudentName { get; set; }
        public bool IsBookReturned { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }

}
