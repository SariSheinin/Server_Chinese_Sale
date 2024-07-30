using sale_server.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Chinese_Sale.Models.DTOs
{
    public class PresentDTO
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int DonorId { get; set; }
        public int CardPrice { get; set; }
        // public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
    }
}
