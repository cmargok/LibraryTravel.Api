using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Application.Dtos.Editoriales;
using Travel.Application.Services.Contracts;
using Travel.Domain.Enums;
using Travel.Domain.Tools;

namespace Travel.Api.Controllers
{
    /// <summary>
    /// Editoriales
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EditorialesController : ControllerBase
    {
        private readonly IEditorialManager _editorialManager;

       
        public EditorialesController(IEditorialManager editorialManager)
        {
            _editorialManager = editorialManager;
        }

        /// <summary>
        /// Metodo para obtener todas las editoriales
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<List<EditorialDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<EditorialDto>>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _editorialManager.GetEditoriales(cancellationToken);

            if (result.Count > 0)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, result.Count));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, result.Count));

        }



        /// <summary>
        /// Metodo para añadir una nueva editorial a la libreria
        /// </summary>
        /// <param name="editorialBasic"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(EditorialBasicDto editorialBasic, CancellationToken cancellationToken)
        {
            var result = await _editorialManager.AddEditorial(editorialBasic, cancellationToken);

            if (result)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, 1));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, 0));

        }



    }
}
