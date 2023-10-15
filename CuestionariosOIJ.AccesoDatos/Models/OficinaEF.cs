using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class OficinaEF
    {
        public OficinaEF()
        {
            Cuestionarios = new HashSet<CuestionarioEF>();
            Usuarios = new HashSet<UsuarioEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public bool Eliminado { get; set; }

        public virtual ICollection<CuestionarioEF> Cuestionarios { get; set; }
        public virtual ICollection<UsuarioEF> Usuarios { get; set; }
    }
}
