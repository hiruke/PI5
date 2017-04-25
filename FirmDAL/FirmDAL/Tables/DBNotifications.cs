using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirmDAL.Tables
{
			class DBNotifications
			{

						private int nid;
						private int uid;
						private int type;
						private DateTime timestamp;
						private int status;
						private string message;
						private string command;
						private string table = "notifications";


						public DBNotifications(int nid, int uid, int type, DateTime timestamp, int status, string message, string command)
						{
									this.nid = nid;
									this.uid = uid;
									this.type = type;
									this.timestamp = timestamp;
									this.status = status;
									this.message = message;
									this.command = command;
						}

						public DBNotifications(int nid)
						{
									SqlDataReader reader = new SelectQuery("uid;type;timestamp;status;message;command", table, "nid=" + nid).Read();
									this.nid = nid;
									while (reader.Read())
									{

												this.uid = reader.GetInt32(0);
												this.type = reader.GetInt32(1);
												this.timestamp = reader.GetDateTime(2);
												this.status = reader.GetInt32(3);
												this.message = reader.GetString(4);
												this.command = reader.GetString(5);
									}
									reader.Close();
						}

						public void Delete()
						{
									new DeleteQuery(table, "nid=" + this.nid).Exec();
						}

						public void Create()
						{

									string values = uid + ";" + type + ";" + "SYSDATETIME()" + ";" + status + ";'" + message + "';'" + command + "'";
									new InsertQuery(table, "uid;type;timestamp;status;message;command", values).Exec();
						}

			}
}
