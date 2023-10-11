using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.API.Models;

namespace CuestionariosOIJ.ReglasNegocio
{
    public class SubcategoriaRN
    {
        private readonly SubcategoriaData _data;


        public SubcategoriaRN()
        {
            _data = new SubcategoriaData(new CuestionariosContext());
        }

        public void InsertarCategoria(Subcategoria subcategoria)
        {
            SubcategoriaEF nuevoItem = new SubcategoriaEF()
            {
                Nombre = subcategoria.Nombre,
                Descripcion = subcategoria.Descripcion,
                CategoriaId = subcategoria.Categoria.Id
            };

            _data.InsertarSubcategoria(nuevoItem);
        }

        public void ActualizarSubcategoria(Subcategoria subcategoria)
        {
            SubcategoriaEF nuevoItem = new SubcategoriaEF()
            {
                Id = subcategoria.Id,
                Nombre = subcategoria.Nombre,
                Descripcion = subcategoria.Descripcion,
                CategoriaId = subcategoria.Categoria.Id,
            };

            _data.ActualizarSubcategoria(nuevoItem);
        }

        public void EliminarSubcategoria(Subcategoria subcategoria)
        {
            SubcategoriaEF itemBorrado = new SubcategoriaEF()
            {
                Id = subcategoria.Id
            };

            _data.EliminarSubcategoria(itemBorrado);
        }

        public List<Subcategoria> ListarSubcategorias()
        {
            List<Subcategoria> resultado = new List<Subcategoria>();
            List<SubcategoriaEF> itemsGuardados = _data.ListarSubcategorias();
            CategoriaRN categoriaRN = new CategoriaRN();
            foreach (var item in itemsGuardados)
            {
                resultado.Add(
                    new Subcategoria()
                    { 
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        Categoria = categoriaRN.ObtenerPorID(item.CategoriaId)
                    }
                    );
            }

            return resultado;
        }

    }
}
