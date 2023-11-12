using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CuestionariosOIJ.AccesoDatos.Context
{
    internal class DataBaseManager
    {


        #region Private Attributes
        private readonly string _conectionString = "" +
            "Server=163.178.107.10; " +
            "Initial Catalog=DataBaseCuestionarios; " +
            "Persist Security Info=False; " +
            "User ID=laboratorios; " +
            "Password=TUy&)&nfC7QqQau.%278UQ24/=%;";
        private SqlConnection _objSqlConnection;
        private SqlCommand _objSqlCommand;
        private SqlDataAdapter _objSqlDataAdapter;
        private DataSet _dsResults;
        private DataTable _dtParameters;
        private string _tableName, _dbName, _spName, _scalarValue, _errorMessage;
        private bool _scalar, _response;

        #endregion

        #region Public Attributes

        public SqlConnection ObjSqlConnection { get => _objSqlConnection; set => _objSqlConnection = value; }
        public SqlCommand ObjSqlCommand { get => _objSqlCommand; set => _objSqlCommand = value; }
        public SqlDataAdapter ObjSqlDataAdapter { get => _objSqlDataAdapter; set => _objSqlDataAdapter = value; }
        public DataSet DsResults { get => _dsResults; set => _dsResults = value; }
        public DataTable DtParameters { get => _dtParameters; set => _dtParameters = value; }
        public string TableName { get => _tableName; set => _tableName = value; }
        public string DbName { get => _dbName; set => _dbName = value; }
        public string SpName { get => _spName; set => _spName = value; }
        public string ScalarValue { get => _scalarValue; set => _scalarValue = value; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        public bool Scalar { get => _scalar; set => _scalar = value; }
        public bool Response { get => _response; set => _response = value; }

        #endregion

        #region Constructores

        public DataBaseManager()
        {
            DtParameters = new DataTable("spParameters");
            DtParameters.Columns.Add("Name");
            DtParameters.Columns.Add("DataType");
            DtParameters.Columns.Add("Value");

            DbName = String.Empty;

            ErrorMessage = "";
        }

        #endregion

        #region Private Methods

        private void createConnection(ref DataBaseManager dataBaseManager)
        {
            switch(dataBaseManager.DbName)
            {
                case "db_cuestionarios":
                    dataBaseManager.ObjSqlConnection = new SqlConnection(this._conectionString);
                    break;
            }
        }

        private void switchConnectionState(ref DataBaseManager dataBaseManager)
        {
            if(dataBaseManager.ObjSqlConnection.State == ConnectionState.Closed)
            {
                dataBaseManager.ObjSqlConnection.Open();
            }
            else
            {
                dataBaseManager.ObjSqlConnection.Close();
                dataBaseManager.ObjSqlConnection.Dispose();
            }
        }
        private void setParameters(ref DataBaseManager dataBaseManager)
        {
            if(dataBaseManager.DtParameters != null)
            {
                foreach (DataRow param in dataBaseManager.DtParameters.Rows)
                {

                    SqlDbType dataType = SqlDbType.Int;
                    switch (param["DataType"].ToString())
                    {
                        case "int":
                            dataType = SqlDbType.Int;
                            break;
                        case "float": 
                            dataType = SqlDbType.Float;
                            break;
                        case "varchar":
                            dataType = SqlDbType.VarChar;
                            break;
                        case "datetime":
                            dataType = SqlDbType.DateTime;
                            break;
                        case "bit":
                            dataType = SqlDbType.Bit;
                            break;
                    }

                    if (dataBaseManager.Scalar)
                    {
                        if (param["Value"].ToString().Equals(String.Empty)) //means an empty value: null
                        {
                            dataBaseManager.ObjSqlCommand.Parameters.Add(param["name"].ToString(), dataType).Value = DBNull.Value;
                        }
                        else
                        {
                            dataBaseManager.ObjSqlCommand.Parameters.Add(param["name"].ToString(), dataType).Value = param["Value"];
                        }
                    }
                    else
                    {
                        if (param["Value"].ToString().Equals(String.Empty)) //means an empty value: null
                        {
                            dataBaseManager.ObjSqlDataAdapter.SelectCommand.Parameters.Add(param["name"].ToString(), dataType).Value = DBNull.Value;
                        }
                        else
                        {
                            dataBaseManager.ObjSqlDataAdapter.SelectCommand.Parameters.Add(param["name"].ToString(), dataType).Value = param["Value"];
                        }
                    }
                }//forearch
            }// validate existing parameters
            
        }
        private void prepareConnection(ref DataBaseManager dataBaseManager)
        {
            createConnection(ref dataBaseManager);
            switchConnectionState(ref dataBaseManager);
        }
        private void getDataSetResult(ref DataBaseManager dataBaseManager)
        {
            try
            {
                prepareConnection(ref dataBaseManager);
                dataBaseManager.ObjSqlDataAdapter = new SqlDataAdapter(dataBaseManager.SpName, dataBaseManager.ObjSqlConnection);
                dataBaseManager.ObjSqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                setParameters(ref dataBaseManager);
                dataBaseManager.DsResults = new DataSet();
                dataBaseManager.ObjSqlDataAdapter.Fill(dataBaseManager.DsResults, dataBaseManager.TableName);

            }
            catch (Exception ex)
            {
                dataBaseManager.ErrorMessage = ex.ToString();
            }
            finally
            {
                if(dataBaseManager.ObjSqlConnection.State == ConnectionState.Open)
                {
                    switchConnectionState(ref dataBaseManager);
                }
            }
        }
        private void getScalarResult(ref DataBaseManager dataBaseManager)
        {
            try
            {
                prepareConnection(ref dataBaseManager);
                dataBaseManager.ObjSqlCommand = new SqlCommand(dataBaseManager.SpName, dataBaseManager.ObjSqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                setParameters(ref dataBaseManager);

                if (dataBaseManager.Response)
                {
                    dataBaseManager.ScalarValue = dataBaseManager.ObjSqlCommand.ExecuteScalar().ToString().Trim();
                }
                else
                {
                    dataBaseManager.ObjSqlCommand.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                dataBaseManager.ErrorMessage = ex.ToString();
            }
            finally
            {
                if (dataBaseManager.ObjSqlConnection.State == ConnectionState.Open)
                {
                    switchConnectionState(ref dataBaseManager);
                }
            }
        }

        #endregion

        #region Public Methods

        public string DateToSQL(DateTime date)
        {
            return $"{date.Year}-{date.Month}-{date.Day} {date.Hour}:{date.Minute}:{date.Second}";
        }

        public void addParameter(string name, string dbType,  object value)
        {
            DataRow param = this.DtParameters.NewRow();
            param["Name"] = name;
            param["DataType"] = dbType;
            param["Value"] = value;

            this.DtParameters.Rows.Add(param);
        }
        public void ExecuteQuery(ref DataBaseManager dataBaseManager)
        {
            if (dataBaseManager.Scalar)
            {
                getScalarResult(ref dataBaseManager);
            }
            else
            {
                getDataSetResult(ref dataBaseManager);
            }
        }
        #endregion
    }
}
