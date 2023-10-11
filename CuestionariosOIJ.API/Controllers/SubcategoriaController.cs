using Cuestionarios.Domain;
using CuestionariosOIJ.ReglasNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuestionariosOIJ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriaController : ControllerBase
    {
        [HttpPost(Name = "InsertarSubcategoria")]
        public async Task<ActionResult> InsertarSubcategoria([FromBody] Subcategoria subcategoria)
        {
            // Validar los datos recibidos
            if (subcategoria == null || subcategoria.Nombre == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la subcategoria
            SubcategoriaRN business = new SubcategoriaRN();
            business.InsertarCategoria(subcategoria);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok(subcategoria));
        }

        [HttpGet(Name = "ListarSubcategorias")]
        public async Task<ActionResult<List<Subcategoria>>> ListarSubcategorias()
        {
            SubcategoriaRN business = new SubcategoriaRN();
            List<Subcategoria> subcategorias = business.ListarSubcategorias();

            return await Task.FromResult(Ok(subcategorias));
        }

        [HttpPut(Name = "ActualizarSubcategoria")]
        public async Task<ActionResult<Categoria>> ActualizarSubcategoria([FromBody] Subcategoria subcategoria)
        {
            // Validar los datos recibidos
            if (subcategoria == null || subcategoria.Nombre == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la subcategoria
            SubcategoriaRN business = new SubcategoriaRN();
            business.ActualizarSubcategoria(subcategoria);

            return await Task.FromResult(Ok(subcategoria));
        }

        [HttpDelete(Name = "EliminarSubcategoria")]
        public async Task<ActionResult> EliminarSubcategoria([FromBody] Subcategoria subcategoria)
        {
            SubcategoriaRN business = new SubcategoriaRN();
            business.EliminarSubcategoria(subcategoria);

            return await Task.FromResult(Ok()); ;
        }

    }
}
