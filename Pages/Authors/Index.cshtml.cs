using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Data;
using Muzsi_Daria_Lab2.Models;

namespace Muzsi_Daria_Lab2.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly Muzsi_Daria_Lab2Context _context;

        public IndexModel(Muzsi_Daria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Author> Author { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Author != null)
            {
                Author = await _context.Author.ToListAsync();
            }
        }
    }
}