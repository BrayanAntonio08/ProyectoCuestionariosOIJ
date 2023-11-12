using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class JustiicacionRespuestaEF
    {
        public int RespuestaId { get; set; }
        public string Justificacion { get; set; } = null!;

        public virtual RespuestaEF Respuesta { get; set; } = null!;
    }
}
