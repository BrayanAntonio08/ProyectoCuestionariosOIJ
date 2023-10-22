using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class CuestionarioEF
    {
        public CuestionarioEF()
        {
            Pregunta = new HashSet<PreguntaEF>();
            Revisadores = new HashSet<UsuarioEF>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public bool Eliminado { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public int TipoCuestionarioId { get; set; }
        public int OficinaId { get; set; }

        public virtual OficinaEF Oficina { get; set; } = null!;
        public virtual TipoCuestionarioEF TipoCuestionario { get; set; } = null!;
        public virtual ICollection<PreguntaEF> Pregunta { get; set; }
        public virtual ICollection<UsuarioEF> Revisadores { get; set; }
    }
}
