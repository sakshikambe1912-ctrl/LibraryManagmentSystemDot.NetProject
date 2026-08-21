using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repository
{
    public interface IAuthorService
    {
        List<Author> GetAuthors();

        Author GetAuthorById(int id);

        Author AddAuthor(Author author);

        Author UpdateAuthor(int id,Author author);

        Author? DeleteAuthor(int id);
    }
}
