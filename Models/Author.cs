using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ThinhBlogAPI.Models;

[Table("Author")]
public partial class Author
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [InverseProperty("AuthorNavigation")]
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
