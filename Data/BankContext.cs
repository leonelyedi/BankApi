using System;
using System.Collections.Generic;
using BankApi.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Data;

public partial class BankContext : DbContext
{
    public BankContext()
    {
    }

    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Accounttype> Accounttypes { get; set; }

    public virtual DbSet<Administrator> Administrators { get; set; } 

    public virtual DbSet<Banktransaction> Banktransactions { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Transactiontype> Transactiontypes { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Server=localhost;Database=Bank;Integrated Security=true;Port=5432;User Id=postgres;Password=leonel;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accounttype).HasColumnName("accounttype");
            entity.Property(e => e.Balance)
                .HasPrecision(10, 2)
                .HasColumnName("balance");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Regdate)
                .HasDefaultValueSql("('now'::text)::timestamp(6) with time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("regdate");

            entity.HasOne(d => d.AccounttypeNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Accounttype)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_accounttype_fkey");

            entity.HasOne(d => d.Client).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_clientid_fkey");
        });

        modelBuilder.Entity<Accounttype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounttype_pkey");

            entity.ToTable("accounttype", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Regdate)
                .HasDefaultValueSql("('now'::text)::timestamp(6) with time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("regdate");
        });

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("administrator_pkey");

            entity.ToTable("administrator", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Admintype)
                .HasMaxLength(30)
                .HasColumnName("admintype");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(40)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Pwd)
                .HasMaxLength(50)
                .HasColumnName("pwd");
            entity.Property(e => e.Regdate)
                .HasDefaultValueSql("('now'::text)::timestamp(6) with time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("regdate");
        });

        modelBuilder.Entity<Banktransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banktransaction_pkey");

            entity.ToTable("banktransaction", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Externalaccount).HasColumnName("externalaccount");
            entity.Property(e => e.Regdate)
                .HasDefaultValueSql("('now'::text)::timestamp(6) with time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("regdate");
            entity.Property(e => e.Transactiontype).HasColumnName("transactiontype");

            entity.HasOne(d => d.Account).WithMany(p => p.Banktransactions)
                .HasForeignKey(d => d.Accountid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("banktransaction_accountid_fkey");

            entity.HasOne(d => d.TransactiontypeNavigation).WithMany(p => p.Banktransactions)
                .HasForeignKey(d => d.Transactiontype)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("banktransaction_transactiontype_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(40)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Regdate)
                .HasDefaultValueSql("('now'::text)::timestamp(6) with time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("regdate");
        });

        modelBuilder.Entity<Transactiontype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transactiontype_pkey");

            entity.ToTable("transactiontype", "dbo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Regdate)
                .HasDefaultValueSql("('now'::text)::timestamp(6) with time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("regdate");
        });
        modelBuilder.HasSequence("client");
        modelBuilder.HasSequence("client_sequence");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
