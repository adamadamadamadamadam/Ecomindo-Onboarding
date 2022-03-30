using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnboardingApp.Model
{
    public class Borrower
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Book { get; set; }
    }
}
