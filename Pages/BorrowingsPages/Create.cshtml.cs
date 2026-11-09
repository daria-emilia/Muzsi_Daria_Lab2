using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Data;
using Muzsi_Daria_Lab2.Models;

namespace Muzsi_Daria_Lab2.Pages.BorrowingsPages;

public class CreateModel : PageModel
{
    private readonly Muzsi_Daria_Lab2Context _context;

    public CreateModel(Muzsi_Daria_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
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

    [BindProperty]
    public Borrowings Borrowings { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Borrowings.Add(Borrowings);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}