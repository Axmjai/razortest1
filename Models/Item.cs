using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Models;

public partial class Item
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public int? Pirce { get; set; }

    public int? CategoryId { get; set; }

    public bool? IsDeleted { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Updatedby { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public int? ClientId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Items")]
    public virtual Category Category { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Items")]
    public virtual Client Client { get; set; }

    [InverseProperty("Item")]
    public virtual ItemClient ItemClient { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<SerialNumber> SerialNumbers { get; set; } = new List<SerialNumber>();

}
