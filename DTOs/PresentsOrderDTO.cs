using sale_server.Models;

namespace Chinese_Sale.Models.DTOs
{
    public class PresentsOrderDTO
    {
        public int? Id { get; set; }
        public int PresentId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public bool IsDraft { get; set; }
    }
}
