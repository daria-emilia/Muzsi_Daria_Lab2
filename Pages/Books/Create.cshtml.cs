using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Muzsi_Daria_Lab2.Models;
using Muzsi_Daria_Lab2.Data;

namespace Muzsi_Daria_Lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Muzsi_Daria_Lab2Context _context;

        public CreateModel(Muzsi_Daria_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var book = new Book();
            book.BookCategories = new List<BookCategory>();

            // Populăm lista goală pentru categorii la creare
            PopulateAssignedCategoryData(_context, book);

            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newBook = new Book();

            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var category in selectedCategories)
                {
                    var categoryToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(category)
                    };
                    newBook.BookCategories.Add(categoryToAdd);
                }
            }

            if (await TryUpdateModelAsync<Book>(
                newBook,
                "Book",
                i => i.Title, i => i.AuthorID, i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                _context.Book.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, newBook);
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            return Page();
        }
    }
}