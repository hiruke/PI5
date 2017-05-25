using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace FirmDAL
{
			public class DBUser
			{
						public int uid;
						public int status;
						public string name;
						public string phone;
						public string email;
						private string password;
						private string table = "users";


						/// <summary>
						/// Cria objeto com todos os dados. Utilizado para transferência de dados.
						/// </summary>
						/// <param name="uid"></param>
						/// <param name="status"></param>
						/// <param name="name"></param>
						/// <param name="phone"></param>
						/// <param name="email"></param>
						/// <param name="password"></param>
						public DBUser(int uid, int status, string name, string phone, string email, string password)
						{
									this.uid = uid;
									this.status = status;
									this.name = name;
									this.phone = phone;
									this.email = email;
									this.password = password;
						}

						public DBUser(string name, string phone, string email, string password)
						{
									this.name = name;
									this.phone = phone;
									this.email = email;
									this.password = password;
						}


						/// <summary>
						/// Busca os dados do objeto a partir de sua ID no banco de dados.
						/// </summary>
						/// <param name="uid"></param>
						public DBUser(int uid)
						{
									this.uid = uid;
									SqlDataReader reader = new SelectQuery("status;name;phone;email;password", table, "uid=" + uid).Read();
									while (reader.Read())
									{
												this.status = reader.GetInt32(0);
												this.name = reader.GetString(1);
												this.phone = reader.GetString(2);
												this.email = reader.GetString(3);
												this.password = reader.GetString(4);

									}
									reader.Close();
						}

						public DBUser(string email)
						{
									this.email = email;
									SqlDataReader reader = new SelectQuery("uid;status;name;phone;password", table, "email='" + email + "'").Read();
									while (reader.Read())
									{
												this.uid = reader.GetInt32(0);
												this.status = reader.GetInt32(1);
												this.name = reader.GetString(2);
												this.phone = reader.GetString(3);
												this.password = reader.GetString(4);

									}
									reader.Close();
						}

                        public string getPassword()
                        {
                            return this.password;
                        }

						public string Update()
						{
									string values = "'" + name + "';'" + phone + "';'" + email + "';'" + password + "'";
									return new UpdateQuery(table, "name;phone;email;password", values, "uid=" + this.uid).Exec();
						}
						public void Delete()
						{
									new DeleteQuery(table, "uid=" + this.uid).Exec();
						}

						public string Create()
						{
									string values = "" + status + ";'" + name + "';'" + phone + "';'" + email + "';'" + password + "'";
								return	new InsertQuery(table, "status;name;phone;email;password", values).Exec();
						}

			}
}
