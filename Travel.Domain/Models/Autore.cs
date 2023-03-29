using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel.Domain.Models
{
    public partial class Autore
    {
        [Key]
        [MaxLength(10)]
        public int Id { get; set; }
        [Column("nombre")]
        [StringLength(45)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        [Column("apellido")]
        [StringLength(45)]
        [Unicode(false)]
        public string Apellido { get; set; } = null!;
    }
}
