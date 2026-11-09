using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.Data;

namespace Muzsi_Daria_Lab2.Pages.BorrowingsPages;

public class DetailsModel : PageModel
{
    private readonly Muzsi_Daria_Lab2Context _context;
    public DetailsModel(Muzsi_Daria_Lab2Context context)
    {
        _context = context;
    }

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
}
