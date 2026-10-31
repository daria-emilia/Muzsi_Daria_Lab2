using Microsoft.AspNetCore.Mvc.RazorPages;
using Muzsi_Daria_Lab2.Data;
using Muzsi_Daria_Lab2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Muzsi_Daria_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList = new List<AssignedCategoryData>();

        public void PopulateAssignedCategoryData(Muzsi_Daria_Lab2Context context, Book book)
        {
            var allCategories = context.Category;
            var bookCategories = new HashSet<int>(
                book.BookCategories != null ? book.BookCategories.Select(c => c.CategoryID) : new List<int>());

            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var category in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = category.ID,
                    Name = category.CategoryName,
                    Assigned = bookCategories.Contains(category.ID)
                });
            }
        }

        public void UpdateBookCategories(Muzsi_Daria_Lab2Context context, string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(
                bookToUpdate.BookCategories.Select(c => c.Category.ID));

            foreach (var category in context.Category)
            {
                if (selectedCategoriesHS.Contains(category.ID.ToString()))
                {
                    if (!bookCategories.Contains(category.ID))
                    {
                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = category.ID
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(category.ID))
                    {
                        BookCategory courseToRemove = bookToUpdate.BookCategories.SingleOrDefault(i => i.CategoryID == category.ID)!;
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}