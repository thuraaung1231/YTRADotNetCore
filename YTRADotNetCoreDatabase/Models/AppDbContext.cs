using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace YTRADotNetCoreDatabase.Databases.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Data Source=THURA\\MSSQLSERVER2;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=sasa;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogContent).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}