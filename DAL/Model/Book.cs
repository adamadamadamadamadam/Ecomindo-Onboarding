using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int LamaPinjam { get; set; } 
    }
}
