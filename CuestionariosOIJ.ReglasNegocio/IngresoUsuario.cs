using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.ReglasNegocio
{
    public class IngresoUsuario
    {
        private readonly UsuarioData _data;

        public IngresoUsuario()
        {
            _data = new UsuarioData(new CuestionariosContext());
        }
        public void iniciarSesion(Usuario user)
        {
            _data.IniciarSesion(user.NombreUsuario,user.Contrasenna);
        }
    }
}
