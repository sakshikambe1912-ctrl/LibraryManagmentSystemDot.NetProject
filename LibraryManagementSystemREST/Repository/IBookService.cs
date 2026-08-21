using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.Repository
{
    public interface IBookService
    {
        List<Book> GetBooks();

        Book GetBookById(int id);

        Book AddBook(Book book);

        Book UpdateBook(int id, int availableCopies);

        Book? DeleteBook(int id);
    }
}
