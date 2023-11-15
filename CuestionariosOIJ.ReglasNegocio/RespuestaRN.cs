using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.AccesoDatos.Models;
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

        public void InsertarRespuesta(Respuesta respuesta, string respuestaCuestionarioID)
        {

            // Crea un nuevo objeto RespuestaEF
            RespuestaEF nuevoItem = new RespuestaEF()
            {
                TextoRespuesta = respuesta.TextoRespuesta,
                FechaRespondida = DateTime.Now,
                PreguntaId = respuesta.PreguntaRespondida.Id,
                RespuestaCuestionarioId = respuestaCuestionarioID
            };

            int id = _data.InsertarRespuesta(nuevoItem);

            foreach (OpcionRespuesta opcion in respuesta.OpcionesEscogidas)
            {
                _data.AgregarOpcionEscogida(opcion.Id, id);
            }

            if(respuesta.Encuestado != null)
            {
                _data.AsignarEncuestado(id, respuesta.Encuestado);
            }
        }

        public void BorrarRespuestasCuestionario(int cuestionario) {
            CuestionarioEF cuestionarioEF = new CuestionarioData().ObtenerPorID(cuestionario);
            List<RespuestaEF> respuestas = _data.ListarRespuestas(cuestionarioEF);
            foreach (var respuesta in respuestas)
            {
                RespuestaEF resultado = _data.EliminarRespuesta(respuesta.Id);
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

        public List<Respuesta> ListarRespuestasTotales(int cuestionario)
        {
            List<Respuesta> resultado = new List<Respuesta>();
            CuestionarioEF cuestionarioEF = new CuestionarioData().ObtenerPorID(cuestionario);
            List<RespuestaEF> itemsGuardados = _data.ListarRespuestasTotales(cuestionarioEF);

            PreguntaRN preguntaRN = new PreguntaRN();
            foreach (var item in itemsGuardados)
            {
                Pregunta pregunta = preguntaRN.ObtenerPreguntaPorID(item.PreguntaId);
                resultado.Add(
                    new Respuesta()
                    {
                        Id = item.Id,
                        TextoRespuesta = item.TextoRespuesta,
                        TipoRespuesta = pregunta.TipoRespuesta,
                        PreguntaRespondida = pregunta,
                        Encuestado = item.UsuarioRespuesta?.Usuario,
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
                        Encuestado = item.UsuarioRespuesta?.Usuario,
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
                        Encuestado = item.UsuarioRespuesta?.Usuario,
                        Periodo = item.FechaEliminada,
                    }
                );
            }
            return resultado;
        }

        public List<Dictionary<string, object>> ReporteExcel(int cuestionarioId)
        {
            List<Dictionary<string, object>> resultados = new List<Dictionary<string, object>>();
            List<RespuestaData.RespuestaUsuario> respuestasCuestionarios = _data.ListarUsuariosRespondidos(cuestionarioId);
            List<Pregunta> preguntas = new PreguntaRN().ListarPreguntas(cuestionarioId);

            foreach(RespuestaData.RespuestaUsuario respuestaCuestionario in respuestasCuestionarios)
            {
                Dictionary<string, object> resultado = new Dictionary<string, object>();
                resultado.Add("Codigo", respuestaCuestionario.RespuestaCuestionarioId);
                resultado.Add("Fecha", respuestaCuestionario.Fecha.ToShortDateString());
                resultado.Add("Usuario", respuestaCuestionario.Usuario);
                
                //cargar las preguntas
                foreach(Pregunta pregunta in preguntas)
                {
                    resultado.Add(pregunta.ContenidoPregunta, "");
                }

                //cargar respuestas en los espacios de las preguntas
                List<object> respuestas = _data.ObtenerRespuestas(respuestaCuestionario.RespuestaCuestionarioId);
                foreach (var respuesta in respuestas)
                {
                    string respuestaString = (string) respuesta.GetType().GetProperty("Respuesta").GetValue(respuesta);
                    if(respuestaString == "EX100-vary")
                    {
                        respuestaString = _data.LeerOpcionesRespuesta((int)respuesta.GetType().GetProperty("Id").GetValue(respuesta));
                    }
                    resultado[(string)respuesta.GetType().GetProperty("Pregunta").GetValue(respuesta)] = respuestaString;
                }
                resultados.Add(resultado);
            }
            return resultados;
        }

    }
}
