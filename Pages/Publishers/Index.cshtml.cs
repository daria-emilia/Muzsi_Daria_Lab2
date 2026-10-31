using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Data;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.ViewModels; // Aici am adaugat ViewModel-ul tau

namespace Muzsi_Daria_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Muzsi_Daria_Lab2Context _context;

        public IndexModel(Muzsi_Daria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get; set; } = default!;

        // Proprietatile noi cerute de laborator
        public PublisherIndexData PublisherData { get; set; } = default!;
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();

            // Incarcam editurile impreuna cu cartile si autorii lor
            PublisherData.Publishers = await _context.Publisher
                .Include(i => i.Books)
                    .ThenInclude(c => c.Author)
                .OrderBy(i => i.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                PublisherID = id.Value;
                Publisher publisher = PublisherData.Publishers
                    .Where(i => i.ID == id.Value).Single();
                PublisherData.Books = publisher.Books;
            }
        }
    }
}