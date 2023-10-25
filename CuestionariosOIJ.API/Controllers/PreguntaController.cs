using Cuestionarios.Domain;
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

        [HttpPut("{cuestionarioId}")]
        public async Task<ActionResult<Pregunta>> ActualizarPregunta(int cuestionarioId, [FromBody] Pregunta pregunta)
        {
            // Validar los datos recibidos
            if (pregunta == null || pregunta.ContenidoPregunta == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la pregunta
            PreguntaRN business = new PreguntaRN();
            business.ActualizarPregunta(cuestionarioId, pregunta);

            return await Task.FromResult(Ok(pregunta));
        }

        [HttpPut("ordenar/{cuestionarioId}")]
        public async Task<ActionResult<Pregunta>> ActualizarOrdenPreguntas(int cuestionarioId, [FromBody] List<Pregunta> preguntas)
        {
            // Guardar la pregunta
            PreguntaRN business = new PreguntaRN();
            foreach (var pregunta in preguntas)
                business.ActualizarPregunta(cuestionarioId, pregunta);

            return await Task.FromResult(Ok());
        }

        [HttpGet("ListarPreguntas")]
        public async Task<ActionResult<List<Pregunta>>> ListarPreguntas(int cuestionarioID)
        {
            PreguntaRN business = new PreguntaRN();
            List<Pregunta> preguntas = business.ListarPreguntas(cuestionarioID);

            return await Task.FromResult(Ok(preguntas));
        }

        [HttpGet("ListarPreguntasSeleccion/{cuestionarioID}")]
        public async Task<ActionResult<List<Pregunta>>> ListarPreguntasSeleccion(int cuestionarioID)
        {
            PreguntaRN business = new PreguntaRN();
            List<Pregunta> preguntas = business.ListarPreguntasSeleccion(cuestionarioID);

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
        public async Task<ActionResult> EliminarPregunta(int pregunta)
        {
            PreguntaRN business = new PreguntaRN();
            business.EliminarPregunta(pregunta);

            return await Task.FromResult(Ok()); ;
        }


    }
}
