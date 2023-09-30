using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class OpcionRespuesta
    {
        private int _id;
        private string _textoOpcion;

        public OpcionRespuesta(int id, string textoOpcion)
        {
            Id = id;
            TextoOpcion = textoOpcion;
        }

        public int Id { get => _id; set => _id = value; }
        public string TextoOpcion { get => _textoOpcion; set => _textoOpcion = value; }
    }
}
