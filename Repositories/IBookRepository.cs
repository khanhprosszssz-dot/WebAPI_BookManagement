using WebAPI_BookManagement.Models.Domain;
using WebAPI_BookManagement.Models.DTO;

namespace WebAPI_BookManagement.Repositories
{
    public interface IBookRepository
    {
        List<BookWithAuthorAndPublisherDTO> GetAllBooks(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000);

        BookWithAuthorAndPublisherDTO GetBookById(int id);

        addBookRequestDTO AddBook(
            addBookRequestDTO addBookRequestDTO);

        addBookRequestDTO? UpdateBookById(
            int id,
            addBookRequestDTO bookDTO);

        Book? DeleteBookById(int id);
    }
}