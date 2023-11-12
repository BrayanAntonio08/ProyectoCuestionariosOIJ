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
    public class PreguntaRN
    {
        private readonly PreguntaData _data;
        public PreguntaRN()
        {
            _data = new PreguntaData(new CuestionariosContext());
        }

        private PreguntaEF toPreguntaEF(Pregunta pregunta)
        {
            int cuestId = pregunta.Cuestionario != null? pregunta.Cuestionario.Id : 0;
            int? catId = pregunta.Categoria != null ? pregunta.Categoria.Id : null;
            int? subcatId = pregunta.Subcategoria != null ? pregunta.Subcategoria.Id : null;
            int tipoId = _data.BuscarTipoPreguntaPorNombre(pregunta.TipoRespuesta);
            return new PreguntaEF()
            {
                Id = pregunta.Id,
                CategoriaId = catId,
                SubcategoriaId = subcatId,
                CuestionarioId = cuestId,
                Eliminado = false,
                Etiqueta = pregunta.Etiqueta,
                Justificacion = pregunta.Justificacion,
                Posicion = pregunta.Posicion,
                Obligatoria = pregunta.Obligatoria,
                TextoPregunta = pregunta.ContenidoPregunta,
                TipoPreguntaId = tipoId
            };
        }
        public Pregunta InsertarPregunta(Pregunta pregunta)
        {
            int posicion = _data.ObtenerUltimaPosicion(pregunta.Cuestionario.Id);

            // Crea un nuevo objeto PreguntaEF
            PreguntaEF nuevoItem = toPreguntaEF(pregunta);

            pregunta.Id = _data.InsertarPregunta(nuevoItem);
            return pregunta;
        }

        public void ActualizarPregunta(int cuestionarioId, Pregunta pregunta)
        {

            PreguntaEF nuevoItem = toPreguntaEF(pregunta);
            nuevoItem.CuestionarioId = cuestionarioId;

            _data.ActualizarPregunta(nuevoItem);
        }

        public void EliminarPregunta(int preguntaId)
        {
            PreguntaEF itemBorrado = _data.ObtenerPreguntaPorID(preguntaId);

            _data.EliminarPregunta(itemBorrado);
        }


        private Pregunta parsePregunta(PreguntaEF item)
        {
            Pregunta resultado = new Pregunta();
            resultado.Id = item.Id;
            resultado.Categoria = new CategoriaRN().ObtenerPorID(item.CategoriaId);
            resultado.Justificacion = item.Justificacion;
            resultado.Etiqueta = item.Etiqueta;
            resultado.Obligatoria = item.Obligatoria;
            resultado.Subcategoria = new SubcategoriaRN().ObtenerPorID(item.SubcategoriaId);
            resultado.TipoRespuesta = item.TipoPregunta.Nombre;
            resultado.ContenidoPregunta = item.TextoPregunta;
            resultado.Posicion = item.Posicion;
            resultado.Opciones = new OpcionRespuestaRN().ListarOpcionesRespuesta(item.Id);

            return resultado;
        }

        public List<Pregunta> ListarPreguntas(int cuestionarioID)
        {

            List<Pregunta> resultado = new List<Pregunta>();
            List<PreguntaEF> itemsGuardados = (List<PreguntaEF>)_data.ListarPreguntas(cuestionarioID);
            foreach (var item in itemsGuardados)
            {
                resultado.Add(parsePregunta(item));
            }
            return resultado;

        }
    
       public Pregunta ObtenerPreguntaPorID(int id)
         {
            PreguntaEF item = _data.ObtenerPreguntaPorID(id);
            Pregunta resultado = parsePregunta(item);

            return resultado;
        }

        public Pregunta ObtenerPreguntaEn(Cuestionario cuestionario, int posicion)
        {
            CuestionarioEF cuestionarioEF = new CuestionarioData().ObtenerPorID(cuestionario.Id);
            PreguntaEF item = _data.ObtenerPreguntaEn(cuestionarioEF, posicion);
            Pregunta resultado = new Pregunta()
            {
                Id = item.Id,
                Categoria = new Categoria()
                {
                    Id = item.Categoria.Id,
                    Nombre = item.Categoria.Nombre
                },
                Justificacion = item.Justificacion,
                Etiqueta = item.Etiqueta,
                Obligatoria = item.Obligatoria,
                Subcategoria = new Subcategoria()
                {
                    Id = item.Subcategoria.Id,
                    Nombre = item.Subcategoria.Nombre
                },
                TipoRespuesta = item.TipoPregunta.Nombre,
                ContenidoPregunta = item.TextoPregunta,
                Posicion = item.Posicion,
            };

            return resultado;
        }

        public List<Pregunta> ListarPreguntasSeleccion(int cuestionario)
        {
            IEnumerable<PreguntaEF> list = _data.ListarPreguntasSeleccion(cuestionario);
            List<Pregunta> preguntas = new List<Pregunta>();

            foreach(PreguntaEF ef in list)
            {
                if (!ef.Eliminado)
                {
                    Pregunta factible = parsePregunta(ef);
                    preguntas.Add(factible);
                }
            }
            return preguntas;
        }
    }
}
    

