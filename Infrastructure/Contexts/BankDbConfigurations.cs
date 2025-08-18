using Common.Enums;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;

namespace Infrastructure.Contexts;

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .ToTable("Accounts", "Banking")
            .HasIndex(a=> a.AccountNumber)
            .IsUnique()
            .HasDatabaseName("IX_Accounts_AccountNumber");

        builder
            .Property(a => a.Type)
            .HasConversion(new EnumToStringConverter<AccountType>());

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();
        builder.Property(a => a.AccountNumber)
            .IsRequired()
            .HasMaxLength(20);
        builder.Property(a => a.Balance)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.HasOne(a => a.AccountHolder)
            .WithMany(ah => ah.Accounts)
            .HasForeignKey(a => a.AccountHolderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
