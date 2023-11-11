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
            cuestionario = business.InsertarCuestionario(cuestionario);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok(cuestionario));
        }

        [HttpGet("Oficina/{oficina}")]
        public async Task<ActionResult<List<Cuestionario>>> ListarCuestionarios(string oficina)
        {
            CuestionarioRN business = new CuestionarioRN();
            List<Cuestionario> cuestionarios = business.ListarCuestionario(oficina);

            return await Task.FromResult(Ok(cuestionarios));
        }

        [HttpGet("Revisador/{revisador}")]
        public async Task<ActionResult<List<Cuestionario>>> ListarCuestionariosRevisador(string revisador)
        {
            CuestionarioRN business = new CuestionarioRN();
            List<Cuestionario> cuestionarios = business.ListarCuestionariosRevisador(revisador);

            return await Task.FromResult(Ok(cuestionarios));
        }

        [HttpGet("/api/[controller]/{codigo}")]
        public async Task<ActionResult<Cuestionario>> ObtenerCuestionarioCodigo(string codigo)
        {
            CuestionarioRN business = new CuestionarioRN();
            // Aquí debes buscar el cuestionario con el código proporcionado

            // Supongamos que obtienes el cuestionario
            Cuestionario cuestionario = business.ObtenerCuestionarioPorCodigo(codigo);

            if (cuestionario == null)
            {
                return NotFound(); // Puedes devolver un NotFound si no se encuentra el cuestionario.
            }

            return await Task.FromResult(Ok(cuestionario));
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

        [HttpDelete("/api/[controller]/{cuestionarioId}")]
        public async Task<ActionResult> EliminarCuestionario(int cuestionarioId)
        {
            CuestionarioRN business = new CuestionarioRN();
            business.EliminarCuestionario(cuestionarioId);

            return await Task.FromResult(Ok(cuestionarioId)); ;
        }

        [HttpGet("Reporte/{cuestionarioId}")]
        public async Task<ActionResult<List<object>>> ReporteCuestionario(int cuestionarioId)
        {
            ReporteRN business = new ReporteRN();

            return await Task.FromResult(Ok(business.Reporte(cuestionarioId))); ;

        }
    }
}
