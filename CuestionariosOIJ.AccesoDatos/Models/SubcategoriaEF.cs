using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class SubcategoriaEF
    {
        public SubcategoriaEF()
        {
            Pregunta = new HashSet<PreguntaEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int CategoriaId { get; set; }

        public virtual CategoriaEF Categoria { get; set; } = null!;
        public virtual ICollection<PreguntaEF> Pregunta { get; set; }
    }
}
