using System.Collections.Generic;
using Muzsi_Daria_Lab2.Models;

namespace Muzsi_Daria_Lab2.ViewModels
{
    public class BookData
    {
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}