using WebAPI_BookManagement.Models.Domain;
using WebAPI_BookManagement.Models.DTO;

namespace WebAPI_BookManagement.Repositories
{
    public interface IAuthorRepository
    {
        List<AuthorDTO> GellAllAuthors();

        AuthorNoIdDTO GetAuthorById(int id);

        AddAuthorRequestDTO AddAuthor(
            AddAuthorRequestDTO addAuthorRequestDTO);

        AuthorNoIdDTO UpdateAuthorById(
            int id,
            AuthorNoIdDTO authorNoIdDTO);

        Author? DeleteAuthorById(int id);

        List<BookWithAuthorAndPublisherDTO> GetBooksByAuthorId(int id); //bai tap phan 4-5
    }
}