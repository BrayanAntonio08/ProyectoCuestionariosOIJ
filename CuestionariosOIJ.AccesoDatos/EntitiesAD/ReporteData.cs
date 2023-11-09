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

        public List<object>? ReporteSeleccionUnica(int preguntaID)
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

        public List<object>? ReporteSeleccionMultiple(int preguntaID)
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

        public List<object>? ReporteTexto(int preguntaID)
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

        public List<object>? ReporteVerdaderoFalso(int preguntaID)
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

        public object? ReporteEscala(int preguntaID)
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

        public List<object>? ReporteListaEscala(int preguntaID)
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
