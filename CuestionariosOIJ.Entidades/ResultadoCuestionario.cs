using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class ResultadoCuestionario
    {
        
        private List<Reporte> _reportesPreguntas;
        private Cuestionario _cuestionarioRespondido;

        internal List<Reporte> ReportesPreguntas { get => _reportesPreguntas; set => _reportesPreguntas = value; }
        internal Cuestionario CuestionarioRespondido { get => _cuestionarioRespondido; set => _cuestionarioRespondido = value; }

        public ResultadoCuestionario(Cuestionario cuestionarioRespondido)
        {
            ReportesPreguntas = new List<Reporte>();
            CuestionarioRespondido = cuestionarioRespondido;
        }
    }
}
