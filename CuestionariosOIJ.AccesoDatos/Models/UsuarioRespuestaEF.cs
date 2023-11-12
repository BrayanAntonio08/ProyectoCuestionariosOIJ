using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class UsuarioRespuestaEF
    {
        public int RespuestaId { get; set; }
        public string Usuario { get; set; } = null!;

        public virtual RespuestaEF Respuesta { get; set; } = null!;
    }
}
