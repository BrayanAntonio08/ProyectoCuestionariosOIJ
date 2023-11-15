using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void AsignarEncuestado(int respuestaId, string encuestado)
        {
            _db.UsuariosRespuesta.Add(new UsuarioRespuestaEF()
            {
                RespuestaId = respuestaId,
                Usuario = encuestado
            });
            _db.SaveChanges();
        }

        public void ActualizarRespuesta(RespuestaEF respuesta)
        {
            _db.Respuestas.Update(respuesta);
            _db.SaveChanges();
        }

        public RespuestaEF EliminarRespuesta(int respuestaId)
        {

            _db.Respuestas.Find(respuestaId).FechaEliminada = DateTime.Now;
            _db.SaveChanges();
            return _db.Respuestas.Find(respuestaId);

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

        public class RespuestaUsuario
        {
            public string? Usuario { get; set; }
            public string? RespuestaCuestionarioId { get; set; }

            public DateTime Fecha {  get; set; }
        }
        public List<RespuestaUsuario> ListarUsuariosRespondidos(int cuestionarioId)
        {
            var resultados = from r in _db.Respuestas
                             join ur in _db.UsuariosRespuesta on r.Id equals ur.RespuestaId into usuariosRespuestas
                             from ur in usuariosRespuestas.DefaultIfEmpty() // Left Join
                             join p in _db.Preguntas on r.PreguntaId equals p.Id
                             where p.CuestionarioId == cuestionarioId
                             group new
                             {
                                 r.RespuestaCuestionarioId,
                                 FechaRespondida = new DateTime(r.FechaRespondida.Year, r.FechaRespondida.Month, r.FechaRespondida.Day),
                                 Usuario = ur != null ? ur.Usuario : "Anónimo"
                             } by new
                             {
                                 r.RespuestaCuestionarioId,
                                 FechaRespondida = new DateTime(r.FechaRespondida.Year, r.FechaRespondida.Month, r.FechaRespondida.Day),
                                 Usuario = ur != null ? ur.Usuario : "Anónimo"
                             } into g
                             select new
                             {
                                 RespuestaCuestionarioId = g.Key.RespuestaCuestionarioId,
                                 Fecha = g.Key.FechaRespondida,
                                 Usuario = g.Key.Usuario
                             };
            List<RespuestaUsuario> usuarios = new List<RespuestaUsuario>();
            foreach(var resultado in resultados)
            {
                usuarios.Add(new RespuestaUsuario
                {
                    RespuestaCuestionarioId = resultado.RespuestaCuestionarioId,
                    Usuario = resultado.Usuario,
                    Fecha = resultado.Fecha
                });
            }
            return usuarios;
        }

        public List<object> ObtenerRespuestas(string resultadoCuestionarioId)
        {
            using(var db = new CuestionariosContext())
            {
                var resultados = 
                    from r in db.Respuestas
                                 join p in db.Preguntas on r.PreguntaId equals p.Id
                                 join tp in db.TiposPregunta on p.TipoPreguntaId equals tp.Id
                                 where r.RespuestaCuestionarioId == resultadoCuestionarioId
                                 select new
                                 {
                                     Id = r.Id,
                                     Pregunta = p.TextoPregunta,
                                     Respuesta = !tp.Nombre.StartsWith("Seleccion") ? r.TextoRespuesta : "EX100-vary"
                                 };

                List<object> list = new List<object>();
                foreach(var item in resultados)
                {
                    list.Add(new { 
                        Id = item.Id,
                        Pregunta = item.Pregunta,
                        Respuesta = item.Respuesta
                    });
                }

                return list;
            }
        }

        public string LeerOpcionesRespuesta(int respuestaId)
        {
            //var consulta = from r in _db.Respuestas
            //               where r.Id == respuestaId
            //               select r;
            ICollection<OpcionRespuestaEF> opciones = _db.Respuestas.Include(res => res.OpcionRespuesta).FirstOrDefault(res => res.Id == respuestaId).OpcionRespuesta;
            string respuesta = "";
            foreach (var opcion in opciones)
            {
                if (respuesta.Length > 0) respuesta += ", ";
                respuesta += opcion.TextoOpcion;
            }
            return respuesta;
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
