using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class CuestionarioEF
    {
        public CuestionarioEF()
        {
            Preguntas = new HashSet<PreguntaEF>();
            RevisadorCuestionarios = new HashSet<RevisadorCuestionarioEF>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public int TipoCuestionarioId { get; set; }
        public int OficinaId { get; set; }
        public bool Eliminado { get; set; }
        public virtual OficinaEF Oficina { get; set; } = null!;
        public virtual TipoCuestionarioEF TipoCuestionario { get; set; } = null!;
        public virtual ICollection<PreguntaEF> Preguntas { get; set; }
        public virtual ICollection<RevisadorCuestionarioEF> RevisadorCuestionarios { get; set; }
    }
}
