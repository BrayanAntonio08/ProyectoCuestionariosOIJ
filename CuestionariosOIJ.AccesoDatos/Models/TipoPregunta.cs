using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class TipoPregunta
    {
        public TipoPregunta()
        {
            Pregunta = new HashSet<PreguntaEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<PreguntaEF> Pregunta { get; set; }
    }
}
