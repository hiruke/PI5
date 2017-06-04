using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace FirmDAL.Tables
{
			public class DBClients
			{

						public int clid;
						public int uid;
						public int sid;
						public int status;
						public string name;
						public string service;
						private string table = "clients";

						public DBClients(int clid, int uid, int sid, int status)
						{
									this.clid = clid;
									this.uid = uid;
									this.sid = sid;
									this.status = status;
						}

						public DBClients(int uid, int sid)
						{
									this.uid = uid;
									this.sid = sid;
						}

						public DBClients(int sid)
						{
									SqlDataReader reader = new SelectQuery("clid;uid;sid;status", table, "sid=" + sid).Read();
									while (reader.Read())
									{
												this.clid = reader.GetInt32(0);
												this.uid = reader.GetInt32(1);
												this.sid = reader.GetInt32(2);
												this.status = reader.GetInt32(3);
									}
									reader.Close();
						}

						public string Update()
						{
									string values = uid + ";" + sid + ";" + status;
									return new UpdateQuery(table, "uid;sid;status", values, "clid=" + this.clid).Exec();
						}
						public string Delete()
						{
									return new DeleteQuery(table, "clid=" + this.clid).Exec();
						}

						public string Create()
						{
									string values = uid + ";" + sid + ";" + status;
									return new InsertQuery(table, "uid;sid;status", values).Exec();
						}

			}
}
