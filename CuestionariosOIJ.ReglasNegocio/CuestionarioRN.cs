using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.AccesoDatos.Models;
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

        private Cuestionario toCuestionario(CuestionarioEF quest)
        {

            return new Cuestionario()
            {
                Id = quest.Id,
                Nombre = quest.Nombre,
                Descripcion = quest.Descripcion,
                Vencimiento = quest.FechaVencimiento,
                Activo = quest.Activo,
                Codigo = quest.Codigo,
                FechaCreacion = quest.FechaCreacion,
                Oficina = _data.ObtenerOficina(quest.OficinaId),
                Tipo = _data.ObtenerTipo(quest.TipoCuestionarioId),
            };
        }
        private CuestionarioEF toCuestionarioEF(Cuestionario quest)
        {
            OficinaEF oficina = _data.leerOficina(quest.Oficina);
            TipoCuestionarioEF tipo = _data.leerTipo(quest.Tipo);
            return new CuestionarioEF()
            {
                Id=quest.Id,
                Nombre = quest.Nombre,
                Descripcion = quest.Descripcion,
                Activo= quest.Activo,
                Codigo= quest.Codigo,
                Eliminado = false,
                FechaVencimiento = (DateTime)quest.Vencimiento,
                OficinaId = oficina.Id,
                TipoCuestionarioId = tipo.Id
            };
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

        public Cuestionario InsertarCuestionario(Cuestionario cuestionario)
        {
            //se genera el código único
            string codigo = GenerarCodigo();
            while (_data.ExisteCodigo(codigo))
            {
                codigo = GenerarCodigo();
            }
            cuestionario.Codigo = codigo;

            //se carga la oficina asociada, o se crea si no existe
            OficinaEF oficina = _data.leerOficina(cuestionario.Oficina);
            if (oficina == null) {
                _data.NuevaOficina(cuestionario.Oficina);
            }

            CuestionarioEF insertado = toCuestionarioEF(cuestionario);

            _data.InsertarCuestionario(insertado);

            CuestionarioEF resultado = _data.ObtenerPorCodigo(codigo);

            foreach(string revisador in cuestionario.Revisadores)
            {
                _data.InsertarRevisador(resultado, revisador);
            }

            cuestionario.Id = resultado.Id;
            return cuestionario;
        }

        public void ActualizarCuestionario(Cuestionario cuestionario)
        {
            OficinaEF oficina = _data.leerOficina(cuestionario.Oficina); 
            CuestionarioEF actualizado = toCuestionarioEF(cuestionario);

            _data.ActualizarCuestionario(actualizado);

            List<string> revisadoresActuales = _data.listarRevisadores(cuestionario.Id);

            foreach (string revisador in cuestionario.Revisadores)
            {
                //agregar a nuevos
                if(!revisadoresActuales.Contains(revisador))
                    _data.InsertarRevisador(actualizado, revisador);
            }
            foreach(string actual in revisadoresActuales)
            {
                //borrar los que ya no estén
                if (!cuestionario.Revisadores.Contains(actual))
                    _data.RemoverRevisador(cuestionario.Id, actual);
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

        public List<Cuestionario> ListarCuestionario(string oficina)
        {
            List<Cuestionario> resultado = new List<Cuestionario>();

            List<CuestionarioEF> itemsGuardados = _data.Listar();
            foreach (var cuestionario in itemsGuardados.Where(x => x.Eliminado == false))
            {
                Cuestionario temp = toCuestionario(cuestionario);
                temp.Revisadores = _data.listarRevisadores(cuestionario.Id);

                if(temp.Oficina.Contains(oficina))
                    resultado.Add(temp);
            }

            return resultado;
        }

        public List<Cuestionario> ListarCuestionariosRevisador(string revisador)
        {
            List<Cuestionario> resultado = new List<Cuestionario>();

            List<CuestionarioEF> itemsGuardados = _data.Listar();
            foreach (var cuestionario in itemsGuardados.Where(x => x.Eliminado == false))
            {
                Cuestionario temp = toCuestionario(cuestionario);
                temp.Revisadores = _data.listarRevisadores(cuestionario.Id);

                if (temp.Revisadores.Any(x => x == revisador))
                {
                    resultado.Add(temp);
                }
            }

            return resultado;
        }

        public Cuestionario ObtenerPorID(int id)
        {
            CuestionarioEF result = _data.ObtenerPorID(id);

            if (result != null)
            {
                return toCuestionario(result);

            }

            return null;
        }

        public Cuestionario ObtenerCuestionarioPorCodigo(string codigo)
        {
            CuestionarioEF result = _data.ObtenerPorCodigo(codigo);

            if (result != null)
            {
                Cuestionario cargado = toCuestionario(result);
              
                //Se deben cargar todas las preguntas
                PreguntaRN preguntaRN = new PreguntaRN();
                cargado.Preguntas = preguntaRN.ListarPreguntas(cargado.Id);
                cargado.Revisadores = _data.listarRevisadores(cargado.Id);

                return cargado;
            }

            return null;
        }
    }
}
