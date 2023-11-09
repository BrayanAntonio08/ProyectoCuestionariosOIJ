using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class Respuesta
    {
        private int _id;
        private string _tipoRespuesta, _textoRespuesta;
        private List<OpcionRespuesta> _opcionesEscogidas;
        private Pregunta _preguntaRespondida;
        private string? _encuestado; //que guarde solo el id
        private DateTime? _periodo;

        public Respuesta() { }

        public string TipoRespuesta { get => _tipoRespuesta; set => _tipoRespuesta = value; }
        public string TextoRespuesta { get => _textoRespuesta; set => _textoRespuesta = value; }
        public List<OpcionRespuesta> OpcionesEscogidas { get => _opcionesEscogidas; set => _opcionesEscogidas = value; }
        public Pregunta PreguntaRespondida { get => _preguntaRespondida; set => _preguntaRespondida = value; }
        public int Id { get => _id; set => _id = value; }
        public string? Encuestado { get => _encuestado; set => _encuestado = value; }
        public DateTime? Periodo { get => _periodo; set => _periodo = value; }
    }
}
