using Microsoft.EntityFrameworkCore;

namespace Muzsi_Daria_Lab2.Data
{
    public class Muzsi_Daria_Lab2Context : DbContext
    {
        public Muzsi_Daria_Lab2Context(DbContextOptions<Muzsi_Daria_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Muzsi_Daria_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Muzsi_Daria_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Muzsi_Daria_Lab2.Models.Author> Author { get; set; } = default!;
    }
}