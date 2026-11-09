using System.ComponentModel.DataAnnotations;

namespace Muzsi_Daria_Lab2.Models
{
    public class Members
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }
        public ICollection<Borrowings>? Borrowingss { get; set; }
    }
}
