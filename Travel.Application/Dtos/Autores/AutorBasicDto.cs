using System.ComponentModel.DataAnnotations;

namespace Travel.Application.Dtos.Autores
{
    public class AutorBasicDto
    {


        [StringLength(45)]
        public string Nombre { get; set; } = null!;


        [StringLength(45)]
        public string Apellido { get; set; } = null!;


    }
}
