namespace Chinese_Sale.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public int Sum { get; set; }
        public bool IsDraft { get; set; } = true;
        //public ICollection<PresentsOrder>? PresentsOrder { get; set; }
    }
}
