using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirmDAL.Tables
{
			public class DBCategories
			{
						public int cid;
						public string name;
						public string description;
						private string table = "categories";

						public DBCategories(string name, string description)
						{
									this.name = name;
									this.description = description;
						}


						/// <summary>
						/// Busca os dados do objeto a partir de sua ID no banco de dados.
						/// </summary>
						/// <param name="cid"></param>
						public DBCategories(int cid)
						{
									this.cid = cid;
									SqlDataReader reader = new SelectQuery("name;description", table, "cid=" + cid).Read();
									while (reader.Read())
									{
												this.name = reader.GetString(0);
												this.description = reader.GetString(1);
									}
									reader.Close();
						}

						public string Update()
						{
									string values = "'" + name + "';'" + description + "'";
									return new UpdateQuery(table, "name;description", values, "cid=" + this.cid).Exec();
						}
						public void Delete()
						{
									new DeleteQuery(table, "cid=" + this.cid).Exec();
						}

						public string Create()
						{
									string values = "'" + name + "';'" + description + "'";
									return new InsertQuery(table, "name;description", values).Exec();
						}

			}
}
