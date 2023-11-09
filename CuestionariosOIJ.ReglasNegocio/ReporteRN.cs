using Cuestionarios.Domain;
using CuestionariosOIJ.AccesoDatos.EntitiesAD;
using CuestionariosOIJ.AccesoDatos.Models;
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

        public List<ResultadoCuestionario>? Reporte (int cuestionarioId){
            List<ResultadoCuestionario> resultados = new List<ResultadoCuestionario>();

            //consultar los periodos disponibles
            List<DateTime> periodos = _data.ListarPeriodos(cuestionarioId);

            //completar cada resultado por periodo
            resultados.Add(ReportePeriodo(cuestionarioId, null));  
            foreach (DateTime periodo in periodos)
            {
                resultados.Add(ReportePeriodo(cuestionarioId, periodo));
            }

            //retornar la lista de datos
            return resultados;
        }

        private ResultadoCuestionario ReportePeriodo(int cuestionarioId, DateTime? periodo)
        {
            Cuestionario consultado = new CuestionarioRN().ObtenerPorID(cuestionarioId);
            ResultadoCuestionario resultado = new ResultadoCuestionario(consultado);
            resultado.Periodo = periodo;

            List<Pregunta> preguntas = new PreguntaRN().ListarPreguntas(cuestionarioId);
            foreach (Pregunta pregunta in preguntas)
            {
                Reporte reporte = new Reporte();
                reporte.Id = pregunta.Id;
                reporte.TextoPregunta = pregunta.ContenidoPregunta;
                reporte.TipoRespuesta = pregunta.TipoRespuesta;
                switch (pregunta.TipoRespuesta)
                {
                    case "Seleccion Unica":
                        reporte.ResultadosDB = _data.ReporteSeleccionUnica(pregunta.Id, periodo);
                        break;
                    case "Seleccion Multiple":
                        reporte.ResultadosDB = _data.ReporteSeleccionMultiple(pregunta.Id, periodo);
                        break;
                    case "Texto Corto":
                        reporte.ResultadosDB = _data.ReporteTexto(pregunta.Id, periodo);
                        break;
                    case "Texto Largo":
                        reporte.ResultadosDB = _data.ReporteTexto(pregunta.Id, periodo);
                        break;
                    case "Verdadero/Falso":
                        reporte.ResultadosDB = _data.ReporteVerdaderoFalso(pregunta.Id, periodo);
                        break;
                    case "Escala":
                        reporte.ResultadosDB = new
                        {
                            General = _data.ReporteEscala(pregunta.Id, periodo),
                            Desgloce = _data.ReporteListaEscala(pregunta.Id, periodo)
                        };
                        break;
                    default:
                        reporte.ResultadosDB = "No es un tipo de pregunta procesable";
                        break;
                }
                resultado.ReportesPreguntas.Add(reporte);
            }

            return resultado;
        }
    }
}
