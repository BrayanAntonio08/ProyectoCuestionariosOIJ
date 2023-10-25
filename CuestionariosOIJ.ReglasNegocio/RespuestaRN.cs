using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CuestionariosOIJ.ReglasNegocio
{
    public class RespuestaRN
    {

        private readonly RespuestaData _data;

        public RespuestaRN()
        {
            _data = new RespuestaData(new CuestionariosContext());
        }

        public void InsertarRespuesta(Respuesta respuesta)
        {
            // Crea un nuevo objeto RespuestaEF
            RespuestaEF nuevoItem = new RespuestaEF()
            {
                TextoRespuesta = respuesta.TextoRespuesta,
                FechaRespondida = DateTime.Now,
                PreguntaId = respuesta.PreguntaRespondida.Id,
                UsuarioId = respuesta.Encuestado,
            };

            _data.InsertarRespuesta(nuevoItem);

        }

        public void BorrarRespuestasCuestionario(Cuestionario cuestionario) {
            CuestionarioEF cuestionarioEF = new CuestionarioData(new CuestionariosContext()).ObtenerPorID(cuestionario.Id);
            List<RespuestaEF> respuestas = _data.ListarRespuestas(cuestionarioEF);
            foreach (var respuesta in respuestas)
            {
                respuesta.FechaRespondida = DateTime.Now;
                _data.ActualizarRespuesta(respuesta);
            }
        }

        public void EliminarRespuesta(Respuesta respuesta)
        {
            RespuestaEF nuevoItem = new RespuestaEF()
            {
                Id = respuesta.Id
            };

            _data.EliminarRespuesta(nuevoItem);
        }

        public List<Respuesta> ListarRespuestasTotales(Cuestionario cuestionario)
        {
            List<Respuesta> resultado = new List<Respuesta>();
            CuestionarioEF cuestionarioEF = new CuestionarioData(new CuestionariosContext()).ObtenerPorID(cuestionario.Id);
            List<RespuestaEF> itemsGuardados = _data.ListarRespuestasTotales(cuestionarioEF);
            foreach (var item in itemsGuardados)
            {
                resultado.Add(
                    new Respuesta()
                    {
                        Id = item.Id,
                        TextoRespuesta = item.TextoRespuesta,
                        TipoRespuesta = item.Pregunta.TipoPregunta.Nombre,
                        PreguntaRespondida = new Pregunta() { 
                        Id = item.Pregunta.Id,
                        ContenidoPregunta = item.Pregunta.TextoPregunta
                        },
                        Encuestado = item.UsuarioId,
                        Periodo = item.FechaEliminada,
                    }
                );
            }
            return resultado;
        }

        public List<Respuesta> ListarRespuestasPorPregunta(Pregunta pregunta)
        {
            List<Respuesta> resultado = new List<Respuesta>();
            PreguntaEF preguntaEF = new PreguntaEF()
            {
                Id= pregunta.Id
            };
            List<RespuestaEF> itemsGuardados = _data.ListarRespuestasPorPregunta(preguntaEF);
            foreach (var item in itemsGuardados)
            {
                resultado.Add(
                    new Respuesta()
                    {
                        Id = item.Id,
                        TextoRespuesta = item.TextoRespuesta,
                        TipoRespuesta = item.Pregunta.TipoPregunta.Nombre,
                        PreguntaRespondida = new Pregunta()
                        {
                            Id = item.Pregunta.Id,
                            ContenidoPregunta = item.Pregunta.TextoPregunta
                        },
                        Encuestado = item.UsuarioId,
                        Periodo = item.FechaEliminada,
                    }
                );
            }
            return resultado;
        }

        public List<Respuesta> ListarRespuestasTotalesPorPregunta(Pregunta pregunta)
        {
            List<Respuesta> resultado = new List<Respuesta>();
            PreguntaEF preguntaEF = new PreguntaEF()
            {
                Id = pregunta.Id
            };
            List<RespuestaEF> itemsGuardados = _data.ListarRespuestasTotalesPorPregunta(preguntaEF);
            foreach (var item in itemsGuardados)
            {
                resultado.Add(
                    new Respuesta()
                    {
                        Id = item.Id,
                        TextoRespuesta = item.TextoRespuesta,
                        TipoRespuesta = item.Pregunta.TipoPregunta.Nombre,
                        Encuestado = item.UsuarioId,
                        Periodo = item.FechaEliminada,
                    }
                );
            }
            return resultado;
        }



    }
}
