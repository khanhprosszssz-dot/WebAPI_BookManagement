using WebAPI_BookManagement.Data;
using WebAPI_BookManagement.Models.Domain;
using WebAPI_BookManagement.Models.DTO;

namespace WebAPI_BookManagement.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


//get all TG
        public List<AuthorDTO> GellAllAuthors()
        {
            // Get Data From Database - Domain Model
            var allAuthorsDomain =
                _dbContext.Authors.ToList();

            // Map domain models to DTOs
            var allAuthorDTO =
                new List<AuthorDTO>();

            foreach (var authorDomain in allAuthorsDomain)
            {
                allAuthorDTO.Add(
                    new AuthorDTO()
                    {
                        Id = authorDomain.Id,
                        FullName = authorDomain.FullName
                    });
            }

            // Return DTOs
            return allAuthorDTO;
        }


//get by id
        public AuthorNoIdDTO GetAuthorById(int id)
        {
            // Get Author Domain Model from DB
            var authorWithIdDomain =
                _dbContext.Authors
                    .FirstOrDefault(x => x.Id == id);

            if (authorWithIdDomain == null)
            {
                return null;
            }

            // Map Domain Model to DTO
            var authorNoIdDTO =
                new AuthorNoIdDTO
                {
                    FullName =
                        authorWithIdDomain.FullName
                };

            return authorNoIdDTO;
        }


//add author
        public AddAuthorRequestDTO AddAuthor(
            AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorDomainModel =
                new Author
                {
                    FullName =
                        addAuthorRequestDTO.FullName
                };

            // Use Domain Model to create Author
            _dbContext.Authors.Add(
                authorDomainModel);

            _dbContext.SaveChanges();

            return addAuthorRequestDTO;
        }


//update author
        public AuthorNoIdDTO UpdateAuthorById(
            int id,
            AuthorNoIdDTO authorNoIdDTO)
        {
            var authorDomain =
                _dbContext.Authors
                    .FirstOrDefault(n => n.Id == id);

            if (authorDomain != null)
            {
                authorDomain.FullName =
                    authorNoIdDTO.FullName;

                _dbContext.SaveChanges();
            }

            return authorNoIdDTO;
        }


//delete author
        public Author? DeleteAuthorById(int id)
        {
            var authorDomain =
                _dbContext.Authors
                    .FirstOrDefault(n => n.Id == id);

            if (authorDomain != null)
            {
                _dbContext.Authors.Remove(
                    authorDomain);

                _dbContext.SaveChanges();
            }

            return null;
        }
//baitap phan 4-5
        public List<BookWithAuthorAndPublisherDTO>
    GetBooksByAuthorId(int id)
        {
            var books = _dbContext.Books
                .Where(book =>
                    book.Book_Authors
                        .Any(ba => ba.AuthorId == id))
                .Select(book =>
                    new BookWithAuthorAndPublisherDTO()
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        IsRead = book.IsRead,
                        DateRead = book.DateRead,
                        Rate = book.Rate,
                        Genre = book.Genre,
                        CoverUrl = book.CoverUrl,
                        DateAdded = book.DateAdded,

                        PublisherName = book.Publisher.Name,

                        AuthorNames = book.Book_Authors
                            .Select(ba => ba.Author.FullName)
                            .ToList()
                    })
                .ToList();

            return books;
        }
    }
}