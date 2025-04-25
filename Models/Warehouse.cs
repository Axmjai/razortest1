using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Models;

[Table("Warehouse")]
public partial class Warehouse
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ItemId { get; set; }
}
