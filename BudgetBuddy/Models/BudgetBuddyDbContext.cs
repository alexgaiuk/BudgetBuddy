using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Models
{
    public class BudgetBuddyDbContext : DbContext
    {
        // Create table property for the expense model (will create tables for Id, Value, Description).
        public DbSet<Expense> Expenses { get; set; }

        public BudgetBuddyDbContext(DbContextOptions<BudgetBuddyDbContext> options)
            : base(options)
        {
            
        }
    }
}
