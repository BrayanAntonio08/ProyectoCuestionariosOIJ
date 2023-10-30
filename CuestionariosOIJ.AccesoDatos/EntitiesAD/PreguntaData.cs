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

        public int InsertarPregunta(PreguntaEF pregunta)
        {
            _db.Pregunta.Add(pregunta);
            _db.SaveChanges();
            return _db.Pregunta.Max(aux=>aux.Id);
        }

        public void ActualizarPregunta(PreguntaEF pregunta)
        {
            _db.Pregunta.Update(pregunta);
            _db.SaveChanges();
        }


        public void EliminarPregunta(PreguntaEF pregunta)
        {
            pregunta.Eliminado = true;
            _db.Pregunta.Update(pregunta);
            _db.SaveChanges();
        }

        public IEnumerable<PreguntaEF> ListarPreguntas(int cuestionarioId)
        {
            IEnumerable<PreguntaEF> temp = _db.Pregunta
                .Where(aux => aux.Cuestionario.Id == cuestionarioId && !aux.Eliminado)
                .OrderBy(aux => aux.Posicion)
                .ToList();


            foreach (PreguntaEF ef in temp)
            {
                ef.TipoPregunta = _db.TipoPregunta.Find(ef.TipoPreguntaId);
            }
            return temp;
        }

        public PreguntaEF ObtenerPreguntaPorID(int id)
        {
            PreguntaEF temp = _db.Pregunta.Find(id);
            if(temp != null)
            {
                temp.TipoPregunta = _db.TipoPregunta.Find(id);
            }
            return temp;
        }

        public PreguntaEF ObtenerPreguntaEn(CuestionarioEF cuestionario, int posicion)
        {
            return _db.Pregunta.
                Where(aux => aux.Cuestionario.Equals(cuestionario)
                    && aux.Posicion == posicion).
                First();
        }

        public int BuscarTipoPreguntaPorNombre(string nombre)
        {
            return _db.TipoPregunta.Where(x => x.Nombre.Equals(nombre)).First().Id;
        }

        public string consultarTipoPregunta(int id)
        {
            TipoPregunta ef = _db.TipoPregunta.Find(id);
            if(ef != null)
                return ef.Nombre;
            return "";
        }


        public int ObtenerUltimaPosicion(int cuestionario)
        {
            IEnumerable<PreguntaEF> list = _db.Pregunta.Where(aux => aux.CuestionarioId == cuestionario).ToList();
            if (list.Count() == 0)
                return 1;
            int pos = list.Max(x => x.Posicion);
            return pos+1;
        }

        public IEnumerable<PreguntaEF> ListarPreguntasSeleccion(int cuestionario)
        {
            IEnumerable<PreguntaEF> temp = _db.Pregunta.
                Where(aux => aux.CuestionarioId == cuestionario 
                && aux.TipoPregunta.Nombre.StartsWith("Seleccion"));
            List<TipoPregunta> tipos = _db.TipoPregunta.ToList();

            foreach (PreguntaEF ef in temp)
            {
                ef.TipoPregunta = tipos.Find(tipo => tipo.Id == ef.TipoPreguntaId);
            }
            return temp;
        }
    }
}
