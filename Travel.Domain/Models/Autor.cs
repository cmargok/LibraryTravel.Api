using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel.Domain.Models
{
    [Table("Autor")]
    public class Autor
    {
        [Key]        
        [MaxLength(10)]
        public int AutorId { get; set; }

        [Column("nombre")]
        [StringLength(45)]
        public string Nombre { get; set; } = null!;

        [Column("apellidos")]
        [StringLength(45)]
        public string Apellido { get; set; } = null!;



        public List<AutoresHasLibro> AutorLibros { get; set; }
    }
}
