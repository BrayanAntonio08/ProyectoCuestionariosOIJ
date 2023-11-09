using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.AccesoDatos.Models
{
    public partial class SubcategoriaEF
    {
        public SubcategoriaEF()
        {
            Preguntas = new HashSet<PreguntaEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CategoriaId { get; set; }

        public virtual CategoriaEF Categoria { get; set; } = null!;
        public virtual ICollection<PreguntaEF> Preguntas { get; set; }
    }
}
