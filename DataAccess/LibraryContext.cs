using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class LibraryContext : DbContext
    {

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.AuthorId, ba.BookId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<BookRequest>()
                .HasKey(br => new { br.BookId, br.StudentId });
            modelBuilder.Entity<BookRequest>()
                .HasOne(br => br.Book)
                .WithMany(br => br.BookRequests).HasForeignKey(br => br.BookId);
            modelBuilder.Entity<BookRequest>()
                .HasOne(br => br.Student)
                .WithMany(br => br.BookRequests).HasForeignKey(br => br.StudentId);


            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    Name = "Stephen King",
                    Nationality = "USA"
                },
                new Author
                {
                    Id = 2,
                    Name = "JK Rowling",
                    Nationality = "FRANCIA"
                },
                new Author
                {
                    Id = 3,
                    Name = "JR Tolkien",
                    Nationality = "USA"
                },
                new Author
                {
                    Id = 4,
                    Name = "Ruben Dario",
                    Nationality = "NICARAGUA"
                },
                new Author
                {
                    Id = 5,
                    Name = "Emilio Santamaria",
                    Nationality = "ARGENTINA"
                },
                new Author
                {
                    Id = 6,
                    Name = "Benedetti, Mario",
                    Nationality = "FRANCIA"
                },
                new Author
                {
                    Id = 7,
                    Name = "David Vitter",
                    Nationality = "COSTA RICA"
                },
                new Author
                {
                    Id = 8,
                    Name = "Julian Templeman",
                    Nationality = "HONDURAS"
                }

            );
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<Entity.BookRequest> BookRequest { get; set; }
    }
}

