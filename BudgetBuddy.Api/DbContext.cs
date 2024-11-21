using BudgetBuddy.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public class BudgetBuddyContext : DbContext
{
    public BudgetBuddyContext(DbContextOptions<BudgetBuddyContext> options)
        :base(options)
    {
        
    }
    
    public DbSet<BudgetItem> BudgetItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
}