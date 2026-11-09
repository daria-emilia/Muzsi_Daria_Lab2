using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.Data;

namespace Muzsi_Daria_Lab2.Pages.MembersPages;

public class DeleteModel : PageModel
{
    private readonly Muzsi_Daria_Lab2Context _context;

    public DeleteModel(Muzsi_Daria_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Members Members { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var members = await _context.Members.FirstOrDefaultAsync(m => m.ID == id);
        if (members is null)
        {
            return NotFound();
        }
        else
        {
            Members = members;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var members = await _context.Members.FindAsync(id);
        if (members != null)
        {
            Members = members;
            _context.Members.Remove(Members);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
