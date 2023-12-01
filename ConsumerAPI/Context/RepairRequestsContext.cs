using Microsoft.EntityFrameworkCore;

namespace ConsumerAPI.Context
{
    public class RepairRequestsContext : DbContext
    {
        public DbSet<SharedLibrary.Models.RepairRequest> RepairRequests { get; set; }

        public RepairRequestsContext(DbContextOptions<RepairRequestsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
