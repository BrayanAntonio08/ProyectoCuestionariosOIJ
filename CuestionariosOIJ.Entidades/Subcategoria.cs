using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class Subcategoria
    {
        private int _id;
        private string _nombre;
        private Categoria _categoria;

        public Subcategoria()
        {
            _id = 0;
            _nombre = string.Empty;
            _categoria = new Categoria();
        }

        public Subcategoria(string nombre, Categoria categoria)
        {
            _nombre = nombre;
            _categoria = categoria;
        }

        public Subcategoria(int id, string nombre, Categoria categoria)
        {
            _id = id;
            _nombre = nombre;
            _categoria = categoria;
        }

        public string Nombre { get => _nombre; set => _nombre = value; }
        public Categoria Categoria { get => _categoria; set => _categoria = value; }
        public int Id { get => _id; set => _id = value; }
    }
}
