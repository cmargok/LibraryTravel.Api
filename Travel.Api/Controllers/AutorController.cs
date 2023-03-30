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
