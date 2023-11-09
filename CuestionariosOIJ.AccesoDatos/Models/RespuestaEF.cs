using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class RespuestaEF
    {
        public RespuestaEF()
        {
            OpcionRespuesta = new HashSet<OpcionRespuestaEF>();
        }

        public int Id { get; set; }
        public string TextoRespuesta { get; set; } = null!;
        public DateTime FechaRespondida { get; set; }
        public DateTime? FechaEliminada { get; set; }
        public int PreguntaId { get; set; }

        public virtual PreguntaEF Pregunta { get; set; } = null!;
        public virtual JustiicacionRespuestaEF? JustiicacionRespuesta { get; set; }
        public virtual UsuarioRespuestaEF? UsuarioRespuesta { get; set; }

        public virtual ICollection<OpcionRespuestaEF> OpcionRespuesta { get; set; }
    }
}
