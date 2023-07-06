using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ThinhBlogAPI.Models;

namespace ThinhBlogAPI.Data;

public partial class DataContext : DbContext
{
	public DataContext()
	{
	}

	public DataContext(DbContextOptions<DataContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Author> Authors { get; set; }

	public virtual DbSet<Post> Posts { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Author>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Author__3214EC277984581E");
		});

		modelBuilder.Entity<Post>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__Post__3214EC279116E0E2");

			entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Posts).HasConstraintName("FK__Post__Author__09A971A2");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
