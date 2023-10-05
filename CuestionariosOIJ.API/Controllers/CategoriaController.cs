﻿using Cuestionarios.Domain;
using CuestionariosOIJ.API.Models;
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
            if (categoria == null || categoria.Nombre == null || categoria.Descripcion == null)
            {
                return await Task.FromResult(BadRequest("Los datos recibidos son inválidos."));
            }

            // Guardar la categoria
            CategoriaRN business = new CategoriaRN();
            business.InsertarCategoria(categoria);

            // Devolver una respuesta exitosa
            return await Task.FromResult(Ok(categoria));
        }

        [HttpGet(Name = "ListarCategorias")]
        public async Task<ActionResult<List<Categoria>>> ListarCategorias()
        {
            CategoriaRN business = new CategoriaRN();
            List<Categoria> categorias = business.ListarCategorias();

            return await Task.FromResult(Ok(categorias));
        }

    }
}