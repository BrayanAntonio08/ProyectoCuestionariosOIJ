using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Insertar(OpcionRespuestaEF opcionRespuesta)
        {
            _db.OpcionRespuesta.Add(opcionRespuesta);
            _db.SaveChanges();
        }

        public void Actualizar(OpcionRespuestaEF opcionRespuesta)
        {
            _db.OpcionRespuesta.Update(opcionRespuesta);
            _db.SaveChanges();
        }

        public void Eliminar(OpcionRespuestaEF opcionRespuesta)
        {
            _db.OpcionRespuesta.Remove(opcionRespuesta);
            _db.SaveChanges();
        }

        public List<OpcionRespuestaEF> Listar(PreguntaEF pregunta)
        {
            return _db.OpcionRespuesta.
                Where(aux => aux.Pregunta.Equals(pregunta)).
                ToList();
        }
    }
}
