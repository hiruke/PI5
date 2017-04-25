using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
						private string table = "services";


						public DBNotifications(int nid, int uid, int type, DateTime timestamp, int status, string message, string command)
						{
									this.nid=nid;
									this.uid = uid;
									this.cid = cid;
									this.name = name;
									this.description = description;
						}

						public DBServices(int sid)
						{
									SqlDataReader reader = new SelectQuery("uid;cid;name;description", table, "sid=" + sid).Read();
									this.sid = sid;
									while (reader.Read())
									{
												this.uid = reader.GetInt32(0);
												this.cid = reader.GetInt32(1);
												this.name = reader.GetString(2);
												this.description = reader.GetString(3);
									}
									reader.Close();
						}

						public void Update()
						{
									string values = "'" + name + "';'" + description + "'";
									new UpdateQuery(table, "name;description", values, "sid=" + this.sid).Exec();
						}
						public void Delete()
						{
									new DeleteQuery(table, "sid=" + this.sid).Exec();
						}

						public void Create()
						{

									string values = uid + ";'" + name + "';'" + description + "'";
									new InsertQuery(table, "uid;name;description", values).Exec();
						}

			}
}
