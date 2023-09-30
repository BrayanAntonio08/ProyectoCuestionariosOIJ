using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class PermisoEF
    {
        public PermisoEF()
        {
            Rols = new HashSet<Rol>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Entity { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Rol> Rols { get; set; }
    }
}
