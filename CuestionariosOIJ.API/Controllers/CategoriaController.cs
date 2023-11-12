using Cuestionarios.Domain;
using CuestionariosOIJ.ReglasNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuestionariosOIJ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        
       
        [HttpPost(Name = "InsertarCategoria")]
        public async Task <ActionResult> InsertarCategoria([FromBody] Categoria categoria)
        {
            // Validar los datos recibidos
            if (categoria == null || categoria.Nombre == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la categoria
            CategoriaRN business = new CategoriaRN();
            Categoria result = business.InsertarCategoria(categoria);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok(result));
        }

        [HttpGet(Name = "ListarCategorias")]
        public async Task<ActionResult<List<Categoria>>> ListarCategorias()
        {
            CategoriaRN business = new CategoriaRN();
            List<Categoria> categorias = business.ListarCategorias();

            return await Task.FromResult(Ok(categorias));
        }

        //[HttpGet(Name = "ListarCategoriasID")]
        //[Route("api/CategoriaID")]
        //public async Task<ActionResult<List<Categoria>>> ObtenerCategoriaPorID(int id)
        //{
        //    CategoriaRN business = new CategoriaRN();
        //    List<Categoria> categoriasid = business.ListarCategorias();

        //    return await Task.FromResult(Ok(categoriasid));
        //}

        [HttpPut(Name = "ActualizarCategoria")]
        public async Task<ActionResult<Categoria>> ActualizarCategoria([FromBody] Categoria categoria)
        {
            // Validar los datos recibidos
            if (categoria == null || categoria.Nombre == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la categoria
            CategoriaRN business = new CategoriaRN();
            business.ActualizarCategoria(categoria);

            return await Task.FromResult(Ok(categoria));
        }

        [HttpDelete(Name = "EliminarCategoria")]
        public async Task<ActionResult> EliminarCategoria([FromBody] Categoria categoria)
        {
            CategoriaRN business = new CategoriaRN();
            business.EliminarCategoria(categoria);

            return await Task.FromResult(Ok(categoria)); ;
        }
    }
}
