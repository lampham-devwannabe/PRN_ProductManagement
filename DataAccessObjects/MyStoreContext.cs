using BusinessObjects;
using Microsoft.EntityFrameworkCore;

public class MyStoreContext : DbContext
{
    public DbSet<AccountMember> AccountMembers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
