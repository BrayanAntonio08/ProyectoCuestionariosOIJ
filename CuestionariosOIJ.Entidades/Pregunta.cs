using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class Pregunta
    {
        private int _id, _posicion;
        private Categoria _categoria;
        private Subcategoria _subcategoria;
        private string _etiqueta, _contenidoPregunta, _tipoRespuesta;
        private bool _justificacion, _obligatoria;
        private List<OpcionRespuesta> _opciones;
        private Cuestionario? _cuestionario;

        public Pregunta()
        {
        }

        public Pregunta(int id, string etiqueta, string contenidoPregunta, string tipoRespuesta, bool justificacion, bool obligatoria, Categoria categoria, Subcategoria subcategoria, List<OpcionRespuesta> opciones, Cuestionario cuestionario)
        {
            Id = id;
            Etiqueta = etiqueta;
            ContenidoPregunta = contenidoPregunta;
            TipoRespuesta = tipoRespuesta;
            Justificacion = justificacion;
            Obligatoria = obligatoria;
            Categoria = categoria;
            Subcategoria = subcategoria;
            Opciones = opciones;
            Cuestionario = cuestionario;
        }


        public string Etiqueta { get => _etiqueta; set => _etiqueta = value; }
        public string ContenidoPregunta { get => _contenidoPregunta; set => _contenidoPregunta = value; }
        public string TipoRespuesta { get => _tipoRespuesta; set => _tipoRespuesta = value; }
        public bool Justificacion { get => _justificacion; set => _justificacion = value; }
        public bool Obligatoria { get => _obligatoria; set => _obligatoria = value; }
        public Categoria Categoria { get => _categoria; set => _categoria = value; }
        public Subcategoria Subcategoria { get => _subcategoria; set => _subcategoria = value; }
        public List<OpcionRespuesta> Opciones { get => _opciones; set => _opciones = value; }
        public Cuestionario? Cuestionario { get => _cuestionario; set => _cuestionario = value; }
        public int Id { get => _id; set => _id = value; }
        public int Posicion { get => _posicion; set => _posicion = value; }
    }
}
