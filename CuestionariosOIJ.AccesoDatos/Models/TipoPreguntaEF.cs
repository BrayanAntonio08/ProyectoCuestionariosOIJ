using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class TipoPreguntaEF
    {
        public TipoPreguntaEF()
        {
            Preguntas = new HashSet<PreguntaEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<PreguntaEF> Preguntas { get; set; }
    }
}
