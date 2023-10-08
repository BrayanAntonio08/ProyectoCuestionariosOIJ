using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Permisos = new HashSet<PermisoEF>();
            Usuarios = new HashSet<UsuarioEF>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<PermisoEF> Permisos { get; set; }
        public virtual ICollection<UsuarioEF> Usuarios { get; set; }
    }
}
