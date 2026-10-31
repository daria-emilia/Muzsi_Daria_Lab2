using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.ViewModels;
using Muzsi_Daria_Lab2.Data;
using Muzsi_Daria_Lab2.Models;


namespace Muzsi_Daria_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Muzsi_Daria_Lab2Context _context;

        public IndexModel(Muzsi_Daria_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        public BookData BookD { get; set; } = new BookData();
        public int BookID { get; set; }
        public int CategoryID { get; set; }
        public string TitleSort { get; set; } = "";
        public string AuthorSort { get; set; } = "";
        public string CurrentFilter { get; set; } = "";

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            BookD = new BookData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString;

            // Folosim o interogare sigură care nu dă erori de ThenInclude
            var booksList = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .AsNoTracking()
                .ToListAsync();

            var booksIQ = booksList.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                booksIQ = booksIQ.Where(s => s.Title.Contains(searchString) ||
                                          (s.Author != null && s.Author.FirstName != null && s.Author.FirstName.Contains(searchString)) ||
                                          (s.Author != null && s.Author.LastName != null && s.Author.LastName.Contains(searchString)));
            }

            if (id != null)
            {
                BookID = id.Value;
                var book = booksIQ.Where(i => i.ID == id.Value).SingleOrDefault();
                if (book != null && book.BookCategories != null)
                {
                    BookD.Categories = book.BookCategories.Select(s => s.Category).ToList();
                }
            }

            switch (sortOrder)
            {
                case "title_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Title).AsQueryable();
                    break;
                case "author_desc":
                    booksIQ = booksIQ.OrderByDescending(s => s.Author != null ? s.Author.LastName : "").AsQueryable();
                    break;
                case "author":
                    booksIQ = booksIQ.OrderBy(s => s.Author != null ? s.Author.LastName : "").AsQueryable();
                    break;
                default:
                    booksIQ = booksIQ.OrderBy(s => s.Title).AsQueryable();
                    break;
            }

            BookD.Books = booksIQ.ToList();
        }
    }
}