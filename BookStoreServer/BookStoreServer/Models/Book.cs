using System.ComponentModel.DataAnnotations;

namespace WebApplication15.Models
{
    public class Book
    {
       
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Description { get; set; }


    }
}
