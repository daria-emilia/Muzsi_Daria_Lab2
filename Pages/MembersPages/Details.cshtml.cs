using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.Data;

namespace Muzsi_Daria_Lab2.Pages.MembersPages;

public class DetailsModel : PageModel
{
    private readonly Muzsi_Daria_Lab2Context _context;
    public DetailsModel(Muzsi_Daria_Lab2Context context)
    {
        _context = context;
    }

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
}
