﻿
using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class CuestionarioData
    {
        private readonly CuestionariosContext _db;
        private DataBaseManager _dbManager;

        internal DataBaseManager DbManager { get => _dbManager; set => _dbManager = value; }

        public CuestionarioData(CuestionariosContext context)
        {
            _db = context;
            _dbManager = new DataBaseManager();
        }

        public bool ExisteCodigo(string codigo)
        {
            return _db.Cuestionarios.Where(x => x.Codigo.Equals(codigo)).Count() > 0;
        }

        public string ObtenerTipo(int tipoId)
        {
            return _db.TipoCuestionarios.Where(tipo => tipo.Id == tipoId).First().Nombre;
        }

        public string ObtenerOficina(int oficinaId)
        {
            return _db.Oficinas.Where(tipo => tipo.Id == oficinaId).First().Nombre;
        }

        public void InsertarCuestionario(CuestionarioEF cuestionario)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_InsertarCuestionario",
                Scalar = true,
                Response = false,
                TableName = ""
            };

            //definir parametros
            this.DbManager.addParameter("@Codigo", "varchar", cuestionario.Codigo);
            this.DbManager.addParameter("@Nombre", "varchar", cuestionario.Nombre);
            this.DbManager.addParameter("@Descripcion", "varchar", cuestionario.Descripcion);
            this.DbManager.addParameter("@TipoCuestionarioID", "int", cuestionario.TipoCuestionarioId);
            this.DbManager.addParameter("@OficinaID", "int", cuestionario.OficinaId);
            this.DbManager.addParameter("@FechaVencimiento", "datetime", new SqlDateTime(cuestionario.FechaVencimiento));
            this.DbManager.addParameter("@Activo", "bit", cuestionario.Activo);


            //Ejercutar el procedimiento en la base de datos
            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }

        }

        public OficinaEF leerOficina(string nombreOficina)
        {
            return _db.Oficinas.Where(x => x.Nombre == nombreOficina).First();
        }

        public TipoCuestionarioEF leerTipo(string nombreTipo)
        {
           return _db.TipoCuestionarios.Where(x => x.Nombre == nombreTipo).First();
        }

        public void Actualizar(CuestionarioEF cuestionario)
        {
            _db.Cuestionarios.Update(cuestionario);
            _db.SaveChanges();
        }

        public void Eliminar(CuestionarioEF cuestionario)
        {
            _db.Cuestionarios.Remove(cuestionario);
            _db.SaveChanges();
        }

        public List<CuestionarioEF> Listar()
        {
            return _db.Cuestionarios.ToList();
        }

        public CuestionarioEF ObtenerPorID(int id)
        {
            return _db.Cuestionarios.Find(id);
        }

        public CuestionarioEF ObtenerPorCodigo(string codigo)
        {
            return _db.Cuestionarios.Where(cuest => cuest.Codigo == codigo).First();
        }

        public List<CuestionarioEF> ListarPorTipo(string tipo)
        {
            return _db.Cuestionarios.Where(cuest => cuest.TipoCuestionario.Nombre == tipo).ToList();
        }

        public List<CuestionarioEF> ListarPorOficina(OficinaEF oficina)
        {
            return _db.Cuestionarios.Where(cuest => cuest.Oficina.Equals(oficina)).ToList();
        }

        public List<CuestionarioEF> BuscarNombre(string nombre)
        {
            return _db.Cuestionarios.Where(cuest => cuest.Nombre.Equals(nombre)).ToList();
        }

        public List<CuestionarioEF> ListarParaRevisador(UsuarioEF revisador)
        {
            return revisador.Cuestionarios.ToList();
        }
    }
}
