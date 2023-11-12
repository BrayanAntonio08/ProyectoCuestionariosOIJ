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
    public class PreguntaData
    {
        private readonly CuestionariosContext _db;
        private DataBaseManager _dbManager;

        internal DataBaseManager DbManager { get => _dbManager; set => _dbManager = value; }
        public PreguntaData(CuestionariosContext context)
        {
            _db = context;
            _dbManager = new DataBaseManager();
        }

        public int InsertarPregunta(PreguntaEF pregunta)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_insertar_pregunta",
                Scalar = true,
                Response = false,
                TableName = ""
            };

            //definir parametros
            this.DbManager.addParameter("@TextoPregunta", "varchar", pregunta.TextoPregunta);
            this.DbManager.addParameter("@Posicion", "int", pregunta.Posicion);
            this.DbManager.addParameter("@Etiqueta", "varchar", pregunta.Etiqueta);
            this.DbManager.addParameter("@Justificacion", "bit", pregunta.Justificacion);
            this.DbManager.addParameter("@Obligatoria", "bit", pregunta.Obligatoria);
            this.DbManager.addParameter("@CategoriaID", "int", pregunta.CategoriaId);
            this.DbManager.addParameter("@SubcategoriaID", "int", pregunta.SubcategoriaId);
            this.DbManager.addParameter("@TipoPreguntaID", "int", pregunta.TipoPreguntaId);
            this.DbManager.addParameter("@CuestionarioID", "int", pregunta.CuestionarioId);


            //Ejercutar el procedimiento en la base de datos
            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }

            return _db.Preguntas.
                Where(aux => aux.TextoPregunta == pregunta.TextoPregunta).
                OrderByDescending(e => e.Id).
                FirstOrDefault().Id;
        }

        public void ActualizarPregunta(PreguntaEF pregunta)
        {
            _db.Preguntas.Update(pregunta);
            _db.SaveChanges();
        }


        public void EliminarPregunta(PreguntaEF pregunta)
        {
            pregunta.Eliminado = true;
            _db.Preguntas.Update(pregunta);
            _db.SaveChanges();
        }

        public IEnumerable<PreguntaEF> ListarPreguntas(int cuestionarioId)
        {
            IEnumerable<PreguntaEF> temp = _db.Preguntas
                .Where(aux => aux.CuestionarioId == cuestionarioId && !aux.Eliminado)
                .OrderBy(aux => aux.Posicion)
                .ToList();


            foreach (PreguntaEF ef in temp)
            {
                ef.TipoPregunta = _db.TiposPregunta.Find(ef.TipoPreguntaId);
            }
            return temp;
        }

        public PreguntaEF ObtenerPreguntaPorID(int id)
        {
            PreguntaEF temp = _db.Preguntas.Find(id);
            if(temp != null)
            {
                temp.TipoPregunta = _db.TiposPregunta.Find(id);
            }
            return temp;
        }

        public PreguntaEF ObtenerPreguntaEn(CuestionarioEF cuestionario, int posicion)
        {
            return _db.Preguntas.
                Where(aux => aux.Cuestionario.Equals(cuestionario)
                    && aux.Posicion == posicion).
                First();
        }

        public int BuscarTipoPreguntaPorNombre(string nombre)
        {
            return _db.TiposPregunta.Where(x => x.Nombre.Equals(nombre)).First().Id;
        }

        public string consultarTipoPregunta(int id)
        {
            TipoPreguntaEF ef = _db.TiposPregunta.Find(id);
            if(ef != null)
                return ef.Nombre;
            return "";
        }


        public int ObtenerUltimaPosicion(int cuestionario)
        {
            return _db.Preguntas.Where(aux => aux.CuestionarioId == cuestionario).ToList().Count() + 1;
        }

        public IEnumerable<PreguntaEF> ListarPreguntasSeleccion(int cuestionario)
        {
            IEnumerable<PreguntaEF> temp = _db.Preguntas.
                Where(aux => aux.CuestionarioId == cuestionario 
                && aux.TipoPregunta.Nombre.StartsWith("Seleccion"));
            List<TipoPreguntaEF> tipos = _db.TiposPregunta.ToList();

            foreach (PreguntaEF ef in temp)
            {
                ef.TipoPregunta = tipos.Find(tipo => tipo.Id == ef.TipoPreguntaId);
            }
            return temp;
        }
    }
}
