using System.ComponentModel.DataAnnotations;

namespace sale_server.Models
{
    public enum TypeOfDonation
    {
        Present, Money
    }
    public class Donor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public TypeOfDonation TypeOfDonation { get; set; }
    }
}
