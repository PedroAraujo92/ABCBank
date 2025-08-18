using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AccountHolder> AccountHolders => Set<AccountHolder>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

}
