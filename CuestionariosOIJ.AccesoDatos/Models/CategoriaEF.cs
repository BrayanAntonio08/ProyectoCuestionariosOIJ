using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class CategoriaEF
    {
        public CategoriaEF()
        {
            Pregunta = new HashSet<PreguntaEF>();
            Subcategoria = new HashSet<SubcategoriaEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<PreguntaEF> Pregunta { get; set; }
        public virtual ICollection<SubcategoriaEF> Subcategoria { get; set; }
    }
}
