using System.ComponentModel.DataAnnotations;

namespace Travel.Application.Dtos.Editoriales
{
    public class EditorialBasicDto
    {
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(45)]
        public string Sede { get; set; } = string.Empty;

    }



}
