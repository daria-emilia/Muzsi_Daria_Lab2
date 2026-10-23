using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.Data;

namespace Muzsi_Daria_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Muzsi_Daria_Lab2Context _context;

        public IndexModel(Muzsi_Daria_Lab2Context context)
        {
            _context = context;
        }

        // Folosim BookData sau păstrăm lista de cărți cu tot cu BookCategories incluse
        public BookData BookD { get; set; } = default!;
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            BookD = new BookData();

            BookD.Books = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (id != null)
            {
                BookID = id.Value;
            }
        }
    }
}