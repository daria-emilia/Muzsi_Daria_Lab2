using System.ComponentModel.DataAnnotations;

namespace Muzsi_Daria_Lab2.Models
{
    public class Borrowings
    {
        public int ID { get; set; }
        public int? MembersID { get; set; }
        public Members? Members { get; set; }
        public int? BookID { get; set; }
        public Book? Book { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
    }
}
