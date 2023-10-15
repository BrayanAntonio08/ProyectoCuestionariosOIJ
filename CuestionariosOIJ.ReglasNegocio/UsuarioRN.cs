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
    public class UsuarioRN
    {
        private readonly UsuarioData _data;

        public UsuarioRN()
        {
            _data = new UsuarioData(new CuestionariosContext());
        }
        public void iniciarSesion(Usuario user)
        {
            _data.IniciarSesion(user.NombreUsuario,user.Contrasenna);
        }

        public List<Usuario> ListarUsuariosRevisadores(int cuestionarioId)
        {
            List<UsuarioEF> listaDb = _data.ListarRevisadores(cuestionarioId);
            List<Usuario> resultado = new List<Usuario>();
            foreach (var usuario in listaDb)
            {
                resultado.Add(new Usuario()
                {
                    Id = usuario.Id,
                    NombreUsuario = usuario.NombreUsuario
                });
            }
            return resultado;
        }
            
            
        
    }
}
