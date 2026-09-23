namespace WebAPI_BookManagement.Models.Domain
{
    public class Book_Author
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; } = null!;

        public int AuthorId { get; set; }

        public Author Author { get; set; } = null!;
    }
}