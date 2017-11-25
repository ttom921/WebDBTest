using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Model.Models
{
    public partial class BloggingContext : DbContext
    {
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Post> Post { get; set; }

        public BloggingContext  (DbContextOptions<BloggingContext> options)
            :base(options)
        {

        }
        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-NITSS8T;Initial Catalog=Blogging;User ID=sa;Password=12345678;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // return;
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.BlogId);
                entity.Property(e => e.BlogId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BlogId).HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);
            });
        }
    }
}
