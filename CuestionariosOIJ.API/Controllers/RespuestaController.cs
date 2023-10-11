using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
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
        public async Task<ActionResult> InsertarRespuesta([FromBody] Respuesta respuesta)
        {
            // Validar los datos recibidos
            if (respuesta == null || respuesta.TipoRespuesta == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la respuesta
            RespuestaRN business = new RespuestaRN();
            business.InsertarRespuesta(respuesta);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok(respuesta));
        }

        [HttpGet ("ListarRespuestasTotales")]
        public async Task<ActionResult<List<Respuesta>>> ListarRespuestasTotales([FromBody] Cuestionario cuestionario)
        {
            RespuestaRN business = new RespuestaRN();
            List<Respuesta> respuestas = business.ListarRespuestasTotales(new CuestionarioData(new CuestionariosContext()).ObtenerPorID(cuestionario.Id));

            return await Task.FromResult(Ok(respuestas));
        }


        [HttpGet ("ListarRespuestasPorPregunta")]
        public async Task<ActionResult<List<Respuesta>>> ListarRespuestasPorPregunta([FromBody] Pregunta pregunta)
        {
            RespuestaRN business = new RespuestaRN();
            List<Respuesta> respuestas = business.ListarRespuestasPorPregunta(new PreguntaData(new CuestionariosContext()).ObtenerPreguntaPorID(pregunta.Id));

            return await Task.FromResult(Ok(respuestas));
        }

        [HttpGet ("ListarRespuestasTotalesPorPregunta")]
        public async Task<ActionResult<List<Respuesta>>> ListarRespuestasTotalesPorPregunta([FromBody] Pregunta pregunta)
        {
            RespuestaRN business = new RespuestaRN();
            List<Respuesta> respuestas = business.ListarRespuestasTotalesPorPregunta(new PreguntaData(new CuestionariosContext()).ObtenerPreguntaPorID(pregunta.Id));

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

            // Guardar la respuesta
            RespuestaRN business = new RespuestaRN();
            business.BorrarRespuestasCuestionario(cuestionario);

            return await Task.FromResult(Ok()); ;
        }

    }
}
