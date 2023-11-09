using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.ReglasNegocio
{
    public class ReporteRN
    {
        private ReporteData _data; 
        public ReporteRN() { 
            _data = new ReporteData();
        }

        public List<Reporte>? Reporte (int cuestionarioId){

            List<Pregunta> preguntas = new PreguntaRN().ListarPreguntas(cuestionarioId);
            List<Reporte> reportes = new List<Reporte>();
            foreach(Pregunta pregunta in preguntas)
            {
                Reporte reporte = new Reporte();
                reporte.Id = pregunta.Id;
                reporte.TextoPregunta = pregunta.ContenidoPregunta;
                reporte.TipoRespuesta = pregunta.TipoRespuesta;
                switch (pregunta.TipoRespuesta)
                {
                    case "Seleccion Unica":
                        reporte.ResultadosDB = _data.ReporteSeleccionUnica(pregunta.Id);
                        break;
                    case "Seleccion Multiple":
                        reporte.ResultadosDB = _data.ReporteSeleccionMultiple(pregunta.Id);
                        break;
                    case "Texto Corto":
                        reporte.ResultadosDB = _data.ReporteTexto(pregunta.Id);
                        break;
                    case "Texto Largo":
                        reporte.ResultadosDB = _data.ReporteTexto(pregunta.Id);
                        break;
                    case "Verdadero/Falso":
                        reporte.ResultadosDB = _data.ReporteVerdaderoFalso(pregunta.Id);
                        break;
                    case "Escala":
                        reporte.ResultadosDB = new {
                            General = _data.ReporteEscala(pregunta.Id),
                            Desgloce = _data.ReporteListaEscala(pregunta.Id)
                        }; 
                        break;
                    default:
                        reporte.ResultadosDB = "No es un tipo de pregunta procesable";
                        break;
                }
                reportes.Add(reporte);
            }
            return reportes;
        }
    }
}
