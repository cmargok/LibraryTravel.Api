using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel.Domain.Models
{
    [Table("Libro")]
    public class Libro
    {
        [Key]
        [Column("Isbn")]
        [MaxLength(13)]
        public string Isbn { get; set; }


        [Column("Titulo")]
        [StringLength(45)]
        public string Titulo { get; set; } = null!;

        [Column("Sinopsis", TypeName = "text")]
        public string Sinopsis { get; set; } = null!;

        [Column("NPaginas")]
        [StringLength(45)]
        public string NPaginas { get; set; } = null!;


        [ForeignKey("Editoriales")]
        public int EditorialId { get; set; }
        public Editorial Editoriales { get; set; } 

        public List<AutoresHasLibro> AutorLibro { get; set; }
    }
}
