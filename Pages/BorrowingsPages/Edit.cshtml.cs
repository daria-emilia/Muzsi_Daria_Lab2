using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Data;
using Muzsi_Daria_Lab2.Models;

namespace Muzsi_Daria_Lab2.Pages.BorrowingsPages;

public class EditModel : PageModel
{
    private readonly Muzsi_Daria_Lab2Context _context;

    public EditModel(Muzsi_Daria_Lab2Context context)
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

        var borrowings = await _context.Borrowings.FirstOrDefaultAsync(m => m.ID == id);
        if (borrowings == null)
        {
            return NotFound();
        }
        Borrowings = borrowings;

        var bookList = _context.Book
            .Include(b => b.Author)
            .Select(x => new
            {
                x.ID,
                BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
            });

        ViewData["BookID"] = new SelectList(bookList, "ID", "BookFullName");
        ViewData["MembersID"] = new SelectList(_context.Members, "ID", "FullName");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Borrowings).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BorrowingsExists(Borrowings.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BorrowingsExists(int id)
    {
        return _context.Borrowings.Any(e => e.ID == id);
    }
}
