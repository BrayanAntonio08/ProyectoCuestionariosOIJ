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
        private string _descripcion;
        private Categoria _categoria;

        public Subcategoria()
        {
        }

        public Subcategoria(string nombre, string descripcion, Categoria categoria)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Categoria = categoria;
        }

        public Subcategoria(int id, string nombre, string descripcion, Categoria categoria)
        {
            Id= id;
            Nombre = nombre;
            Descripcion = descripcion;
            Categoria = categoria;
        }

        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public Categoria Categoria { get => _categoria; set => _categoria = value; }
        public int Id { get => _id; set => _id = value; }
    }
}
