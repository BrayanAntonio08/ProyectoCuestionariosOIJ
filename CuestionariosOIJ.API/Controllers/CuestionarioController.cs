using Cuestionarios.Domain;
using CuestionariosOIJ.ReglasNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuestionariosOIJ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuestionarioController : ControllerBase
    {

        [HttpPost(Name = "InsertarCuestionario")]
        public async Task<ActionResult> InsertarCuestionario([FromBody] Cuestionario cuestionario)
        {
            // Validar los datos recibidos
            if (cuestionario == null || cuestionario.Nombre == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la categoria
            CuestionarioRN business = new CuestionarioRN();
            business.InsertarCuestionario(cuestionario);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok(cuestionario));
        }

        [HttpGet(Name = "ListarCuestionarios")]
        public async Task<ActionResult<List<Cuestionario>>> ListarCuestionarios()
        {
            CuestionarioRN business = new CuestionarioRN();
            List<Cuestionario> cuestionarios = business.ListarCuestionario();

            return await Task.FromResult(Ok(cuestionarios));
        }

        [HttpPut(Name = "ActualizarCuestionario")]
        public async Task<ActionResult<Cuestionario>> ActualizarCuestionario([FromBody] Cuestionario cuestionario)
        {
            // Validar los datos recibidos
            if (cuestionario == null || cuestionario.Nombre == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la categoria
            CuestionarioRN business = new CuestionarioRN();
            business.ActualizarCuestionario(cuestionario);

            return await Task.FromResult(Ok(cuestionario));
        }

        [HttpDelete(Name = "EliminarCuestionario")]
        public async Task<ActionResult> EliminarCuestionario([FromBody] Cuestionario cuestionario)
        {
            CuestionarioRN business = new CuestionarioRN();
            business.EliminarCuestionario(cuestionario);

            return await Task.FromResult(Ok()); ;
        }
    }
}
