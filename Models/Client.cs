using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Models;

public partial class Client
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<ItemClient> ItemClients { get; set; } = new List<ItemClient>();

    [InverseProperty("Client")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
