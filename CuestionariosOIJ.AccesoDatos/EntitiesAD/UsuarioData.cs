using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class UsuarioData
    {
        private readonly CuestionariosContext _db;
        public UsuarioData(CuestionariosContext cuestionariosContext) {
            _db = cuestionariosContext;
        }

        public UsuarioEF IniciarSesion(string nombreUsuario, string contrasena)
        {
            UsuarioEF aux = _db.Usuarios.
                Where(
                    user => user.NombreUsuario == nombreUsuario
                    && user.Contrasena == contrasena
                    && !user.Eliminado
                ).FirstOrDefault();
            if(aux == null )
            {
                return null;
            }

            return aux;
                
        }

        public void ActivarSesion(UsuarioEF usuario)
        {
            usuario.Activo = true;
            _db.Update(usuario);
            _db.SaveChanges();
        }

        public void CerrarSesion(UsuarioEF usuario)
        {
            usuario.Activo = false;
            _db.Update(usuario);
            _db.SaveChanges();
        }
    }
}
