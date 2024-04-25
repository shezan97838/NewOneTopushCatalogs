using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EbookCatalog.Models
{
    public partial class BookCatalogContext : DbContext
    {
        public BookCatalogContext()
        {
        }

        public BookCatalogContext(DbContextOptions<BookCatalogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookCatalog> BookCatalogs { get; set; } = null!;
        public virtual DbSet<BookPublication> BookPublications { get; set; } = null!;
        public virtual DbSet<BookWriter> BookWriters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4VVE0A2;Database=BookCatalog;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCatalog>(entity =>
            {
                entity.ToTable("BookCatalog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PictureFileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pictureuri)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.BookPublication)
                    .WithMany(p => p.BookCatalogs)
                    .HasForeignKey(d => d.BookPublicationId)
                    .HasConstraintName("FK_BookCatalog_BookPublication");

                entity.HasOne(d => d.BookWriter)
                    .WithMany(p => p.BookCatalogs)
                    .HasForeignKey(d => d.BookWriterId)
                    .HasConstraintName("FK_BookCatalog_BookWriter");
            });

            modelBuilder.Entity<BookPublication>(entity =>
            {
                entity.ToTable("BookPublication");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PublicationName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookWriter>(entity =>
            {
                entity.ToTable("BookWriter");

                entity.Property(e => e.WriterName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
