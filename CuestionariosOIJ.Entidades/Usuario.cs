using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class Usuario
    {
        private string 
            _nombreUsuario, 
            _contrasenna,
            _nombre,
            _primerApellido,
            _segundoApellido,
            _correo,
            _oficinaDesignada;

        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string Contrasenna { get => _contrasenna; set => _contrasenna = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string PrimerApellido { get => _primerApellido; set => _primerApellido = value; }
        public string SegundoApellido { get => _segundoApellido; set => _segundoApellido = value; }
        public string Correo { get => _correo; set => _correo = value; }
        public string OficinaDesignada { get => _oficinaDesignada; set => _oficinaDesignada = value; }



        public Usuario(string nombreUsuario, string contrasenna, string nombre, string primerApellido, string segundoApellido, string correo, string oficinaDesignada)
        {
            NombreUsuario = nombreUsuario;
            Contrasenna = contrasenna;
            Nombre = nombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
            Correo = correo;
            OficinaDesignada = oficinaDesignada;
        }

        public Usuario()
        {
            NombreUsuario = string.Empty;
            Contrasenna = string.Empty;
            Nombre = string.Empty;
            PrimerApellido = string.Empty;
            SegundoApellido = string.Empty;
            Correo = string.Empty;
            OficinaDesignada = string.Empty;
        }
    }
}
