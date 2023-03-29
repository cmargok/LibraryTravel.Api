using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel.Domain.Models
{
    public partial class Libro
    {
        [Key]
        [Column("ISBN")]
        [MaxLength(13)]
        public int Isbn { get; set; }
        [Column("editoriales_Id")]
        public int EditorialesId { get; set; }
        [Column("titulo")]
        [StringLength(45)]
        [Unicode(false)]
        public string Titulo { get; set; } = null!;
        [Column("sinopsis", TypeName = "text")]
        public string Sinopsis { get; set; } = null!;
        [Column("n_paginas")]
        [StringLength(45)]
        [Unicode(false)]
        public string NPaginas { get; set; } = null!;

        [ForeignKey("EditorialesId")]
        [InverseProperty("Libros")]
        public virtual Editoriale Editoriales { get; set; } = null!;
    }
}
