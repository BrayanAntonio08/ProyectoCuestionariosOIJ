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
    public class OpcionRespuestaRN
    {
        private readonly OpcionRespuestaData _data;

        public OpcionRespuestaRN()
        {
            _data = new OpcionRespuestaData(new CuestionariosContext());
        }


        public void InsertarOpcionRespuesta(Pregunta pregunta)
        {
            foreach(OpcionRespuesta opcion in pregunta.Opciones) { 
                OpcionRespuestaEF nuevoItem = new OpcionRespuestaEF()
                {
                    PreguntaId = pregunta.Id,
                    TextoOpcion = opcion.TextoOpcion,
                };

                _data.Insertar(nuevoItem);
            }
        }
        
        public List<OpcionRespuesta> ListarOpcionesRespuesta(Pregunta pregunta)
        {
            PreguntaEF origen = new PreguntaData(new CuestionariosContext()).ObtenerPreguntaPorID(pregunta.Id);
            List<OpcionRespuestaEF> datos = _data.Listar(origen);

            List<OpcionRespuesta> resultado = new List<OpcionRespuesta>();
            foreach(OpcionRespuestaEF objeto in  datos)
            {
                resultado.Add(new OpcionRespuesta(objeto.Id, objeto.TextoOpcion));
            }

            return resultado;
        }
    }
}
