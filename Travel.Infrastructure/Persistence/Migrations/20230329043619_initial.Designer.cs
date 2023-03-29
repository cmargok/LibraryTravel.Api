﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Travel.Infrastructure.Persistence;

#nullable disable

namespace Travel.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230329043619_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Travel.Infrastructure.Persistence.Models.Autore", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("apellido");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("nombre");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("Travel.Infrastructure.Persistence.Models.AutoresHasLibro", b =>
                {
                    b.Property<int>("AutoresId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("autores_Id");

                    b.Property<int>("LibrosIsbn")
                        .HasColumnType("int")
                        .HasColumnName("libros_ISBN");

                    b.HasIndex("AutoresId");

                    b.HasIndex("LibrosIsbn");

                    b.ToTable("Autores_has_libros");
                });

            modelBuilder.Entity("Travel.Infrastructure.Persistence.Models.Editoriale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("nombre");

                    b.Property<string>("Sede")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("sede");

                    b.HasKey("Id");

                    b.ToTable("Editoriales");
                });

            modelBuilder.Entity("Travel.Infrastructure.Persistence.Models.Libro", b =>
                {
                    b.Property<int>("Isbn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ISBN");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Isbn"), 1L, 1);

                    b.Property<int>("EditorialesId")
                        .HasColumnType("int")
                        .HasColumnName("editoriales_Id");

                    b.Property<string>("NPaginas")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("n_paginas");

                    b.Property<string>("Sinopsis")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sinopsis");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("titulo");

                    b.HasKey("Isbn");

                    b.HasIndex("EditorialesId");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("Travel.Infrastructure.Persistence.Models.AutoresHasLibro", b =>
                {
                    b.HasOne("Travel.Infrastructure.Persistence.Models.Autore", "Autores")
                        .WithMany()
                        .HasForeignKey("AutoresId")
                        .IsRequired()
                        .HasConstraintName("FK_Autores_has_libros_Autores");

                    b.HasOne("Travel.Infrastructure.Persistence.Models.Libro", "LibrosIsbnNavigation")
                        .WithMany()
                        .HasForeignKey("LibrosIsbn")
                        .IsRequired()
                        .HasConstraintName("FK_Autores_has_libros_Libros");

                    b.Navigation("Autores");

                    b.Navigation("LibrosIsbnNavigation");
                });

            modelBuilder.Entity("Travel.Infrastructure.Persistence.Models.Libro", b =>
                {
                    b.HasOne("Travel.Infrastructure.Persistence.Models.Editoriale", "Editoriales")
                        .WithMany("Libros")
                        .HasForeignKey("EditorialesId")
                        .IsRequired()
                        .HasConstraintName("FK_Libros_Editoriales");

                    b.Navigation("Editoriales");
                });

            modelBuilder.Entity("Travel.Infrastructure.Persistence.Models.Editoriale", b =>
                {
                    b.Navigation("Libros");
                });
#pragma warning restore 612, 618
        }
    }
}
