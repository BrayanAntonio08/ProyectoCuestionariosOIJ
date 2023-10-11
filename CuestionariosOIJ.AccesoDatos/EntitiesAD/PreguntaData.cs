using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class PreguntaData
    {
        private readonly CuestionariosContext _db;

        public PreguntaData(CuestionariosContext context)
        {
            _db = context;
        }

        public void InsertarPregunta(PreguntaEF pregunta)
        {
            _db.Pregunta.Add(pregunta);
            _db.SaveChanges();
        }

        public void ActualizarPregunta(PreguntaEF pregunta)
        {
            _db.Pregunta.Update(pregunta);
            _db.SaveChanges();
        }

        public void EliminarPregunta(PreguntaEF pregunta)
        {
            _db.Pregunta.Remove(pregunta);
            _db.SaveChanges();
        }

        public List<PreguntaEF> ListarPreguntas(CuestionarioEF cuestionario)
        {
            return _db.Pregunta.
                Where(aux => aux.Cuestionario.Equals(cuestionario)).
                ToList();
        }

        public PreguntaEF ObtenerPreguntaPorID(int id)
        {
            return _db.Pregunta.Find(id);
        }

        public PreguntaEF ObtenerPreguntaEn(CuestionarioEF cuestionario, int posicion)
        {
            return _db.Pregunta.
                Where(aux => aux.Cuestionario.Equals(cuestionario)
                    && aux.Posicion == posicion).
                First();
        }

        public int BuscarTipoPreguntaPorNombre(string nombre) {
            return _db.TipoPregunta.Where(x => x.Nombre.Equals(nombre)).First().Id;
        }

    }
}
