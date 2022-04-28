using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace urlshorter
{
    public partial class UrlshorterContext : DbContext
    {
        public UrlshorterContext()
        {
        }

        public UrlshorterContext(DbContextOptions<UrlshorterContext> options) : base(options)
        {
        }

        public DbSet<UrlEntity> Urls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=urlshorter.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlEntity>(entity =>
            {
                entity.ToTable("URL");

                entity.HasIndex(e => e.Hash, "IX_URL_Hash")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "IX_URL_Id")
                    .IsUnique();

                entity.Property(e => e.Url).HasColumnName("URL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
