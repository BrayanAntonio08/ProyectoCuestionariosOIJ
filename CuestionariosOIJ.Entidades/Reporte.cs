using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class Reporte
    {
        private int _id;
        private string _textoPregunta, _tipoRespuesta;
        private object? _resultadosDB; //guarda los datos de la consulta sql

        public Reporte()
        {
            TextoPregunta = string.Empty;
            ResultadosDB = null;
        }

        public string TextoPregunta { get => _textoPregunta; set => _textoPregunta = value; }
        public object? ResultadosDB { get => _resultadosDB; set => _resultadosDB = value; }
        public string TipoRespuesta { get => _tipoRespuesta; set => _tipoRespuesta = value; }
        public int Id { get => _id; set => _id = value; }
    }
}
