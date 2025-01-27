//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace Common
{
    public static class DatabaseHelper
    {
        private static string _connectionString = "Server=DESKTOP-BP4C1HM\\SQLEXPRESS;Database=IMS;User Id=sa;Password=sa;";

        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static SqlDataReader ExecuteReader(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(_connectionString); // Don't use 'using' here
            SqlCommand cmd = new SqlCommand(query, conn);

            // Add parameters if any
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            conn.Open();

            // CommandBehavior.CloseConnection will close the connection when the SqlDataReader is closed
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value ?? DBNull.Value);
        }

        public static int ExecuteQuery(string query, SqlParameter[] parameters = null, string queryType = "INSERT", bool isStoredProcedure = true)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        con.Open();

                        switch (queryType.ToUpper())
                        {
                            case "INSERT":
                            case "UPDATE":
                            case "DELETE":
                                return cmd.ExecuteNonQuery();
                            case "SELECT":
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                    }
                                }
                                return 0;

                            default:
                                throw new InvalidOperationException("Invalid query type specified.");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error executing the query", ex);
                    }
                }
            }
        }

    }
}
