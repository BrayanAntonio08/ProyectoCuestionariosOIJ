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


        public int InsertarOpcionRespuesta(int preguntaId, OpcionRespuesta opcion)
        {

            OpcionRespuestaEF nuevoItem = new OpcionRespuestaEF()
            {
                PreguntaId = preguntaId,
                TextoOpcion = opcion.TextoOpcion,
            };

            return _data.Insertar(nuevoItem);
        }

        public List<OpcionRespuesta> ListarOpcionesRespuesta(int preguntaId)
        {
            List<OpcionRespuestaEF> datos = _data.Listar(preguntaId);

            List<OpcionRespuesta> resultado = new List<OpcionRespuesta>();
            foreach (OpcionRespuestaEF objeto in datos)
            {
                resultado.Add(new OpcionRespuesta(objeto.Id, objeto.TextoOpcion));
            }

            return resultado;
        }

        public OpcionRespuesta ObtenerPorID(int id)
        {
            OpcionRespuestaEF temp = _data.ObtenerPorID(id);
            return new OpcionRespuesta(temp.Id, temp.TextoOpcion);
        }

        public bool eliminarOpcionRespuesta(int id)
        {
            OpcionRespuestaEF opcion = _data.ObtenerPorID(id);
            if (opcion == null)
            {
                return false;
            }
            _data.Eliminar(opcion);
            return true;

        }

        public OpcionRespuesta actualizarOpcionRespuesta(OpcionRespuesta opcion)
        {
            OpcionRespuestaEF opcionEF = _data.ObtenerPorID(opcion.Id);
            if (opcionEF == null)
            {
                return null;
            }

            opcionEF.TextoOpcion = opcion.TextoOpcion;
            _data.Actualizar(opcionEF);
            return opcion;
        }
    }
}
