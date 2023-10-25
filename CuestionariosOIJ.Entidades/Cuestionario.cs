using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuestionarios.Domain
{
    public class Cuestionario
    {
        private int _id;
        private string _codigo, _nombre, _tipo, _oficina, _descripcion, _link;
        private bool _activo;
        private DateTime? _vencimiento, _fechaCreacion;
        private List<Pregunta>? _preguntas;
        private List<Usuario> _revisadores;

        public Cuestionario() { }
        public Cuestionario(string codigo, string nombre, string tipo, string oficina, string descripcion, string link, bool activo, DateTime vencimiento, DateTime fechaCreacion, List<Pregunta> preguntas, List<Usuario> revisadores)
        {
            Codigo = codigo;
            Nombre = nombre;
            Tipo = tipo;
            Oficina = oficina;
            Descripcion = descripcion;
            Link = link;
            Activo = activo;
            Vencimiento = vencimiento;
            FechaCreacion = fechaCreacion;
            Preguntas = preguntas;
            Revisadores = revisadores;
        }

        public int Id { get => _id; set => _id = value; }
        public string Codigo { get => _codigo; set => _codigo = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Oficina { get => _oficina; set => _oficina = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public string Link { get => _link; set => _link = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public DateTime? Vencimiento { get => _vencimiento; set => _vencimiento = value; }
        public DateTime? FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public List<Pregunta>? Preguntas { get => _preguntas; set => _preguntas = value; }
        public List<Usuario> Revisadores { get => _revisadores; set => _revisadores = value; }
    }
}
