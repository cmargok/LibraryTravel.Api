using System.ComponentModel.DataAnnotations;

namespace Travel.Application.Dtos.Editoriales
{
    public class EditorialBasicDto
    {
        [Required]
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(45)]
        public string Sede { get; set; } = string.Empty;

    }



}
