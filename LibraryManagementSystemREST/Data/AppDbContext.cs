using LibraryManagmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagmentSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors => Set<Author>();

        public DbSet<Book> Books => Set<Book>();

        public DbSet<Member> Members => Set<Member>();

        public DbSet<Transiction> Transictions => Set<Transiction>();

        public DbSet<User> User => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transiction>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            //book -> transiction
            modelBuilder.Entity<Transiction>().HasOne(tc => tc.Book).WithMany(c => c.Transictions).HasForeignKey(cp => cp.BookId).OnDelete(DeleteBehavior.Cascade);

            //member -> transiction
            modelBuilder.Entity<Transiction>().HasOne(cp => cp.Member).WithMany(c => c.Transictions).HasForeignKey(cp => cp.MemberId).OnDelete(DeleteBehavior.Cascade);

            //email must be unique
            modelBuilder.Entity<Member>().HasIndex(c => c.Email).IsUnique();

            //auth
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "admin", Password = "1234" },
                new User { Id = 2, UserName = "member", Password = "5678" }
            );
        }



                
        }
    }

