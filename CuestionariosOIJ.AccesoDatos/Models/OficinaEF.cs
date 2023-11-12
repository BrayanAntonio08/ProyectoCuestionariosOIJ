using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class OficinaEF
    {
        public OficinaEF()
        {
            Cuestionarios = new HashSet<CuestionarioEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<CuestionarioEF> Cuestionarios { get; set; }
    }
}
