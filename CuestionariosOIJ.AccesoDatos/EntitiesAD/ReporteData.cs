using CuestionariosOIJ.AccesoDatos.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuestionariosOIJ.AccesoDatos.EntitiesAD
{
    public class ReporteData
    {
        private DataBaseManager _dbManager;

        public ReporteData()
        {
        }

        internal DataBaseManager DbManager { get => _dbManager; set => _dbManager = value; }

        public List<DateTime> ListarPeriodos (int cuestionarioId)
        {
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_obtener_fechas_eliminacion_por_cuestionario",
                Scalar = false,
                TableName = "Reporte"
            };
            this.DbManager.addParameter("@CuestionarioID", "int", cuestionarioId);

            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }


            // Acceder a los resultados en el DataSet
            DataSet dsResults = this.DbManager.DsResults;
            List<DateTime> periodos = new List<DateTime>();

            if (dsResults != null && dsResults.Tables.Contains("Reporte"))
            {
                DataTable resultTable = dsResults.Tables["Reporte"];
                if (resultTable != null)
                {
                    int cantidadFilas = resultTable.Rows.Count;
                    Console.WriteLine($"La DataTable tiene {cantidadFilas} filas.");
                }

                // Recorrer las filas del resultado
                foreach (DataRow row in resultTable.Rows)
                {
                    if (!Convert.IsDBNull(row["Dia"]) && !Convert.IsDBNull(row["Mes"]) && !Convert.IsDBNull(row["Anio"]))
                    {
                        int dia = Convert.ToInt32(row["Dia"]);
                        int mes = Convert.ToInt32(row["Mes"]);
                        int anio = Convert.ToInt32(row["Anio"]);

                        periodos.Add(new DateTime(anio, mes, dia));
                    }
                }
            }
            return periodos;
        }

        public List<object>? ReporteSeleccionUnica(int preguntaID, DateTime? periodo)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_reporte_seleccion_unica",
                Scalar = false,
                TableName = "Reporte"
            };

            //definir parametros
            this.DbManager.addParameter("@preguntaId", "int", preguntaID);
            string periodoString;
            if (periodo != null)
            {
                DateTime date = periodo.Value;
                periodoString = $"{date.Day}/{date.Month}/{date.Year}";
            }
            else
            {
                periodoString = ""; //significa null
            }

            Console.WriteLine(periodoString);
            this.DbManager.addParameter("@periodo", "datetime", periodoString);


            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }


            // Acceder a los resultados en el DataSet
            DataSet dsResults = this.DbManager.DsResults;

            if (dsResults != null && dsResults.Tables.Contains("Reporte"))
            {
                List<object> resultados = new List<object>();
                DataTable resultTable = dsResults.Tables["Reporte"];

                // Recorrer las filas del resultado
                foreach (DataRow row in resultTable.Rows)
                {
                    string textoOpcion = row["TextoOpcion"].ToString();
                    int elecciones = Convert.ToInt32(row["Elecciones"]);
                    decimal porcentaje = Convert.ToDecimal(row["Porcentaje"]);

                    resultados.Add(new
                    {
                        Opcion = textoOpcion,
                        Elecciones = elecciones,
                        Porcentaje = porcentaje
                    });
                    // Hacer algo con los datos, como imprimirlos o almacenarlos en una estructura de datos
                    Console.WriteLine($"TextoOpcion: {textoOpcion}, Elecciones: {elecciones}, Porcentaje: {porcentaje}");
                }
                return resultados;
            }
            return null;
        }

        public List<object>? ReporteSeleccionMultiple(int preguntaID, DateTime? periodo)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_reporte_seleccion_multiple",
                Scalar = false,
                TableName = "Reporte"
            };

            //definir parametros
            this.DbManager.addParameter("@preguntaId", "int", preguntaID);
            string periodoString;
            if (periodo != null)
            {
                DateTime date = periodo.Value;
                periodoString = $"{date.Day}/{date.Month}/{date.Year}";
            }
            else
            {
                periodoString = ""; //significa null
            }

            Console.WriteLine(periodoString);
            this.DbManager.addParameter("@periodo", "datetime", periodoString);

            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }


            // Acceder a los resultados en el DataSet
            DataSet dsResults = this.DbManager.DsResults;

            if (dsResults != null && dsResults.Tables.Contains("Reporte"))
            {
                List<object> resultados = new List<object>();
                DataTable resultTable = dsResults.Tables["Reporte"];

                // Recorrer las filas del resultado
                foreach (DataRow row in resultTable.Rows)
                {
                    string textoOpcion = row["TextoOpcion"].ToString();
                    int elecciones = Convert.ToInt32(row["Elecciones"]);
                    int total = Convert.ToInt32(row["TotalRespuestas"]);

                    resultados.Add(new
                    {
                        Opcion = textoOpcion,
                        Elecciones = elecciones,
                        Total = total
                    });
                }
                return resultados;
            }
            return null;
        }

        public List<object>? ReporteTexto(int preguntaID, DateTime? periodo)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_reporte_texto",
                Scalar = false,
                TableName = "Reporte"
            };

            //definir parametros
            this.DbManager.addParameter("@preguntaId", "int", preguntaID);

            string periodoString;
            if (periodo != null)
            {
                DateTime date = periodo.Value;
                periodoString = $"{date.Day}/{date.Month}/{date.Year}";
            }
            else
            {
                periodoString = ""; //significa null
            }

            Console.WriteLine(periodoString);
            this.DbManager.addParameter("@periodo", "datetime", periodoString);

            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }


            // Acceder a los resultados en el DataSet
            DataSet dsResults = this.DbManager.DsResults;

            if (dsResults != null && dsResults.Tables.Contains("Reporte"))
            {
                List<object> resultados = new List<object>();
                DataTable resultTable = dsResults.Tables["Reporte"];

                // Recorrer las filas del resultado
                foreach (DataRow row in resultTable.Rows)
                {
                    string usuario = row["Usuario"].ToString();
                    string textoRespuesta = row["TextoRespuesta"].ToString();

                    resultados.Add(new
                    {
                        Usuario = usuario,
                        TextoRespuesta = textoRespuesta
                    });
                }
                return resultados;
            }
            return null;
        }

        public List<object>? ReporteVerdaderoFalso(int preguntaID, DateTime? periodo)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_reporte_verdadero_falso",
                Scalar = false,
                TableName = "Reporte"
            };

            //definir parametros
            this.DbManager.addParameter("@preguntaId", "int", preguntaID);
            string periodoString;
            if (periodo != null)
            {
                DateTime date = periodo.Value;
                periodoString = $"{date.Day}/{date.Month}/{date.Year}";
            }
            else
            {
                periodoString = ""; //significa null
            }

            Console.WriteLine(periodoString);
            this.DbManager.addParameter("@periodo", "datetime", periodoString);

            //Ejecutar la consulta
            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }


            // Acceder a los resultados en el DataSet
            DataSet dsResults = this.DbManager.DsResults;

            if (dsResults != null && dsResults.Tables.Contains("Reporte"))
            {
                List<object> resultados = new List<object>();
                DataTable resultTable = dsResults.Tables["Reporte"];

                // Recorrer las filas del resultado
                foreach (DataRow row in resultTable.Rows)
                {
                    string textoOpcion = row["TextoRespuesta"].ToString();
                    int elecciones = Convert.ToInt32(row["Respuestas"]);
                    decimal porcentaje = Convert.ToDecimal(row["Porcentaje"]);

                    resultados.Add(new
                    {
                        Opcion = textoOpcion,
                        Elecciones = elecciones,
                        Porcentaje = porcentaje
                    });
                }
                return resultados;
            }
            return null;
        }

        public object? ReporteEscala(int preguntaID, DateTime? periodo)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_reporte_escala",
                Scalar = false,
                TableName = "Reporte"
            };

            //definir parametros
            this.DbManager.addParameter("@preguntaId", "int", preguntaID);
            string periodoString;
            if (periodo != null)
            {
                DateTime date = periodo.Value;
                periodoString = $"{date.Day}/{date.Month}/{date.Year}";
            }
            else
            {
                periodoString = ""; //significa null
            }

            Console.WriteLine(periodoString);
            this.DbManager.addParameter("@periodo", "datetime", periodoString);

            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }


            // Acceder a los resultados en el DataSet
            DataSet dsResults = this.DbManager.DsResults;

            if (dsResults != null && dsResults.Tables.Contains("Reporte"))
            {
                List<object> resultados = new List<object>();
                DataTable resultTable = dsResults.Tables["Reporte"];

                // Recorrer las filas del resultado
                foreach (DataRow row in resultTable.Rows)
                {
                    int elecciones = Convert.ToInt32(row["Respuestas"]);
                    decimal promedio = 0;
                    if(elecciones > 0)
                    {
                        promedio = Convert.ToDecimal(row["Promedio"]);
                    }

                    return new
                    {
                        Promedio = promedio,
                        Respuestas = elecciones
                    };
                }
            }
            return null;
        }

        public List<object>? ReporteListaEscala(int preguntaID, DateTime? periodo)
        {
            //Crear el gestor y establecer informacion de control
            this.DbManager = new DataBaseManager()
            {
                DbName = "db_cuestionarios",
                SpName = "sp_reporte_respuestas_escala",
                Scalar = false,
                TableName = "Reporte"
            };

            //definir parametros
            this.DbManager.addParameter("@preguntaId", "int", preguntaID);
            string periodoString;
            if (periodo != null)
            {
                DateTime date = periodo.Value;
                periodoString = $"{date.Day}/{date.Month}/{date.Year}";
            }
            else
            {
                periodoString = ""; //significa null
            }

            Console.WriteLine(periodoString);
            this.DbManager.addParameter("@periodo", "datetime", periodoString);

            //Ejecutar la consulta
            DbManager.ExecuteQuery(ref _dbManager);

            if (DbManager.ErrorMessage.Length > 0)
            {
                throw new Exception(DbManager.ErrorMessage);
            }


            // Acceder a los resultados en el DataSet
            DataSet dsResults = this.DbManager.DsResults;

            if (dsResults != null && dsResults.Tables.Contains("Reporte"))
            {
                List<object> resultados = new List<object>();
                DataTable resultTable = dsResults.Tables["Reporte"];

                // Recorrer las filas del resultado
                foreach (DataRow row in resultTable.Rows)
                {
                    string valor = row["Valor"].ToString();
                    int elecciones = Convert.ToInt32(row["Respuestas"]);

                    resultados.Add(new
                    {
                        Valor = valor,
                        Respuestas = elecciones,
                    });
                }
                return resultados;
            }
            return null;
        }
    }
}
