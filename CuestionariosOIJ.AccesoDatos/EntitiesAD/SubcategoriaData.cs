using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class SubcategoriaData
    {
        private readonly CuestionariosContext _db;

        public SubcategoriaData(CuestionariosContext context)
        {
            _db = context;
        }

        public void InsertarSubcategoria(SubcategoriaEF subcategoria)
        {
            _db.Subcategoria.Add(subcategoria);
            _db.SaveChanges();
        }

        public void ActualizarSubcategoria(SubcategoriaEF subcategoria)
        {
            _db.Subcategoria.Update(subcategoria);
            _db.SaveChanges ();
        }

        public void EliminarSubcategoria(SubcategoriaEF subcategoria)
        {
            _db.Subcategoria.Remove(subcategoria);
            _db.SaveChanges ();
        }

        public List<SubcategoriaEF> ListarSubcategorias()
        {
            return _db.Subcategoria.ToList();
        }

        public SubcategoriaEF ObtenerSubcategoriaPorID(int id)
        {
            return _db.Subcategoria.Find(id);
        }

        public SubcategoriaEF ObtenerSubcategoriaPorNombre(string nombre)
        {
            return _db.Subcategoria.Where(x => x.Nombre == nombre).First();
        }

        public List<SubcategoriaEF> ListarSubcategoriasDeCategoria(CategoriaEF categoria)
        {
            return _db.Subcategoria.Where(sub => sub.CategoriaId == categoria.Id).ToList();
        }

    }
}
