using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class OpcionRespuestaEF
    {
        public OpcionRespuestaEF()
        {
            Respuestas = new HashSet<RespuestaEF>();
        }

        public int Id { get; set; }
        public int PreguntaId { get; set; }
        public string TextoOpcion { get; set; } = null!;

        public virtual PreguntaEF Pregunta { get; set; } = null!;

        public virtual ICollection<RespuestaEF> Respuestas { get; set; }
    }
}
