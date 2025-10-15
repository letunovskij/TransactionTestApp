using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TransactionTestApp.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TransactionTestApp.Data;

public class TransactionDbContext
    : DbContext
{
    public DbSet<TransactionModel> Transactions { get; set; }

    public TransactionDbContext()
    {

    }

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TransactionModel>(entity =>
        {
        });
    }
}