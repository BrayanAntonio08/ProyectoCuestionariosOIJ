using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class CategoriaEF
    {
        public CategoriaEF()
        {
            Preguntas = new HashSet<PreguntaEF>();
            Subcategorias = new HashSet<SubcategoriaEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<PreguntaEF> Preguntas { get; set; }
        public virtual ICollection<SubcategoriaEF> Subcategorias { get; set; }
    }
}
