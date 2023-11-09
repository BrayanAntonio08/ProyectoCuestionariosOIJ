using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.Models;
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

        public int InsertarSubcategoria(SubcategoriaEF subcategoria)
        {
            _db.Subcategorias.Add(subcategoria);
            _db.SaveChanges();
            return subcategoria.Id;
        }

        public void ActualizarSubcategoria(SubcategoriaEF subcategoria)
        {
            _db.Subcategorias.Update(subcategoria);
            _db.SaveChanges ();
        }

        public void EliminarSubcategoria(SubcategoriaEF subcategoria)
        {
            _db.Subcategorias.Remove(subcategoria);
            _db.SaveChanges ();
        }

        public List<SubcategoriaEF> ListarSubcategorias()
        {
            return _db.Subcategorias.ToList();
        }

        public SubcategoriaEF? ObtenerSubcategoriaPorID(int? id)
        {
            return _db.Subcategorias.Find(id);
        }

        public SubcategoriaEF ObtenerSubcategoriaPorNombre(string nombre)
        {
            return _db.Subcategorias.Where(x => x.Nombre == nombre).FirstOrDefault();
        }

        public List<SubcategoriaEF> ListarSubcategoriasDeCategoria(CategoriaEF categoria)
        {
            return _db.Subcategorias.Where(sub => sub.CategoriaId == categoria.Id).ToList();
        }

    }
}
