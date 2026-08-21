using LibraryManagmentSystem.Data;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LibraryManagmentSystem.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext context;

        public AuthorService(AppDbContext context)
        {
            this.context = context;
        }

        public Author AddAuthor(Author author)
        {
            context.Authors.Add(author);    
            context.SaveChanges();
            return author;
        }

        public Author? DeleteAuthor(int id)
        {
            var author = context.Authors.Find(id);
            if(author == null)
            {
                return null;
            }
            context.Authors.Remove(author);
            context.SaveChanges();
            return author;
        }

        public Author GetAuthorById(int id)
        {
            return context.Authors.Find(id);
        }

        public List<Author> GetAuthors()
        {
            return context.Authors.ToList();
        }

        public Author UpdateAuthor(int id,Author author)
        {
            var existingAuthor=context.Authors.Find(id);
            if(existingAuthor == null)
            {
                return null;
            }
            existingAuthor.Books = author.Books;
            context.SaveChanges();
            return existingAuthor;
        }
    }
    }
