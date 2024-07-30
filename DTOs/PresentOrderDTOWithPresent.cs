using sale_server.Models;

namespace Chinese_Sale.DTOs
{
    public class PresentOrderDTOWithPresent
    {
        public int? Id { get; set; }
        public Present Present { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public bool IsDraft { get; set; }

    }
}
