using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.Application.Dtos.Autores;
using Travel.Application.Dtos.Editoriales;
using Travel.Application.Services.Contracts;
using Travel.Domain.Enums;
using Travel.Domain.Tools;

namespace Travel.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorManager _autorManager;
        public AutorController(IAutorManager autorManager)
        {
            _autorManager = autorManager;
        }

        /// <summary>
        /// Metodo para obtener todos los autores que tienen o han tenido libros en la libreria
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<List<AutorDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<List<AutorDto>>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _autorManager.GetAutores(cancellationToken);

            if (result.Count > 0)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, result.Count));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, result.Count));

        }
        /// <summary>
        /// Metodo para obtener informacion sobre un autor
        /// </summary>
        /// <param name="AutorId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<AutorDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<AutorDto>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [Authorize]
        [HttpGet("{AutorId}")]
        public async Task<IActionResult> Get(int AutorId,CancellationToken cancellationToken)
        {
            var result = await _autorManager.GetAuthorInfoById(AutorId, cancellationToken);

            if (result != null)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, 1));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, 0));

        }


        /// <summary>
        /// Metodo para añadir un nuevo autor a la libreria
        /// </summary>
        /// <param name="autorBasic"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(Problem), 500)]
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AutorBasicDto autorBasic, CancellationToken cancellationToken)
        {
            var result = await _autorManager.AddAutor(autorBasic, cancellationToken);

            if (result)
            {
                return Ok(Tools.CreateResponse(result, Result.Success, 1));
            }

            return NotFound(Tools.CreateResponse(result, Result.NotFound, 0));
        }



    }
}
