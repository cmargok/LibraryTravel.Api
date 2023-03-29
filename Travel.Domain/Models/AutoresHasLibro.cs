using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Travel.Domain.Models
{
    [Keyless]
    [Table("Autores_has_libros")]
    public partial class AutoresHasLibro
    {
        [Column("autores_Id")]
        [MaxLength(10)]
        public int AutoresId { get; set; }
        [MaxLength(13)]
        [Column("libros_ISBN")]
        public int LibrosIsbn { get; set; }

        [ForeignKey("AutoresId")]
        public virtual Autore Autores { get; set; } = null!;
        [ForeignKey("LibrosIsbn")]
        public virtual Libro LibrosIsbnNavigation { get; set; } = null!;
    }
}
