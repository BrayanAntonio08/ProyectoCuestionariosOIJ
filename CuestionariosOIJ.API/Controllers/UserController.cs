using Cuestionarios.Domain;
using CuestionariosOIJ.ReglasNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuestionariosOIJ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost(Name = "IniciarSesión")]
        public async Task<ActionResult> IngresoUsuario([FromBody] Usuario usuario)
        {
            // Validar los datos recibidos
            if (usuario == null || usuario.Nombre == null || usuario.Contrasenna == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // IniciarSesion la categoria
            IngresoUsuario business = new IngresoUsuario();
            business.iniciarSesion(usuario);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok(usuario));
        }
    }
}
