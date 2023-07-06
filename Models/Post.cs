using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ThinhBlogAPI.Models;

[Table("Post")]
public partial class Post
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(200)]
    public string? Title { get; set; }

    [StringLength(500)]
    public string? Body { get; set; }

    public int? Author { get; set; }

    [ForeignKey("Author")]
    [InverseProperty("Posts")]
    public virtual Author? AuthorNavigation { get; set; }
}
