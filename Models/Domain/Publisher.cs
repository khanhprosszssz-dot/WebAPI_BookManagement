using System.ComponentModel.DataAnnotations;

namespace WebAPI_BookManagement.Models.Domain
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
            = string.Empty;

        public List<Book> Books { get; set; }
            = new();
    }
}