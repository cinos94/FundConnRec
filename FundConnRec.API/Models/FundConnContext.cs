using FundConnRec.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace FundConnRec.API.Models
{
    public class FundConnContext : DbContext
    {
        public FundConnContext(DbContextOptions<FundConnContext> options) : base(options)
        {

        }
        public FundConnContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder
                   .UseLazyLoadingProxies();
        public virtual DbSet<Portfolio> Portfolios { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<Security> Securities { get; set; }
    }
}
