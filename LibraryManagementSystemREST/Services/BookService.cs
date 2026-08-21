using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext context;

        public BookService(AppDbContext context)
        {
            this.context = context;
        }
        public Book AddBook(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return book;
        }

        public Book?  DeleteBook(int id)
        {
            var book = context.Books.Find(id);
            if (book == null)
            {
                return null;
            }
            context.Books.Remove(book);
            context.SaveChanges();
            return book;
        }

        public Book GetBookById(int id)
        {
            return context.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);
        }

        public List<Book> GetBooks()
        {
            return context.Books
                .Include(b => b.Author)
                .ToList();
        }
        public Book UpdateBook(int id, int availableCopies)
        {
            var existingBook = context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.AvailableCpoies = availableCopies;
            context.SaveChanges();
            return existingBook;
        }






    }
}
