using System.ComponentModel.DataAnnotations;

namespace WebAPI_BookManagement.Models.Domain
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
            = string.Empty;

        public List<Book_Author> Book_Authors
        { get; set; } = new();
    }
}