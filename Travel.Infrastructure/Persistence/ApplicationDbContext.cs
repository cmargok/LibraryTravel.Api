using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Travel.Domain.Models;

namespace Travel.Infrastructure.Persistence
{
    public partial class ApplicationDbContext : DbContext
    {
       /* public ApplicationDbContext()
        {
        }*/

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autore> Autores { get; set; } = null!;
        public virtual DbSet<AutoresHasLibro> AutoresHasLibros { get; set; } = null!;
        public virtual DbSet<Editoriale> Editoriales { get; set; } = null!;
        public virtual DbSet<Libro> Libros { get; set; } = null!;

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = CMARGOK\\SQLEXPRESS; Database=TravelBookBD; PersistSecurityInfo=True; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=true;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autore>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AutoresHasLibro>(entity =>
            {
                entity.Property(e => e.AutoresId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Autores)
                    .WithMany()
                    .HasForeignKey(d => d.AutoresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autores_has_libros_Autores");

                entity.HasOne(d => d.LibrosIsbnNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.LibrosIsbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Autores_has_libros_Libros");
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasOne(d => d.Editoriales)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.EditorialesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Libros_Editoriales");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
//se uso el siguiente comando en la consola nuget para genera los modeleos
//Scaffold-DbContext Microsoft.EntityFrameworkCore.SqlServer
//-Context ApplicationDbContext
//-OutputDir Persistence\Models
//-Connection "Server = CMARGOK\\SQLEXPRESS; Database=TravelBookBD; PersistSecurityInfo=True; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=true;"
//-ContextDir Persistence
//-DataAnnotations