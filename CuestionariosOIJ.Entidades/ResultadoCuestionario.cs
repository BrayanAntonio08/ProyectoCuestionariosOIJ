using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class ResultadoCuestionario
    {
        private Usuario _usuarioConsultado;
        private List<Respuesta> _respuestas;
        private Cuestionario _cuestionarioRespondido;
        private DateTime _fechaRespondido;

        public DateTime FechaRespondido { get => _fechaRespondido; set => _fechaRespondido = value; }
        internal Usuario UsuarioConsultado { get => _usuarioConsultado; set => _usuarioConsultado = value; }
        internal List<Respuesta> Respuestas { get => _respuestas; set => _respuestas = value; }
        internal Cuestionario CuestionarioRespondido { get => _cuestionarioRespondido; set => _cuestionarioRespondido = value; }

        public ResultadoCuestionario(DateTime fechaRespondido, Usuario usuarioConsultado, List<Respuesta> respuestas, Cuestionario cuestionarioRespondido)
        {
            FechaRespondido = fechaRespondido;
            UsuarioConsultado = usuarioConsultado;
            Respuestas = respuestas;
            CuestionarioRespondido = cuestionarioRespondido;
        }
    }
}
