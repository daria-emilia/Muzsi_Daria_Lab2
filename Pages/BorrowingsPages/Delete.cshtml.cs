using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.Data;

namespace Muzsi_Daria_Lab2.Pages.BorrowingsPages;

public class DeleteModel : PageModel
{
    private readonly Muzsi_Daria_Lab2Context _context;

    public DeleteModel(Muzsi_Daria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowings Borrowings { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null || _context.Borrowings == null)
        {
            return NotFound();
        }

        var borrowings = await _context.Borrowings
            .Include(b => b.Book)
            .ThenInclude(b => b.Author)
            .Include(b => b.Members)
            .FirstOrDefaultAsync(m => m.ID == id);

        if (borrowings == null)
        {
            return NotFound();
        }
        else
        {
            Borrowings = borrowings;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowings = await _context.Borrowings.FindAsync(id);
        if (borrowings != null)
        {
            Borrowings = borrowings;
            _context.Borrowings.Remove(Borrowings);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
