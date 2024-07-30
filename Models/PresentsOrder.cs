using sale_server.Models;

namespace Chinese_Sale.Models
{
    public class PresentsOrder
    {
        public int Id { get; set; }
        public int PresentId { get; set; }
        public int OrderId { get; set; }
        public bool IsDraft { get; set; } = false;
        public int? Quantity { get; set; }
        public Order Order { get; set; }
        public Present Present { get; set; }
    }
}