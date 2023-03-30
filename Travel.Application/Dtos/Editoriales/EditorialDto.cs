using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Travel.Application.Dtos.Editoriales
{
    public class EditorialDto : EditorialBasicDto
    {

        [Key]
        [MaxLength(10)]
        public int EditorialId { get; set; }


    }



}
