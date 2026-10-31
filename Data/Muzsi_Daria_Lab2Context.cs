using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Models;

namespace Muzsi_Daria_Lab2.Data
{
    public class Muzsi_Daria_Lab2Context : DbContext
    {
        public Muzsi_Daria_Lab2Context(DbContextOptions<Muzsi_Daria_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;
        public DbSet<Author> Author { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<BookCategory> BookCategory { get; set; } = default!;
    }
}