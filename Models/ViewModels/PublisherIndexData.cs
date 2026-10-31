using System.Collections.Generic;
using Muzsi_Daria_Lab2.Models;

namespace Muzsi_Daria_Lab2.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher>? Publishers { get; set; }
        public IEnumerable<Book>? Books { get; set; }
    }
}