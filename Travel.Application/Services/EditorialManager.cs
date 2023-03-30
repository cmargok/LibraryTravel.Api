using Travel.Application.Dtos.Editoriales;
using Travel.Application.Infra_Contracts;
using Travel.Application.Services.Contracts;
using Travel.Domain.Models;

namespace Travel.Application.Services
{
    public class EditorialManager : IEditorialManager
    {
        private readonly IEditorialRepository _editorialRepository;
        public EditorialManager(IEditorialRepository editorialRepository)
        {
            _editorialRepository = editorialRepository;
        }


        public async Task<bool> AddEditorial(EditorialBasicDto editorial, CancellationToken cancellationToken)
        {
            if (editorial == null) throw new ArgumentNullException();

            var editorialEntity = new Editorial()
            {
                Nombre = editorial.Nombre,
                Sede = editorial.Sede,
            };

            return await _editorialRepository.AddAsync(editorialEntity, cancellationToken);           

        }

        public async Task<List<EditorialDto>> GetEditoriales(CancellationToken cancellationToken)
        {

            var editoriales = await _editorialRepository.GetAllAsync(cancellationToken);

            if (editoriales == null || editoriales.Count == 0)
            {
                return new List<EditorialDto>();
            }

            return editoriales.Select(e => new EditorialDto
            {
                Nombre = e.Nombre,
                Sede = e.Sede,
                EditorialId = e.EditorialId,
            }).ToList();  

        }
    }
}
