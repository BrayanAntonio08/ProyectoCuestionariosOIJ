using Cuestionarios.Domain;
using CuestionariosOIJ.ReglasNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuestionariosOIJ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpcionRespuestaController : ControllerBase
    {
        [HttpPost("InsertarOpcionRespuesta/{preguntaId}")]
        public async Task<ActionResult<OpcionRespuesta>> InsertarOpcionRespuesta(int preguntaId, [FromBody] OpcionRespuesta opcion)
        {
            
            OpcionRespuestaRN opcionRespuestaRN = new OpcionRespuestaRN();
            int nuevoId = opcionRespuestaRN.InsertarOpcionRespuesta(preguntaId, opcion);
            opcion.Id = nuevoId;
            return Ok(opcion);
        }

        [HttpGet ("/api/[controller]/{preguntaId}")]
        public async Task<ActionResult<List<OpcionRespuesta>>> ListarOpcionRespuesta(int preguntaId){
            OpcionRespuestaRN opcionRespuestaRN = new OpcionRespuestaRN();
            return Ok(opcionRespuestaRN.ListarOpcionesRespuesta(preguntaId));
        }

        [HttpGet("/api/[controller]/{opcionId}")]
        public async Task<ActionResult<OpcionRespuesta>> ObtenerOpcionRespuesta(int opcionId)
        {
            OpcionRespuestaRN opcionRespuestaRN = new OpcionRespuestaRN();
            return Ok(opcionRespuestaRN.ObtenerPorID(opcionId));
        }

    }
}
