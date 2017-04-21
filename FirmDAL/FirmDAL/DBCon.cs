using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirmDAL
{
			public static class DBCon
			{
						static private SqlConnection connection = null;
						static private string server = "187.33.48.115";
						static private string database = "firm";
						static private string port = "14333";
						static private string user = "firm";
						static private string pass = "123*abc";
						static private string connetionString = "Data Source=" + server + "," + port + ";MultipleActiveResultSets=true;Initial Catalog=" + database + ";User ID=" + user + ";Password=" + pass;
						static private SqlCommand command = new SqlCommand("", getCon());
						static private SqlDataReader reader;


						public static SqlConnection getCon()
						{
									connection = new SqlConnection(connetionString);
									connection.Open();
									return connection;
						}

						public static void closeCon()
						{
									connection.Close();
						}
						public static SqlDataReader Read(string query)
						{
									command.CommandText = query;
									reader = command.ExecuteReader();
									return reader;
						}

						public static string Exec(string query)
						{
									command.CommandText = query;
									string exception;
									try
									{
												command.ExecuteNonQuery();
												exception = "0x00";
									}
									catch (SqlException ex)
									{
												exception = ex.Number.ToString();

									}
									return exception;
						}

			}
}
