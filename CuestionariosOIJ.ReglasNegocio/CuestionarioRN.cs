using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.ReglasNegocio
{
    public class CuestionarioRN
    {
        private readonly CuestionarioData _data;

        public CuestionarioRN()
        {
            _data = new CuestionarioData(new CuestionariosContext());
        }

        public static string GenerarCodigo()
        {
            // Definir un conjunto de caracteres alfanuméricos
            char[] caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

            // Generar un string de 20 caracteres aleatorios
            string codigo = "";
            for (int i = 0; i < 20; i++)
            {
                codigo += caracteres[RandomNumberGenerator.GetInt32(caracteres.Length)];
            }

            return codigo;
        }
        public void InsertarCuestionario(Cuestionario cuestionario)
        {
            CuestionarioEF nuevoItem = new CuestionarioEF()
            {
                Codigo = GenerarCodigo(),
                Nombre = cuestionario.Descripcion,
                Descripcion = cuestionario.Descripcion,
                Activo = cuestionario.Activo,
                FechaCreacion = DateTime.Now,
                FechaVencimiento = cuestionario.Vencimiento,
            };

            _data.Insertar(nuevoItem);
        }

        public void ActualizarCuestionario(Cuestionario cuestionario)
        {
            CuestionarioEF nuevoItem = new CuestionarioEF()
            {
                Id = cuestionario.Id,
                Codigo = cuestionario.Codigo,
                Nombre = cuestionario.Nombre,
                Descripcion = cuestionario.Descripcion,
                Activo = cuestionario.Activo,
                FechaCreacion = DateTime.Now,
                FechaVencimiento=cuestionario.Vencimiento,
            };

            _data.Actualizar(nuevoItem);
        }

        public void EliminarCuestionario(Cuestionario cuestionario)
        {
            CuestionarioEF itemBorrado = new CuestionarioEF()
            {
                Id = cuestionario.Id
            };

            _data.Eliminar(itemBorrado);
        }

        public List<Cuestionario> ListarCuestionario()
        {
            List<Cuestionario> resultado = new List<Cuestionario>();
            List<CuestionarioEF> itemsGuardados = _data.Listar();
            foreach (var cuestionario in itemsGuardados)
            {
                resultado.Add(
                    new Cuestionario()
                    {
                        Id = cuestionario.Id,
                        Codigo = cuestionario.Nombre,
                        Nombre = cuestionario.Nombre,
                        Descripcion = cuestionario.Descripcion,
                        Activo = cuestionario.Activo,
                        FechaCreacion = DateTime.Now,
                        Vencimiento = cuestionario.FechaVencimiento,
                    }
                    );
            }

            return resultado;
        }

        public Cuestionario ObtenerPorID(int id)
        {
            CuestionarioEF result = _data.ObtenerPorID(id);

            if (result != null)
            {
                Cuestionario nuevoItem = new Cuestionario()
                {
                    Id = result.Id,
                    Codigo = result.Codigo,
                    Nombre = result.Nombre,
                    Descripcion = result.Descripcion,
                    Activo = result.Activo,
                    FechaCreacion = DateTime.Now,
                    Vencimiento = result.FechaVencimiento,
                };
                return nuevoItem;
            }

            return null;
        }
    }
}
