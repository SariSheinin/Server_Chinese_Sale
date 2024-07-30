using Chinese_Sale;
using System.ComponentModel.DataAnnotations;

namespace sale_server.Models
{
   public enum Category
    {
        Furniture, Vacation, Clothing, Events
    }
    public class Present
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public int DonorId { get; set; }
        public int CardPrice { get; set; }
        public Category Category { get; set; }
        public string Image { get; set; }
        public Donor? Donor { get; set; }
    }
}


