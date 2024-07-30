using System.ComponentModel.DataAnnotations;

namespace Chinese_Sale.Models.DTOs
{
    public class DonorDTO
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        [StringLength(maximumLength: 10)]
        public string? Phone { get; set; }
        [StringLength(30)]
        public string? Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
