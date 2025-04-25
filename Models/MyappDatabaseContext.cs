using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Models;

public partial class MyappDatabaseContext : DbContext
{
    public MyappDatabaseContext()
    {
    }

    public MyappDatabaseContext(DbContextOptions<MyappDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemClient> ItemClients { get; set; }

    public virtual DbSet<SerialNumber> SerialNumbers { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-MF38127;Initial Catalog=myapp_database;Integrated Security=True;Pooling=False;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Items).HasConstraintName("FK_Items_Categories");

            entity.HasOne(d => d.Client).WithMany(p => p.Items).HasConstraintName("FK_Items_Clients");
        });

        modelBuilder.Entity<ItemClient>(entity =>
        {
            entity.Property(e => e.ItemId).ValueGeneratedNever();

            entity.HasOne(d => d.Client).WithMany(p => p.ItemClients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clients_ItemClients__Id_ClientId");

            entity.HasOne(d => d.Item).WithOne(p => p.ItemClient).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SerialNumber>(entity =>
        {
            entity.HasOne(d => d.Item).WithMany(p => p.SerialNumbers).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
