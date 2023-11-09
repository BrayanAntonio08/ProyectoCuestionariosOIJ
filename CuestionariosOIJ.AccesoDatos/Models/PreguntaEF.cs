using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class PreguntaEF
    {
        public PreguntaEF()
        {
            OpcionesRespuesta = new HashSet<OpcionRespuestaEF>();
            Respuestas = new HashSet<RespuestaEF>();
        }

        public int Id { get; set; }
        public string TextoPregunta { get; set; } = null!;
        public int Posicion { get; set; }
        public string? Etiqueta { get; set; }
        public bool Justificacion { get; set; }
        public bool Obligatoria { get; set; }
        public int? CategoriaId { get; set; }
        public int? SubcategoriaId { get; set; }
        public int TipoPreguntaId { get; set; }
        public int CuestionarioId { get; set; }
        public bool Eliminado { get; set; }
        public virtual CategoriaEF? Categoria { get; set; }
        public virtual CuestionarioEF Cuestionario { get; set; } = null!;
        public virtual SubcategoriaEF? Subcategoria { get; set; }
        public virtual TipoPreguntaEF TipoPregunta { get; set; } = null!;
        public virtual ICollection<OpcionRespuestaEF> OpcionesRespuesta { get; set; }
        public virtual ICollection<RespuestaEF> Respuestas { get; set; }
    }
}
