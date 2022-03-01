namespace Library.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Area { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

    }
}
