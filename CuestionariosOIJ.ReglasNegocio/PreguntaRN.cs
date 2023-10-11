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
                Posicion = pregunta.Posicion,
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

        public void EliminarPregunta(Pregunta pregunta)
        {
            PreguntaEF itemBorrado = new PreguntaEF()
            {
                Id = pregunta.Id
            };

            _data.EliminarPregunta(itemBorrado);
        }

        public List<Pregunta> ListarPreguntas(Cuestionario cuestionario)
        {

            List<Pregunta> resultado = new List<Pregunta>();
            CuestionarioEF cuestionarioEF = new CuestionarioData(new CuestionariosContext()).ObtenerPorID(cuestionario.Id);
            List<PreguntaEF> itemsGuardados = _data.ListarPreguntas(cuestionarioEF);
            foreach (var item in itemsGuardados)
            {
                resultado.Add(
                   new Pregunta()
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
                   }
                   );
            }
            return resultado;

        }
    
       public Pregunta ObtenerPreguntaPorID(int id)
         {
            PreguntaEF item = _data.ObtenerPreguntaPorID(id);
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
    }
}
    

