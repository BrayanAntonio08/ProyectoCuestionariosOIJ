using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.ReglasNegocio
{
    public class CategoriaRN
    {
        private readonly CategoriaData _data;

        public CategoriaRN() {
            _data = new CategoriaData(new CuestionariosContext());
        }

        public void InsertarCategoria(Categoria categoria)
        {
            CategoriaEF nuevoItem = new CategoriaEF()
            {
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
            };

            _data.InsertarCategoria(nuevoItem);
        }

        public List<Categoria> ListarCategorias()
        {
            List<Categoria> resultado = new List<Categoria>();
            List<CategoriaEF> itemsGuardados = _data.ListarCategorias();
            foreach (var item in itemsGuardados)
            {
                resultado.Add(
                    new Categoria()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion
                    }
                    );
            }

            return resultado;
        }
    }
}
