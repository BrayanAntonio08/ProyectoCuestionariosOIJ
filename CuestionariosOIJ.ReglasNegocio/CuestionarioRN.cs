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
            _data = new CuestionarioData();
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
            string codigo = GenerarCodigo();
            while (_data.ExisteCodigo(codigo))
            {
                codigo = GenerarCodigo();
            }
            CuestionarioEF nuevoItem = new CuestionarioEF()
            {
                Codigo = codigo,
                Nombre = cuestionario.Nombre,
                Descripcion = cuestionario.Descripcion,
                Activo = cuestionario.Activo,
                FechaVencimiento = cuestionario.Vencimiento,
                OficinaId = _data.leerOficina(cuestionario.Oficina).Id,
                TipoCuestionarioId = _data.leerTipo(cuestionario.Tipo).Id,  
                Eliminado = false
            };

            _data.InsertarCuestionario(nuevoItem);

            CuestionarioEF resultado = _data.ObtenerPorCodigo(codigo);

            foreach(Usuario revisador in cuestionario.Revisadores)
            {
                _data.InsertarRevisador(resultado, revisador.NombreUsuario);
            }
        }

        public void ActualizarCuestionario(Cuestionario cuestionario)
        {
            CuestionarioEF nuevoItem = new CuestionarioEF()
            {
                Id = cuestionario.Id,
                Nombre = cuestionario.Nombre,
                Descripcion = cuestionario.Descripcion,
                Activo = cuestionario.Activo,
                FechaVencimiento = cuestionario.Vencimiento,
                TipoCuestionarioId = _data.leerTipo(cuestionario.Tipo).Id
            };

            _data.ActualizarCuestionario(nuevoItem);

            // actualizar todas las preguntas (posicion)
            PreguntaRN preguntaRN = new PreguntaRN();
            foreach (var pregunta in cuestionario.Preguntas)
            {
                Pregunta aux = preguntaRN.ObtenerPreguntaPorID(pregunta.Id);
                aux.Posicion = pregunta.Posicion;
                preguntaRN.ActualizarPregunta(aux);
            }
        }

        public void EliminarCuestionario(int cuestionarioId)
        {
            CuestionarioEF itemBorrado = new CuestionarioEF()
            {
                Id = cuestionarioId
            };

            _data.Eliminar(itemBorrado);
        }

        public List<Cuestionario> ListarCuestionario()
        {
            List<Cuestionario> resultado = new List<Cuestionario>();
            List<CuestionarioEF> itemsGuardados = _data.Listar();
            foreach (var cuestionario in itemsGuardados.Where(x => x.Eliminado == false))
            {
                resultado.Add(
                    new Cuestionario()
                    {
                        Id = cuestionario.Id,
                        Codigo = cuestionario.Codigo,
                        Nombre = cuestionario.Nombre,
                        Descripcion = cuestionario.Descripcion,
                        Activo = cuestionario.Activo,
                        FechaCreacion = cuestionario.FechaCreacion,
                        Vencimiento = cuestionario.FechaVencimiento,
                        Oficina = _data.ObtenerOficina(cuestionario.OficinaId),
                        Tipo = _data.ObtenerTipo(cuestionario.TipoCuestionarioId),
                        Revisadores = new UsuarioRN().ListarUsuariosRevisadores(cuestionario.Id)

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

        public Cuestionario ObtenerCuestionarioPorCodigo(string codigo)
        {
            CuestionarioEF result = _data.ObtenerPorCodigo(codigo);

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


                //Se deben cargar todas las preguntas
                PreguntaRN preguntaRN = new PreguntaRN();
                nuevoItem.Preguntas = preguntaRN.ListarPreguntas(nuevoItem.Id);

                return nuevoItem;
            }

            return null;
        }
    }
}
