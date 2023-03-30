using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel.Domain.Models
{
    [Table("Editoriales")]
    public  class Editorial
    {
        
        [Key]
        [MaxLength(10)]
        public int EditorialId { get; set; }

        [Column("nombre")]
        [StringLength(45)]
        public string Nombre { get; set; } = null!;

        [Column("sede")]
        [StringLength(45)]
        public string Sede { get; set; } = null!;
       
        public List<Libro> Libros { get; set; }
    }
}
