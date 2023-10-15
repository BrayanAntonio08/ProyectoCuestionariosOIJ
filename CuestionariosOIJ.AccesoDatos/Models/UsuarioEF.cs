using System;
using System.Collections.Generic;

namespace CuestionariosOIJ.API.Models
{
    public partial class UsuarioEF
    {
        public UsuarioEF()
        {
            Respuesta = new HashSet<RespuestaEF>();
            Cuestionarios = new HashSet<CuestionarioEF>();
            Rols = new HashSet<Rol>();
        }

        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public string Correo { get; set; } = null!;
        public bool Eliminado { get; set; }
        public bool Activo { get; set; }
        public int OficinaId { get; set; }

        public virtual OficinaEF Oficina { get; set; } = null!;
        public virtual ICollection<RespuestaEF> Respuesta { get; set; }

        public virtual ICollection<CuestionarioEF> Cuestionarios { get; set; }

        public virtual ICollection<Rol> Rols { get; set; }
    }
}
