using Travel.Application.Dtos.Editoriales;

namespace Travel.Application.Services.Contracts
{
    public interface IEditorialManager
    {
        public Task<List<EditorialDto>> GetEditoriales(CancellationToken cancellationToken);

        public Task<bool> AddEditorial(EditorialBasicDto editorial, CancellationToken cancellationToken);
    }
}
