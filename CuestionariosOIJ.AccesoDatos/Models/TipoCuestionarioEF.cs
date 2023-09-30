using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class TipoCuestionarioEF
    {
        public TipoCuestionarioEF()
        {
            Cuestionarios = new HashSet<CuestionarioEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<CuestionarioEF> Cuestionarios { get; set; }
    }
}
