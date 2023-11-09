using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class Categoria
    {
        #region Atributos privados

        private int _id;
        private string _nombre;

        #endregion

        #region Propiedades
        public string Nombre { get => _nombre; set => _nombre = value; }
        public int Id { get => _id; set => _id = value; }
        #endregion

        #region Contructores
        public Categoria() 
        {
            Id = 0;
            Nombre = string.Empty;
        }
        #endregion
    }
}
