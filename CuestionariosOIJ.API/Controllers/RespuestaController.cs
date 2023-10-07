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
        [HttpDelete(Name = "EliminarRespuesta")]
        public async Task<ActionResult> EliminarRespuesta([FromBody] Respuesta respuesta)
        {
            BorradoRespuestaRN business = new BorradoRespuestaRN();
            business.EliminarRespuesta(respuesta);

            return await Task.FromResult(Ok()); ;
        }
    }
}
