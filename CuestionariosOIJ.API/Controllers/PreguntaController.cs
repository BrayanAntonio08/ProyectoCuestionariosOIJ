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
    public class PreguntaController : ControllerBase
    {
        [HttpPost(Name = "InsertarPregunta")]
        public async Task<ActionResult> InsertarPregunta([FromBody] Pregunta pregunta)
        {
            // Validar los datos recibidos
            if (pregunta == null || pregunta.TipoRespuesta == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la pregunta
            PreguntaRN business = new PreguntaRN();
            business.InsertarPregunta(pregunta);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok(pregunta));
        }

        [HttpPut(Name = "ActualizarPregunta")]
        public async Task<ActionResult<Pregunta>> ActualizarPregunta([FromBody] Pregunta pregunta)
        {
            // Validar los datos recibidos
            if (pregunta == null || pregunta.ContenidoPregunta == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la pregunta
            PreguntaRN business = new PreguntaRN();
            business.ActualizarPregunta(pregunta);

            return await Task.FromResult(Ok(pregunta));
        }

        [HttpGet("ListarPreguntas")]
        public async Task<ActionResult<List<Pregunta>>> ListarPreguntas([FromBody] Cuestionario cuestionario)
        {
            PreguntaRN business = new PreguntaRN();
            List<Pregunta> preguntas = business.ListarPreguntas(cuestionario);

            return await Task.FromResult(Ok(preguntas));
        }

        [HttpGet("ObtenerPreguntaPorId")]
        public async Task<ActionResult<Pregunta>> ObtenerPreguntaPorId(int id)
        {
            PreguntaRN business = new PreguntaRN();
            Pregunta pregunta = business.ObtenerPreguntaPorID(id);

            return await Task.FromResult(Ok(pregunta));
        }

        [HttpGet("ObtenerPreguntaEn")]
        public async Task<ActionResult<Pregunta>> ObtenerPreguntaEn(Cuestionario cuestionario, int posicion)
        {
            PreguntaRN business = new PreguntaRN();
            Pregunta pregunta = business.ObtenerPreguntaEn(cuestionario,posicion);

            return await Task.FromResult(Ok(pregunta));
        }

        [HttpDelete(Name = "EliminarPregunta")]
        public async Task<ActionResult> EliminarPregunta([FromBody] Pregunta pregunta)
        {
            PreguntaRN business = new PreguntaRN();
            business.EliminarPregunta(pregunta);

            return await Task.FromResult(Ok()); ;
        }


    }
}
