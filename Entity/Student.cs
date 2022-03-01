using System.Collections.Generic;

namespace Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Career { get; set; }
        public int Age { get; set; }
        public ICollection<BookRequest> BookRequests { get; set; }

    }
}
