using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Travel.Domain.Models;

namespace Travel.Infrastructure.Persistence
{
    public partial class ApplicationDbContext : DbContext
    {
         public ApplicationDbContext()
         {
         }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Editorial> Editoriales { get; set; } = null!;
        public  DbSet<AutoresHasLibro> AutoresHasLibros { get; set; } = null!;
    
        public  DbSet<Libro> Libros { get; set; } = null!;
        public  DbSet<Autor> Autores { get; set; } = null!;

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoresHasLibro>()
                .HasKey(t => new { t.AutorId, t.Isbn });

            modelBuilder.Entity<AutoresHasLibro>()
                .HasOne(t => t.Autor)
                .WithMany(f => f.AutorLibros)
                .HasForeignKey(t => t.AutorId);

            modelBuilder.Entity<AutoresHasLibro>()
                .HasOne(t => t.Libro)
                .WithMany(f => f.AutorLibro)
                .HasForeignKey(t => t.Isbn);
        }


    }
}
//se uso el siguiente comando en la consola nuget para genera los modeleos
//Scaffold-DbContext Microsoft.EntityFrameworkCore.SqlServer
//-Context ApplicationDbContext
//-OutputDir Persistence\Models
//-Connection "Server = CMARGOK\\SQLEXPRESS; Database=TravelBookBD; PersistSecurityInfo=True; Trusted_Connection=True; MultipleActiveResultSets=True; TrustServerCertificate=true;"
//-ContextDir Persistence
//-DataAnnotations