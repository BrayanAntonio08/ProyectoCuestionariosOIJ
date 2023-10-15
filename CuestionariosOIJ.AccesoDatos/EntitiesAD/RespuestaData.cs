using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class RespuestaData
    {
        private readonly CuestionariosContext _db;

        public RespuestaData(CuestionariosContext context)
        {
            _db = context;
        }

        public void InsertarRespuesta(RespuestaEF respuesta)
        {
            _db.Respuesta.Add(respuesta);
            _db.SaveChanges();
        }

        public void ActualizarRespuesta(RespuestaEF respuesta)
        {
            _db.Respuesta.Update(respuesta);
            _db.SaveChanges();
        }

        public void EliminarRespuesta(RespuestaEF respuesta)
        {
            _db.Respuesta.Remove(respuesta);
            _db.SaveChanges();
        }

        public List<RespuestaEF> ListarRespuestas(CuestionarioEF cuestionario)
        {
            return _db.Respuesta.
                Where(aux => aux.Pregunta.Cuestionario.Equals(cuestionario)
                        && aux.FechaEliminada == null).
                ToList();
        }

        public List<RespuestaEF> ListarRespuestasTotales(CuestionarioEF cuestionario)
        {
            return _db.Respuesta.
                Where(aux => aux.Pregunta.Cuestionario.Equals(cuestionario)).
                ToList();
        }

        public List<RespuestaEF> ListarRespuestasPorPregunta(PreguntaEF pregunta)
        {
            return _db.Respuesta.
                Where(aux => aux.Pregunta.Equals(pregunta)
                        && aux.FechaEliminada == null).
                ToList();
        }

        public List<RespuestaEF> ListarRespuestasTotalesPorPregunta(PreguntaEF pregunta)
        {
            return _db.Respuesta.
                Where(aux => aux.Pregunta.Equals(pregunta)).
                ToList();
        }

        public RespuestaEF ObtenerPorId(int id) {
            return _db.Respuesta.Find(id);
        }
    }
}
