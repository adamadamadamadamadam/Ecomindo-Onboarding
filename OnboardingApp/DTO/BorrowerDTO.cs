using System.Collections.Generic;

namespace OnboardingApp.DTO
{
    public class BorrowerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookDTO> Book { get; set; }
    }
}
