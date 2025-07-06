using Microsoft.EntityFrameworkCore;
using MyEconomy.Models;

namespace MyEconomy.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BudgetEntry> BudgetEntries { get; set; }
    }
}