using Cuestionarios.Domain;
using CuestionariosOIJ.ReglasNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuestionariosOIJ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaController : ControllerBase
    {
        [HttpPost(Name = "InsertarRespuesta")]
        public async Task<ActionResult> InsertarRespuesta([FromBody] List<Respuesta> respuestas)
        {
            // Validar los datos recibidos
            if (respuestas == null || respuestas.Count() == 0)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la categoria
            RespuestaRN business = new RespuestaRN();
            foreach (var respuesta in respuestas)
                business.InsertarRespuesta(respuesta);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok());
        }

        [HttpGet ("ListarTotal/{cuestionario}")]
        public async Task<ActionResult<List<Respuesta>>> ListarRespuestasTotales(int cuestionario)
        {
            RespuestaRN business = new RespuestaRN();
            List<Respuesta> respuestas = business.ListarRespuestasTotales(cuestionario);

            return await Task.FromResult(Ok(respuestas));
        }


        [HttpGet ("ListarRespuestasPorPregunta")]
        public async Task<ActionResult<List<Respuesta>>> ListarRespuestasPorPregunta([FromBody] Pregunta pregunta)
        {
            RespuestaRN business = new RespuestaRN();
            List<Respuesta> respuestas = business.ListarRespuestasPorPregunta(pregunta);

            return await Task.FromResult(Ok(respuestas));
        }

        [HttpGet ("ListarRespuestasTotalesPorPregunta")]
        public async Task<ActionResult<List<Respuesta>>> ListarRespuestasTotalesPorPregunta([FromBody] Pregunta pregunta)
        {
            RespuestaRN business = new RespuestaRN();
            
            List<Respuesta> respuestas = business.ListarRespuestasTotalesPorPregunta(pregunta);

            return await Task.FromResult(Ok(respuestas));
        }


        [HttpPut(Name = "BorrarRespuestasCuestionario")]
        public async Task<ActionResult> BorrarRespuestasCuestionario([FromBody] Cuestionario cuestionario)
        {
            // Validar los datos recibidos
            if (cuestionario == null || cuestionario.Id == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la categoria
            RespuestaRN business = new RespuestaRN();
            business.BorrarRespuestasCuestionario(cuestionario);

            return await Task.FromResult(Ok()); ;
        }
        [HttpDelete(Name = "EliminarRespuesta")]
        public async Task<ActionResult> EliminarRespuesta([FromBody] Respuesta respuesta)
        {
            BorradoRespuestaRN business = new BorradoRespuestaRN();
            business.EliminarRespuesta(respuesta);

            return await Task.FromResult(Ok()); ;
        }
    }
}
