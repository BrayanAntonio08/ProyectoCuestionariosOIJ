using CuestionariosOIJ.AccesoDatos.Context;
using CuestionariosOIJ.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class CuestionarioData
    {
        private readonly CuestionariosContext _db;

        public CuestionarioData(CuestionariosContext context)
        {
            _db = context;
        }

        public void Insertar(CuestionarioEF cuestionario)
        {
            _db.Cuestionarios.Add(cuestionario);
            _db.SaveChanges();
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
