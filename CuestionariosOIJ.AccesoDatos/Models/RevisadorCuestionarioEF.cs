using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class RevisadorCuestionarioEF
    {
        public string Revisador { get; set; } = null!;
        public int CuestionarioId { get; set; }

        public virtual CuestionarioEF Cuestionario { get; set; } = null!;
    }
}
