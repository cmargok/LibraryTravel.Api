using System.ComponentModel.DataAnnotations;

namespace Travel.Application.Dtos.Autores
{
    public class AutorDto : AutorBasicDto
    {

        [MaxLength(10)]
        public int AutorId { get; set; }
    }
}
