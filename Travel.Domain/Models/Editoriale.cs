using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel.Domain.Models
{
    public partial class Editoriale
    {
        public Editoriale()
        {
            Libros = new HashSet<Libro>();
        }

        [Key]
        [MaxLength(10)]
        public int Id { get; set; }
        [Column("nombre")]
        [StringLength(45)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        [Column("sede")]
        [StringLength(45)]
        [Unicode(false)]
        public string Sede { get; set; } = null!;

        [InverseProperty("Editoriales")]
        public virtual ICollection<Libro> Libros { get; set; }
    }
}
