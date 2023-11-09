using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.AccesoDatos.Models;
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

        public Categoria InsertarCategoria(Categoria categoria)
        {
            CategoriaEF nuevoItem = new CategoriaEF()
            {
                Nombre = categoria.Nombre
            };

            categoria.Id = _data.InsertarCategoria(nuevoItem);
            return categoria;
        }

        public void ActualizarCategoria(Categoria categoria)
        {
            CategoriaEF nuevoItem = new CategoriaEF()
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
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
                        Nombre = item.Nombre
                    }
                    );
            }

            return resultado;
        }

        public Categoria? ObtenerPorID(int? id)
        {
            if (id == null)
                return null;

            CategoriaEF result = _data.ObtenerCategoriaPorID(id);

            if(result != null)
            {
                Categoria respuesta = new Categoria()
                {
                    Id = result.Id,
                    Nombre = result.Nombre
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
                    Nombre = result.Nombre
                };

                return respuesta;
            }

            return null;
        }
    }
}
