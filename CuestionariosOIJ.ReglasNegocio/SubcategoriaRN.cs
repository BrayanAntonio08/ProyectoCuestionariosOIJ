using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.AccesoDatos.Models;

namespace CuestionariosOIJ.ReglasNegocio
{
    public class SubcategoriaRN
    {
        private readonly SubcategoriaData _data;


        public SubcategoriaRN()
        {
            _data = new SubcategoriaData(new CuestionariosContext());
        }

        public Subcategoria InsertarSubcategoria(Subcategoria subcategoria)
        {
            SubcategoriaEF nuevoItem = new SubcategoriaEF()
            {
                Nombre = subcategoria.Nombre,
                CategoriaId = subcategoria.Categoria.Id
            };

            subcategoria.Id = _data.InsertarSubcategoria(nuevoItem);
            return subcategoria;
        }

        public void ActualizarSubcategoria(Subcategoria subcategoria)
        {
            SubcategoriaEF nuevoItem = new SubcategoriaEF()
            {
                Id = subcategoria.Id,
                Nombre = subcategoria.Nombre,
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
                        Categoria = categoriaRN.ObtenerPorID(item.CategoriaId)
                    }
                    );
            }

            return resultado;
        }

        public Subcategoria? ObtenerPorID(int? id)
        {
            if (id == null)
                return null;

            SubcategoriaEF item = _data.ObtenerSubcategoriaPorID(id);
            return new Subcategoria()
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Categoria = new CategoriaRN().ObtenerPorID(item.CategoriaId)
                };
        }
    }
}
