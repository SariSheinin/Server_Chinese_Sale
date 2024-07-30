using System.ComponentModel.DataAnnotations;

namespace Chinese_Sale.Models.DTOs
{
    public class OrderDTO
    {
 
        [Required]
        public int UserId { get; set; }
        //[StringLength(maximumLength:5)]
        //public User User { get; set; }

        //[MinLength(10)]
        public int? Sum { get; set; }

    }
}
