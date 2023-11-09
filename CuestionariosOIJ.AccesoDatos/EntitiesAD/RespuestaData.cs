using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class RespuestaData
    {
        private readonly CuestionariosContext _db;
        private DataBaseManager _dbManager;

        internal DataBaseManager DbManager { get => _dbManager; set => _dbManager = value; }

        public RespuestaData(CuestionariosContext context)
        {
            _db = context;
            DbManager = new DataBaseManager();
        }

        public int InsertarRespuesta(RespuestaEF respuesta)
        {
            _db.Respuestas.Add(respuesta);
            _db.SaveChanges();

            return respuesta.Id;
        }

        public void AgregarOpcionEscogida(int opcionId, int respuestaId)
        {
            //sp_agregar_opcion_escogida
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_agregar_opcion_escogida",
                Scalar = true,
                Response = false,
                TableName = ""
            };

            //definir parametros
            this.DbManager.addParameter("@respuestaId", "int", respuestaId);
            this.DbManager.addParameter("@opcionId", "int", opcionId);

            //Ejercutar el procedimiento en la base de datos
            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }
        }
        public void ActualizarRespuesta(RespuestaEF respuesta)
        {
            _db.Respuestas.Update(respuesta);
            _db.SaveChanges();
        }

        public void EliminarRespuesta(RespuestaEF respuesta)
        {
            _db.Respuestas.Remove(respuesta);
            _db.SaveChanges();
        }

        public List<RespuestaEF> ListarRespuestas(CuestionarioEF cuestionario)
        {
            return _db.Respuestas.
                Where(aux => aux.Pregunta.Cuestionario.Equals(cuestionario)
                        && aux.FechaEliminada == null).
                ToList();
        }

        public List<RespuestaEF> ListarRespuestasTotales(CuestionarioEF cuestionario)
        {
            return _db.Respuestas.
                Where(aux => aux.Pregunta.Cuestionario.Equals(cuestionario)).
                ToList();
        }

        public List<RespuestaEF> ListarRespuestasPorPregunta(PreguntaEF pregunta)
        {
            return _db.Respuestas.
                Where(aux => aux.Pregunta.Equals(pregunta)
                        && aux.FechaEliminada == null).
                ToList();
        }

        public List<RespuestaEF> ListarRespuestasTotalesPorPregunta(PreguntaEF pregunta)
        {
            return _db.Respuestas.
                Where(aux => aux.Pregunta.Equals(pregunta)).
                ToList();
        }

        public RespuestaEF ObtenerPorId(int id) {
            return _db.Respuestas.Find(id);
        }
    }
}
