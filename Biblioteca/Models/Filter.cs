using System;

namespace Library.Models
{
    public class Filter : BookRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
