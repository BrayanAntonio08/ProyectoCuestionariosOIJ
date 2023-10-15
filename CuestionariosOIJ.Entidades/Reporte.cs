using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class Reporte
    {
        private List<ResultadoCuestionario> _resultadosCuestionario;

        internal List<ResultadoCuestionario> ResultadosCuestionario { get => _resultadosCuestionario; set => _resultadosCuestionario = value; }

        public Reporte(List<ResultadoCuestionario> resultadosCuestionario)
        {
            ResultadosCuestionario = resultadosCuestionario;
        }

        public Reporte()
        {
            ResultadosCuestionario = new List<ResultadoCuestionario>();
        }


    }
}
