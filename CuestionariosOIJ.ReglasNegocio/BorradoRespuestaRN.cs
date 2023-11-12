using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.ReglasNegocio
{
    public class BorradoRespuestaRN
    {
        private readonly RespuestaData _data;

        public BorradoRespuestaRN()
        {
            _data = new RespuestaData(new CuestionariosContext());
        }
        public void EliminarRespuesta(Respuesta respuesta)
        {
            RespuestaEF itemBorrado = new RespuestaEF()
            {
                Id = respuesta.Id
            };

            _data.EliminarRespuesta(itemBorrado);
        }
    }
}
