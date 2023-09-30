using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class OpcionRespuestaEF
    {
        public OpcionRespuestaEF()
        {
            Respuesta = new HashSet<RespuestaEF>();
        }

        public int Id { get; set; }
        public int PreguntaId { get; set; }
        public string TextoOpcion { get; set; } = null!;

        public virtual PreguntaEF Pregunta { get; set; } = null!;

        public virtual ICollection<RespuestaEF> Respuesta { get; set; }
    }
}
