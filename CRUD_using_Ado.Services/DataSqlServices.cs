using CRUD_using_Ado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using CRUD_using_Ado.DatabaseConnection;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace CRUD_using_Ado.Services
{
    public class DataSqlServices : IDataRepository
    {
        private readonly SqlConnection _sqlConnection;

        public DataSqlServices(DatabaseContext databaseContext)
        {
            _sqlConnection = databaseContext.createConnection();
        }

        public Responce GetDataUsingDirectQuery(string sqlQuery)
        {
            try
            {
                _sqlConnection.Open();
                DataTable dataTable = new DataTable();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, _sqlConnection))
                {
                    // Fill the DataTable with the results of the query
                    dataAdapter.Fill(dataTable);
                }
                _sqlConnection.Close();

                if(dataTable.Rows.Count > 0)
                {

                    Table table = new Table();
                    table.columns = new List<Column>();
                    table.rows = new List<Row>();

                    // Populate columns
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        table.columns.Add(new Column { column_name = column.ColumnName });
                    }

                    // Populate rows
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Row row = new Row
                        {
                            row_id = dataTable.Rows.IndexOf(dataRow) + 1,
                            Row_Data = dataRow.ItemArray.Select(item => item.ToString()).ToArray()
                        };
                        table.rows.Add(row);
                    }

                    return new Responce
                    {
                        statusCode = HttpStatusCode.OK,
                        content = table
                    };
                }
                return new Responce
                {
                    statusCode = HttpStatusCode.NotFound,
                    message = "Data not found"
                };
            }
            catch (Exception)
            {
                return new Responce
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    message = "Internal server error"
                };
            }
        }

        public Responce GetDataWithoutParaValues(string strProcudure)
        {
            try
            {
                _sqlConnection.Open();
                DataTable dataTable = new DataTable();
                SqlCommand sqlCommand = new SqlCommand(strProcudure, _sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    dataAdapter.Fill(dataTable);
                }
                _sqlConnection.Close();

                if(dataTable.Rows.Count > 0)
                {
                    Table table = new Table();
                    table.columns = new List<Column>();
                    table.rows = new List<Row>();

                    foreach(DataColumn dataColumn in dataTable.Columns)
                    {
                        table.columns.Add(new Column { column_name = dataColumn.ColumnName });
                    }

                    foreach(DataRow dataRow in dataTable.Rows)
                    {
                        Row row = new Row
                        {
                            row_id = dataTable.Rows.IndexOf(dataRow) + 1,
                            Row_Data = dataRow.ItemArray.Select(item => item.ToString()).ToArray()
                        };
                        table.rows.Add(row);
                    }

                    return new Responce
                    {
                        statusCode = HttpStatusCode.OK,
                        content = table
                    };
                }
                return new Responce
                {
                    statusCode = HttpStatusCode.NotFound,
                    message = "Data not found"
                };
            }
            catch (Exception)
            {
                return new Responce
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    message = "Internal server error"
                };
            }
        }

        public Responce GetDataWithParaMetersInHeader(string strProcedure, string strParaNames, string strParaValues)
        {
            try
            {
                object[] objParaName = strParaNames.Split('|');
                object[] objParaValue = strParaValues.Split('|');
                DataTable dataTable = new DataTable();

                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(strProcedure, _sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < objParaName.Length; i++)
                {
                    sqlCommand.Parameters.AddWithValue("@" + objParaName[i].ToString(), objParaValue[i]);
                }

                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand))
                {
                    adapter.Fill(dataTable);
                }
                _sqlConnection.Close();

                if(dataTable.Rows.Count > 0)
                {
                    Table table = new Table();
                    table.columns = new List<Column>();
                    table.rows = new List<Row>();

                    foreach(DataColumn dataColumn in dataTable.Columns)
                    {
                        table.columns.Add(new Column { column_name = dataColumn.ColumnName });
                    }
                    foreach(DataRow dataRow in dataTable.Rows)
                    {
                        Row row = new Row
                        {
                            row_id = dataTable.Rows.IndexOf(dataRow) + 1,
                            Row_Data = dataRow.ItemArray.Select(item => item.ToString()).ToArray()
                        };
                        table.rows.Add(row);
                    }
                    return new Responce
                    {
                        statusCode = HttpStatusCode.OK,
                        content = table
                    };
                }
                return new Responce
                {
                    statusCode = HttpStatusCode.NotFound,
                    message = "Data not found"
                };
            }
            catch (Exception)
            {
                return new Responce
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    message = "Internal server error"
                };
            }
        }

        public Responce GetDataWithParaMetersInBody(ProData proData)
        {
            object[] objParaNames = proData.paranames.Split('|');
            object[] objParaValues = proData.paravalues.Split('|');
            DataTable dataTable = new DataTable();

            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(proData.procedureName, _sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                for(int i = 0; i < objParaNames.Length; i++)
                {
                    sqlCommand.Parameters.AddWithValue("@" + objParaNames[i].ToString(), objParaValues[i]);
                }
                using(SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand))
                {
                    dataAdapter.Fill(dataTable);
                }

                if(dataTable.Rows.Count > 0)
                {
                    Table table = new Table();
                    table.columns = new List<Column>();
                    table.rows = new List<Row>();

                    foreach(DataColumn dataColumn in dataTable.Columns)
                    {
                        table.columns.Add(new Column { column_name = dataColumn.ColumnName });
                    }
                    foreach(DataRow dataRow in dataTable.Rows)
                    {
                        Row row = new Row
                        {
                            row_id = dataTable.Rows.IndexOf(dataRow) + 1,
                            Row_Data = dataRow.ItemArray.Select(item => item.ToString()).ToArray()
                        };
                        table.rows.Add(row);
                    }
                    return new Responce
                    {
                        statusCode = HttpStatusCode.OK,
                        content = table
                    };
                }
                return new Responce
                {
                    statusCode = HttpStatusCode.NotFound,
                    message = "Data not found"
                };
            }
            catch (Exception)
            {
                return new Responce
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    message = "Internal server error"
                };
            }
        }

        public Responce InsertDatawithHeader(string strProcedure, string strParaNames, string strParaValues)
        {
            object[] objParaNames = strParaNames.Split('|');
            object[] objParaValues = strParaValues.Split('|');

            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(strProcedure, _sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < objParaNames.Length; i++)
                {
                    sqlCommand.Parameters.AddWithValue("@" + objParaNames[i].ToString(), objParaValues[i]);
                }

                int result = sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();

                if (result > 0)
                {
                    return new Responce
                    {
                        statusCode = HttpStatusCode.OK,
                        message = "Successfully saved"
                    };
                }

                return new Responce
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Save failed"
                };
            }
            catch (Exception)
            {
                return new Responce
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    message = "Internal server error"
                };
            }
        }

        public Responce InsertDatawithBody(ProData proData)
        {
            object[] objParaNames = proData.paranames.Split('|');
            object[] objParaValues = proData.paravalues.Split('|');

            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(proData.procedureName, _sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < objParaNames.Length; i++)
                {
                    sqlCommand.Parameters.AddWithValue("@" + objParaNames[i].ToString(), objParaValues[i]);
                }

                int result = sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();

                if (result > 0)
                {
                    return new Responce
                    {
                        statusCode = HttpStatusCode.OK,
                        message = "Successfully saved"
                    };
                }

                return new Responce
                {
                    statusCode = HttpStatusCode.BadRequest,
                    message = "Save failed"
                };
            }
            catch (Exception)
            {
                return new Responce
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    message = "Internal server error"
                };
            }
        }
    }
}





