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
    public class PreguntaRN
    {
        private readonly PreguntaData _data;
        public PreguntaRN()
        {
            _data = new PreguntaData(new CuestionariosContext());
        }

        public void InsertarPregunta(Pregunta pregunta)
        {
            int posicion = _data.ObtenerUltimaPosicion(pregunta.Cuestionario.Id);

            // Crea un nuevo objeto PreguntaEF
            PreguntaEF nuevoItem = new PreguntaEF()
            {
                CategoriaId = pregunta.Categoria.Id,
                Justificacion = pregunta.Justificacion,
                CuestionarioId = pregunta.Cuestionario.Id,
                Etiqueta = pregunta.Etiqueta,
                Obligatoria = pregunta.Obligatoria,
                SubcategoriaId = pregunta.Subcategoria.Id,
                TipoPreguntaId = _data.BuscarTipoPreguntaPorNombre(pregunta.TipoRespuesta),
                TextoPregunta = pregunta.ContenidoPregunta,
                Posicion = posicion,
            };

            _data.InsertarPregunta(nuevoItem);
        }

        public void ActualizarPregunta(Pregunta pregunta)
        {

            PreguntaEF nuevoItem = new PreguntaEF()
            {
                Id = pregunta.Id,
                CategoriaId = pregunta.Categoria.Id,
                Justificacion = pregunta.Justificacion,
                Etiqueta = pregunta.Etiqueta,
                Obligatoria = pregunta.Obligatoria,
                SubcategoriaId = pregunta.Subcategoria.Id,
                TipoPreguntaId = _data.BuscarTipoPreguntaPorNombre(pregunta.TipoRespuesta),
                TextoPregunta = pregunta.ContenidoPregunta,
                Posicion = pregunta.Posicion,
            };

            _data.ActualizarPregunta(nuevoItem);
        }

        public void EliminarPregunta(int preguntaId)
        {
            PreguntaEF itemBorrado = new PreguntaEF()
            {
                Id = preguntaId
            };

            _data.EliminarPregunta(itemBorrado);
        }


        private Pregunta parsePregunta(PreguntaEF item)
        {
            Pregunta resultado = new Pregunta();
            resultado.Id = item.Id;
            resultado.Categoria = new CategoriaRN().ObtenerPorID((int)item.CategoriaId);
            resultado.Justificacion = item.Justificacion;
            resultado.Etiqueta = item.Etiqueta;
            resultado.Obligatoria = item.Obligatoria;
            resultado.Subcategoria = new SubcategoriaRN().ObtenerPorID((int)item.SubcategoriaId);
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
            CuestionarioEF cuestionarioEF = new CuestionarioData(new CuestionariosContext()).ObtenerPorID(cuestionario.Id);
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
                Pregunta factible = parsePregunta(ef);
                preguntas.Add(factible);
            }
            return preguntas;
        }
    }
}
    

