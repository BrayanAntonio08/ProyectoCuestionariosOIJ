using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class OpcionRespuestaData
    {
        private readonly CuestionariosContext _db;

        public OpcionRespuestaData(CuestionariosContext context)
        {
            _db = context;
        }

        public int Insertar(OpcionRespuestaEF opcionRespuesta)
        {
            IEnumerable<OpcionRespuestaEF> exist = _db.OpcionesRespuesta.
                Where(x =>
                    x.TextoOpcion.Equals(opcionRespuesta.TextoOpcion)
                    && x.PreguntaId == opcionRespuesta.PreguntaId);
            if (exist.Count() == 0)
            {
                _db.OpcionesRespuesta.Add(opcionRespuesta);
                _db.SaveChanges();

                return _db.OpcionesRespuesta.
                Where(x =>
                    x.TextoOpcion.Equals(opcionRespuesta.TextoOpcion)
                    && x.PreguntaId == opcionRespuesta.PreguntaId).First().Id;
            }
            return exist.First().Id;
        }

        public void Actualizar(OpcionRespuestaEF opcionRespuesta)
        {
            _db.OpcionesRespuesta.Update(opcionRespuesta);
            _db.SaveChanges();
        }

        public void Eliminar(OpcionRespuestaEF opcionRespuesta)
        {
            _db.OpcionesRespuesta.Remove(opcionRespuesta);
            _db.SaveChanges();
        }

        public List<OpcionRespuestaEF> Listar(int preguntaId)
        {
            return _db.OpcionesRespuesta.
                Where(aux => aux.Pregunta.Id == preguntaId).
                ToList();
        }

        public OpcionRespuestaEF ObtenerPorID(int id)
        {
            return _db.OpcionesRespuesta.Find(id);
        }
    }
}
