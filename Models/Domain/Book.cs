using System.ComponentModel.DataAnnotations;

namespace WebAPI_BookManagement.Models.Domain
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public int? Rate { get; set; }

        public string? Genre { get; set; }

        public string? CoverUrl { get; set; }

        public DateTime DateAdded { get; set; }
            = DateTime.UtcNow;

        public int PublisherID { get; set; }

        public Publisher Publisher { get; set; } = null!;

        public List<Book_Author> Book_Authors { get; set; }
            = new();
    }
}