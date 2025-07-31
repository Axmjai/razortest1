using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Models;

public partial class ItemClient
{
    [Key]
    public int ItemId { get; set; }

    public int ClientId { get; set; }

    public string Remark { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("ItemClients")]
    public virtual Client Client { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("ItemClient")]
    public virtual Item Item { get; set; }
}
