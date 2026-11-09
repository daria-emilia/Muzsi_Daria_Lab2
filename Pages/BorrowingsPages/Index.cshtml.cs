using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.Data;

namespace Muzsi_Daria_Lab2.Pages.BorrowingsPages;

public class IndexModel : PageModel
{
    private readonly Muzsi_Daria_Lab2Context _context;

    public IndexModel(Muzsi_Daria_Lab2Context context)
    {
        _context = context;
    }

    public IList<Borrowings> Borrowings { get; set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Borrowings != null)
        {
            Borrowings = await _context.Borrowings
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.Members).ToListAsync();
        }
    }
}