using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
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
            return _db.Categoria.ToList();
        }

        public CategoriaEF ObtenerCategoriaPorID(int id)
        {
            return _db.Categoria.Find(id);
        }

        public CategoriaEF ObtenerCategoriaPorNombre(string nombre)
        {
            return _db.Categoria.Where( x => x.Nombre == nombre).First();
        }

        public void InsertarCategoria(CategoriaEF categoria)
        {
            _db.Categoria.Add(categoria);
            _db.SaveChanges();
        }

        public void ActualizarCategoria(CategoriaEF categoria)
        {
            _db.Categoria.Update(categoria);
            _db.SaveChanges();
        }

        public void EliminarCategoria(CategoriaEF categoria)
        {
            _db.Categoria.Remove(categoria);
            _db.SaveChanges();
        }
    }
}
