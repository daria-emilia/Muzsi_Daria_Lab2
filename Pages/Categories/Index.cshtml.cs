using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Data;
using Muzsi_Daria_Lab2.Models;

namespace Muzsi_Daria_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Muzsi_Daria_Lab2Context _context;

        public IndexModel(Muzsi_Daria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;
        public int CategoryID { get; set; }
        public int BookID { get; set; }

        [BindProperty]
        public Category NewCategory { get; set; } = default!;

        public async Task OnGetAsync(int? id, int? bookID)
        {
            Category = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                var selectedCategory = Category.Where(c => c.ID == id.Value).Single();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Category = await _context.Category
                    .Include(c => c.BookCategories)
                        .ThenInclude(bc => bc.Book)
                    .ToListAsync();
                return Page();
            }

            _context.Category.Add(NewCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}