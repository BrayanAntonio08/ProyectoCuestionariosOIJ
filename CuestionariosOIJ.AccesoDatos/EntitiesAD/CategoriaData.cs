using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class CategoriaData
    {
        private readonly CuestionariosContext _db;

        public CategoriaData(CuestionariosContext context)
        {
            _db = context;
        }

        public List<CategoriaEF> ListarCategorias()
        {
            return _db.Categorias.ToList();
        }

        public CategoriaEF? ObtenerCategoriaPorID(int? id)
        {
            return _db.Categorias.Find(id);
        }

        public CategoriaEF ObtenerCategoriaPorNombre(string nombre)
        {
            return _db.Categorias.Where( x => x.Nombre == nombre).FirstOrDefault();
        }

        public int InsertarCategoria(CategoriaEF categoria)
        {
            _db.Categorias.Add(categoria);
            _db.SaveChanges();
            return categoria.Id;
        }

        public void ActualizarCategoria(CategoriaEF categoria)
        {
            _db.Categorias.Update(categoria);
            _db.SaveChanges();
        }

        public void EliminarCategoria(CategoriaEF categoria)
        {
            _db.Categorias.Remove(categoria);
            _db.SaveChanges();
        }
    }
}
