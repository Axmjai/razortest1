using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Controllers;

public class MyAppContext : DbContext
{
    public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<SerialNumber> SerialNumbers { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ItemClient> ItemClients { get; set; }

}

