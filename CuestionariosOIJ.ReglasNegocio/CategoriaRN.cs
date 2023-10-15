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

        public void ActualizarCategoria(Categoria categoria)
        {
            CategoriaEF nuevoItem = new CategoriaEF()
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
            };

            _data.ActualizarCategoria(nuevoItem);
        }

        public void EliminarCategoria(Categoria categoria)
        {
            CategoriaEF itemBorrado = new CategoriaEF()
            {
                Id = categoria.Id
            };

            _data.EliminarCategoria(itemBorrado);
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

        public Categoria ObtenerPorID(int id)
        {
            CategoriaEF result = _data.ObtenerCategoriaPorID(id);

            if(result != null)
            {
                Categoria respuesta = new Categoria()
                {
                    Id = result.Id,
                    Nombre = result.Nombre,
                    Descripcion = result.Descripcion == null? "":result.Descripcion.ToString()
                };

                return respuesta;
            }

            return null;
        }

        public Categoria ObtenerPorNombre(string nombre)
        {
            CategoriaEF result = _data.ObtenerCategoriaPorNombre(nombre);

            if (result != null)
            {
                Categoria respuesta = new Categoria()
                {
                    Id = result.Id,
                    Nombre = result.Nombre,
                    Descripcion = result.Descripcion == null ? "" : result.Descripcion.ToString()
                };

                return respuesta;
            }

            return null;
        }
    }
}
