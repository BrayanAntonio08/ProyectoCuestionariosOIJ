using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class UsuarioData
    {
        private readonly CuestionariosContext _db;
        private DataBaseManager _dbManager;

        internal DataBaseManager DbManager { get => _dbManager; set => _dbManager = value; }

        public UsuarioData(CuestionariosContext cuestionariosContext) {
            _db = cuestionariosContext;
        }

        public UsuarioEF IniciarSesion(string nombreUsuario, string contrasena)
        {
            UsuarioEF aux = _db.Usuarios.
                Where(
                    user => user.NombreUsuario == nombreUsuario
                    && user.Contrasena == contrasena
                    && !user.Eliminado
                ).FirstOrDefault();
            if(aux == null )
            {
                return null;
            }

            return aux;
                
        }

        public void ActivarSesion(UsuarioEF usuario)
        {
            usuario.Activo = true;
            _db.Update(usuario);
            _db.SaveChanges();
        }

        public void CerrarSesion(UsuarioEF usuario)
        {
            usuario.Activo = false;
            _db.Update(usuario);
            _db.SaveChanges();
        }

        public List<UsuarioEF> ListarUsuarios()
        {
            return _db.Usuarios.ToList();
        }

        public List<UsuarioEF> ListarRevisadores(int cuestionarioId)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_listar_revisadores",
                Scalar = false,
                TableName = "Usuarios"
            };

            //definir parametros
            this.DbManager.addParameter("@cuestionarioID", "int", cuestionarioId);

            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }

            List<UsuarioEF> revisadores = new List<UsuarioEF>();
            DataTable resultado = DbManager.DsResults.Tables["Usuarios"];
            foreach (DataRow datos in resultado.Rows)
            {

                revisadores.Add(
                    new UsuarioEF()
                    {
                        Id = (int)datos["Id"],
                        NombreUsuario = (string)datos["NombreUsuario"]
                    }
                    );
            }
            return revisadores;
        }
    }
}
