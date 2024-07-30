using Chinese_Sale.Models;
using Microsoft.EntityFrameworkCore;
using sale_server.Models;

namespace Chinese_Sale.DAL
{
    public class SaleContext:DbContext
    {
        public SaleContext(DbContextOptions<SaleContext> options) : base(options)
        {

        }
        public DbSet<Present> Present { get; set; }
        public DbSet<Donor> Donor { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PresentsOrder> PresentsOrder { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Raffle> Raffle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Present>().Property(p => p.Id).UseIdentityColumn(100, 1);
            base.OnModelCreating(modelBuilder);

        }
    }
}
