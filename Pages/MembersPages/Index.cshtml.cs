using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.Data;

namespace Muzsi_Daria_Lab2.Pages.MembersPages;

public class IndexModel : PageModel
{
    private readonly Muzsi_Daria_Lab2Context _context;

    public IndexModel(Muzsi_Daria_Lab2Context context)
    {
        _context = context;
    }

    public IList<Members> Members { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Members = await _context.Members.ToListAsync();
    }
}
